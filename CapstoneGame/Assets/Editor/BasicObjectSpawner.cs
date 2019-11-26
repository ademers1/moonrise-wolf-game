using UnityEngine;
using UnityEngine.AI;
using UnityEditor;

public class BasicObjectSpawner : EditorWindow
{
    #region Base Variables
    MonoScript scriptToAdd;
    GameObject objToSpawn;
    string objBaseName = "";
    int objID = 0;
    int amountOfObjToSpawn = 1;
    float objScale = 1f;
    float spawnRadius = 5f;
    bool allowSceneObjects = false;
    Vector2 scrollPos;
    bool advancedMenu = false;
    #endregion Base Variables

    #region RigidBody
    bool addRigidBody;
    bool isKinematic;
    bool useGravity;
    float rbMass = 1;
    float rbDrag = 0;
    float rbAngularDrag = 0.05f;
    RigidBodyInterpolate rbInterpolate = RigidBodyInterpolate.None;
    RigidBodyCollsion rbCollisionType = RigidBodyCollsion.Discrete;
    enum RigidBodyInterpolate
    {
        None,
        Interpolate,
        Extrapolate,
    };
    enum RigidBodyCollsion
    {
        Discrete,
        Continuous,
        ContinuousDynamic,
        ContinuousSpeculative,
    };

    #endregion RigidBody

    #region Collider


    #region Box Collider
    bool addBoxCollider;
    bool isBoxTrigger;
    PhysicMaterial boxPhysicsMat;
    Vector3 boxCenter;
    Vector3 boxSize;
    int variableThatDoesNothing; // This makes my headaches go away
    #endregion Box Collider

    #region Sphere Collider
    bool addSphereCollider;
    bool isSphereTrigger;
    PhysicMaterial spherePhysMat;
    Vector3 sphereCenter;
    float sphereRadius;
    #endregion Sphere Collider

    #region Capsule Collider
    bool addCapsuleCollider;
    bool isCapsuleTrigger;
    PhysicMaterial capsulePhysMat;
    Vector3 capsuleCenter;
    float capsuleRadius;
    float capsuleHeight;
    int capsuleDirection;
    #endregion Capsule Collider

    #endregion Collider

    #region Navmesh

    #region Agent
    bool addNavMeshAgent;
    float navBaseOffset;
    #region steering
    float navSpeed;
    float navAngSpeed;
    float navAcceleration;
    float navStopDistance;
    bool navAutoBrake;
    #endregion Steering

    #region Obstacle Avoidance
    float navAvoidRadius;
    float navAvoidHeight;
    int navAvoidPriority;
    NavAvoidQualityEnum navAvoidQuality;
    #region Nav Avoidance Quality Enum
    enum NavAvoidQualityEnum
    {
        None,
        Low,
        Medium,
        Good,
        High

    }
    #endregion Nav Avoidance Quality Enum

    #endregion Obstacle Avoidance

    #endregion Agent

    #region Obstacle
    bool addNavMeshObstacle;
    ObsShapeEnum obsShape = ObsShapeEnum.Capsule;
    #region shapeEnum
    enum ObsShapeEnum
    {
        Capsule,
        Box
    };
    #endregion
    Vector3 obsCenter;
    Vector3 obsSize;
    bool obsCarve;
    float obsMoveThreshhold;
    float obsTimeToStationary;
    bool obsCarveStationary;
    #endregion Obstacle

    #endregion Navmesh

    #region Animator
    bool addAnimator;
    RuntimeAnimatorController animController;
    Avatar animAvatar;
    bool animRootMotion;
    AnimatorUpdateModeEnum animUpdateMode;
    AnimatorCullingModeEnum animCullMode;
    enum AnimatorUpdateModeEnum
    {
        Normal,
        Animate,
        Unscaled
    };

    enum AnimatorCullingModeEnum
    {
        AlwaysAnimate,
        CullUpdateTransforms,
        CullCompletely
    };


    #endregion Animator

    #region Light
    bool addLight;
    LightTypeEnum lightType;
    float lightRange;
    Color lightColour;
    LightModeEnum lightMode;
    float lightIntensity;
    float lightIndirectMultiplier;
    LightShadowTypeEnum lightShadow;
    Flare lightFlare;

    #region Light Type Enum
    enum LightTypeEnum
    {
        Spot,
        Directional,
        Point,
        Area
    };
    #endregion Light Type Enum

    #region Light Mode Enum
    enum LightModeEnum
    {
        Realtime,
        Mixed,
        Baked
    }
    #endregion Light Mode Enum

    #region Shadow Type Enum
    enum LightShadowTypeEnum
    {
        None,
        Hard,
        Soft
    }
    #endregion Shadow Type Enum

    #region Render Mode Enum
    enum LightRenderModeEnum
    {
        Auto,
        Important,
        NotImportant
    }
    #endregion Render Mode Enum

    #endregion Light

    [MenuItem("Tools/Basic Object Spawner")]
    public static void ShowWindow()
    {
        BasicObjectSpawner window = (BasicObjectSpawner)GetWindow(typeof(BasicObjectSpawner));
        window.minSize = new Vector2(300, 400);
        window.maxSize = new Vector2(600, 800);
        window.Show();
    }

    private void OnGUI()
    {
        #region Spawn Button
        EditorGUILayout.Space();
        EditorGUILayout.Space();
        if (GUILayout.Button("Spawn Object"))
        {
            SpawnObject();
        }
        EditorGUILayout.Space();
        #endregion Spawn Button

        #region Object Container
        GUILayout.Label("Spawn A New Object", EditorStyles.boldLabel);
        objToSpawn = EditorGUILayout.ObjectField("Prefab To Spawn", objToSpawn, typeof(GameObject), allowSceneObjects) as GameObject;
        // scriptToAdd = EditorGUILayout.ObjectField("Script To Add", scriptToAdd, typeof(MonoScript), true) as MonoScript;
        EditorGUI.indentLevel++;
        allowSceneObjects = EditorGUILayout.Toggle("Allow Scene Objects?", allowSceneObjects);
        advancedMenu = EditorGUILayout.Toggle(new GUIContent("Advanced Settings", "Allows Modification Of Component Values"), advancedMenu);
        EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
        EditorGUILayout.Space();
        #endregion Object Container

        #region Scrollable Interface
        scrollPos = GUILayout.BeginScrollView(scrollPos, false, true); // begins scrollbar

        #region Object Null Check
        if (objToSpawn != null)
        {
            #region Base Object Values

            EditorGUILayout.BeginVertical();
            EditorGUILayout.Space();
            GUILayout.Label("Object: " + objBaseName, EditorStyles.boldLabel);
            objBaseName = EditorGUILayout.TextField("Object Name", objBaseName);
            objID = EditorGUILayout.IntSlider("Object ID", objID, 0, 2500);
            objScale = EditorGUILayout.Slider("Base Scale", objScale, 0.01f, 50f);
            spawnRadius = EditorGUILayout.Slider("Spawn Radius", spawnRadius, 0f, 500f);
            amountOfObjToSpawn = EditorGUILayout.IntSlider("Spawn Amount", amountOfObjToSpawn, 1, 2000);
            EditorGUILayout.Space();
            EditorGUILayout.EndVertical();
            EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            #endregion Base Object Values

            #region Add Components
            GUILayout.Label("Add Collider Of Type(s)", EditorStyles.boldLabel);
            EditorGUI.indentLevel++;
            EditorGUILayout.BeginVertical();
            #region Sphere Collider
            addSphereCollider = EditorGUILayout.Toggle("Sphere", addSphereCollider);

            if (addSphereCollider && advancedMenu)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                isSphereTrigger = EditorGUILayout.Toggle("isTrigger", isSphereTrigger);
                spherePhysMat = EditorGUILayout.ObjectField("Physics Material", spherePhysMat, typeof(PhysicMaterial), false) as PhysicMaterial;
                sphereCenter = EditorGUILayout.Vector3Field("Center", sphereCenter);
                sphereRadius = EditorGUILayout.Slider("Radius", sphereRadius, 0.01f, 25f);
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--; EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++; EditorGUI.indentLevel++;
                EditorGUILayout.Space();
            }
            #endregion Sphere Collider

            #region Box Collider
            addBoxCollider = EditorGUILayout.Toggle(new GUIContent("Box", "Will Create A Box Type Collider"), addBoxCollider);

            if (addBoxCollider && advancedMenu) // Wrote == true to try and fix a viewport issue, auto resizing seems to be broken
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                isBoxTrigger = EditorGUILayout.Toggle(new GUIContent("isTrigger", "Is this Collider A Trigger?"), isBoxTrigger);
                boxPhysicsMat = EditorGUILayout.ObjectField("Physics Material", boxPhysicsMat, typeof(PhysicMaterial), false) as PhysicMaterial;
                boxCenter = EditorGUILayout.Vector3Field(new GUIContent("Center", "Center Point On XYZ Axis"), boxCenter);
                boxSize = EditorGUILayout.Vector3Field(new GUIContent("Size", "This Is The Boxes Scale"), boxSize);
                variableThatDoesNothing = EditorGUILayout.IntSlider("", variableThatDoesNothing, 0, 1);
                EditorGUI.indentLevel--;
                EditorGUILayout.Space();
                EditorGUI.indentLevel--; EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++; EditorGUI.indentLevel++;
            }
            #endregion Box Collider

            #region Capsule Collider
            addCapsuleCollider = EditorGUILayout.Toggle("Capsule", addCapsuleCollider);

            if (addCapsuleCollider && advancedMenu)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                isCapsuleTrigger = EditorGUILayout.Toggle("isTrigger", isCapsuleTrigger);
                capsulePhysMat = EditorGUILayout.ObjectField("Physics Material", capsulePhysMat, typeof(PhysicMaterial), false) as PhysicMaterial;
                capsuleCenter = EditorGUILayout.Vector3Field("Center", capsuleCenter);
                capsuleRadius = EditorGUILayout.Slider("Radius", capsuleRadius, 0.01f, 25f);
                capsuleHeight = EditorGUILayout.Slider("Height", capsuleHeight, 0.01f, 25f);
                capsuleDirection = EditorGUILayout.IntSlider("Direction:X|Y|Z", capsuleDirection, 0, 2);
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--; EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++; EditorGUI.indentLevel++;
                EditorGUILayout.Space();
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            if (!addCapsuleCollider)
            {
                EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
            }
            else if (addCapsuleCollider && !advancedMenu)
            {
                EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
            }
            #endregion Capsule Collider

            #region RigidBody
            EditorGUILayout.Space();
            GUILayout.Label("RigidBody", EditorStyles.boldLabel);
            addRigidBody = EditorGUILayout.Toggle("Add Rigidbody", addRigidBody);
            EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();
            EditorGUI.indentLevel++;

            if (addRigidBody && advancedMenu)
            {
                EditorGUILayout.Space();
                EditorGUI.indentLevel++;
                useGravity = EditorGUILayout.Toggle("useGravity", useGravity);
                isKinematic = EditorGUILayout.Toggle("isKinematic", isKinematic);
                rbMass = EditorGUILayout.Slider(new GUIContent("Mass", "Default = 1"), rbMass, 0, 10);  // No object should weigh over 20, gravity will begin to break a bit after 10 through Unity default physics
                rbDrag = EditorGUILayout.Slider(new GUIContent("Drag", "Default = 0"), rbDrag, 0, 10);
                rbAngularDrag = EditorGUILayout.Slider(new GUIContent("Angular Drag", "Default = 0.05"), rbAngularDrag, 0, 1); // from what I can see angular drag is set to default at 0.05 meaning this value shouldnt increase too high
                rbInterpolate = (RigidBodyInterpolate)EditorGUILayout.EnumPopup("Interpolate", rbInterpolate);
                rbCollisionType = (RigidBodyCollsion)EditorGUILayout.EnumPopup("Collision Type", rbCollisionType);
                EditorGUI.indentLevel--;
                EditorGUI.indentLevel--; EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++; EditorGUI.indentLevel++;
                EditorGUILayout.Space();
            }
            EditorGUI.indentLevel--;
            EditorGUILayout.EndVertical();
            #endregion RigidBody

            #region NavMesh Obstacle
            GUILayout.Label("NavMesh Obstacle", EditorStyles.boldLabel);
            addNavMeshObstacle = EditorGUILayout.Toggle("Add NavMesh Obstacle", addNavMeshObstacle);
            EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            if (addNavMeshObstacle && advancedMenu)
            {

                GUILayout.Label("Shape", EditorStyles.boldLabel);
                obsShape = (ObsShapeEnum)EditorGUILayout.EnumPopup("", obsShape);
                GUILayout.Label("Center", EditorStyles.boldLabel);
                obsCenter = EditorGUILayout.Vector3Field("", obsCenter);
                GUILayout.Label("Size", EditorStyles.boldLabel);
                obsSize = EditorGUILayout.Vector3Field("", obsSize);
                if (obsSize == Vector3.zero)
                {
                    obsSize = Vector3.one;
                }
                EditorGUILayout.Space();
                EditorGUILayout.BeginHorizontal();
                GUILayout.Label("Carve", EditorStyles.boldLabel);
                obsCarve = EditorGUILayout.Toggle(new GUIContent("", "Enable To Carve Object From NavMesh"), obsCarve);
                EditorGUILayout.EndHorizontal();
                EditorGUI.indentLevel++;
                if (obsCarve)
                {
                    EditorGUILayout.Space();
                    obsMoveThreshhold = EditorGUILayout.Slider(new GUIContent("Move Threshold", "Default = 0.1"), obsMoveThreshhold, 0, 1);
                    obsTimeToStationary = EditorGUILayout.Slider(new GUIContent("Time to Stationary", "Default = 0.5"), obsTimeToStationary, 0, 1);
                    obsCarveStationary = EditorGUILayout.Toggle(new GUIContent("Carve Stationary", "Enable If This Object Is / Should Be Static"), obsCarveStationary);
                    EditorGUI.indentLevel--;
                    EditorGUILayout.Space();
                    EditorGUI.indentLevel--; EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++; EditorGUI.indentLevel++;
                }

            }
            EditorGUILayout.EndVertical();

            #endregion NavMesh Obstacle

            #region NavMesh Agent
            GUILayout.Label("NavMesh Agent", EditorStyles.boldLabel);
            EditorGUILayout.BeginVertical();
            addNavMeshAgent = EditorGUILayout.Toggle("Add NavMesh Agent", addNavMeshAgent);
            EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;

            if (addNavMeshAgent && advancedMenu)
            {
                GUILayout.Label("Agent Type Will Default To Humanoid", EditorStyles.boldLabel);
                EditorGUILayout.Space();
                navBaseOffset = EditorGUILayout.Slider(new GUIContent("Base Offset", "Default = 0.5"), navBaseOffset, 0, 2);
                EditorGUILayout.Space();

                #region innerSteering
                EditorGUILayout.Space();
                EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
                GUILayout.Label("Steering", EditorStyles.label);
                navSpeed = EditorGUILayout.Slider(new GUIContent("Speed", "Default = 6"), navSpeed, 1f, 20f);
                navAngSpeed = EditorGUILayout.Slider(new GUIContent("Angular Speed", "Default = 120"), navAngSpeed, 1f, 360f);
                navAcceleration = EditorGUILayout.Slider(new GUIContent("Acceleration", "Default = 8"), navAcceleration, 1f, 20f);
                navStopDistance = EditorGUILayout.Slider(new GUIContent("Stop Distance", "Default = 6"), navStopDistance, 1f, 20f);
                navAutoBrake = EditorGUILayout.Toggle(new GUIContent("Auto Brake", "Will the agent automatically stop?"), navAutoBrake);
                EditorGUILayout.Space();
                #endregion innerSteering
                EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
                #region Obstacle Avoidance
                GUILayout.Label("Obstacle Avoidance", EditorStyles.label);
                navAvoidRadius = EditorGUILayout.Slider(new GUIContent("Radius", "Default = 0.5"), navAvoidRadius, 0.5f, 6f);
                navAvoidHeight = EditorGUILayout.Slider(new GUIContent("Height", "Default = 1"), navAvoidHeight, 1f, 6f);
                navAvoidQuality = (NavAvoidQualityEnum)EditorGUILayout.EnumPopup(new GUIContent("Avoidance Quality", "Quality Of The Navigation Avoidance"), navAvoidQuality);
                navAvoidPriority = EditorGUILayout.IntSlider(new GUIContent("Priority", "Default = 50"), navAvoidPriority, 50, 150);
                #endregion Obstacle Avoidance

                EditorGUILayout.Space();
            }
            EditorGUILayout.EndVertical();
            #endregion NavMesh Agent

            #region Animator
            GUILayout.Label("Animator", EditorStyles.boldLabel);

            addAnimator = EditorGUILayout.Toggle(new GUIContent("Add Animator", "Add An Animator Component To Instantiated Objects"), addAnimator);
            EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            if (addAnimator && advancedMenu)
            {

                EditorGUI.indentLevel++; EditorGUI.indentLevel++;
                animController = EditorGUILayout.ObjectField(new GUIContent("Controller", "Animator Controller Should Be Placed Here"), animController, typeof(RuntimeAnimatorController), false) as RuntimeAnimatorController;
                animAvatar = EditorGUILayout.ObjectField(new GUIContent("Avatar", "Model Avatar Should Be Placed Here"), animAvatar, typeof(Avatar), false) as Avatar;
                animRootMotion = EditorGUILayout.Toggle(new GUIContent("Apply Root Motion", "Automatically Move The Object Using The Root Motion From The Animations"), animRootMotion);
                animUpdateMode = (AnimatorUpdateModeEnum)EditorGUILayout.EnumPopup(new GUIContent("Update Mode", "This Controls How Often The Animator Is Updated"), animUpdateMode);
                animCullMode = (AnimatorCullingModeEnum)EditorGUILayout.EnumPopup(new GUIContent("Culling Mode", "Controls What Is Updated When The Object Is Culled"), animCullMode);
                EditorGUI.indentLevel--; EditorGUI.indentLevel--;

            }
            EditorGUILayout.EndVertical();
            #endregion

            #region Light
            GUILayout.Label("Light", EditorStyles.boldLabel);

            addLight = EditorGUILayout.Toggle(new GUIContent("Add Light", "Add A Light Component To Instantiated Objects"), addLight);
            EditorGUI.indentLevel--; EditorGUILayout.LabelField("", GUI.skin.horizontalSlider); EditorGUI.indentLevel++;
            EditorGUILayout.Space();
            EditorGUILayout.BeginVertical();

            if (addLight && advancedMenu)
            {
                lightType = (LightTypeEnum)EditorGUILayout.EnumPopup(new GUIContent("Type", "Select Which Type Of Light You Would Like"), lightType);
                EditorGUILayout.Space();
                lightRange = EditorGUILayout.Slider(new GUIContent("Range", "The Range The Light Will Be Projected"), lightRange, 0f, 50f);
                lightColour = EditorGUILayout.ColorField(new GUIContent("Colour", "Colour Of The Light"), lightColour);
                EditorGUILayout.Space();
                lightMode = (LightModeEnum)EditorGUILayout.EnumPopup(new GUIContent("Mode", "Used To Determine How The Light Will Be Rendered"), lightMode);
                lightIntensity = EditorGUILayout.Slider(new GUIContent("Intensity", "Intensity Level Of The Light"), lightIntensity, 0f, 50f);
                EditorGUILayout.Space();
                lightIndirectMultiplier = EditorGUILayout.Slider(new GUIContent("Indirect Multiplier", "Controls The Intensity Of Indirect Light In The Scene"), lightIndirectMultiplier, 0f, 50f);
                lightShadow = (LightShadowTypeEnum)EditorGUILayout.EnumPopup(new GUIContent("Shadow Type", "Specifies What Shadows Will Be Cast By The Light"), lightShadow);
                EditorGUILayout.Space();
                lightFlare = EditorGUILayout.ObjectField("Flare", lightFlare, typeof(Flare), false) as Flare;
                EditorGUILayout.Space();
            }
            EditorGUILayout.EndVertical();
            #endregion Light

            EditorGUILayout.Space();
            EditorGUILayout.Space();

            #endregion Add Components

            #region Update Window
            if (CustomEditorWindow.focusedWindow || CustomEditorWindow.mouseOverWindow)
            {
                Repaint();
            }
            #endregion Update Window
            

        }

        #endregion Object Null Check
        GUILayout.EndScrollView(); //ends scrollbar

        #endregion Scrollable Interface

        #region Object Spawner Function
        void SpawnObject()
        {
            if (objToSpawn == null)
            {
                Debug.LogWarning("Warning: Object Must Be Assigned Before Instantiating");
                return;
            }
            

            #region Component Instantiation Loop
            for (int i = 0; i < amountOfObjToSpawn; i++)
            {
                Vector2 spawnCircle = Random.insideUnitCircle * spawnRadius;
                Vector3 spawnPos = new Vector3(spawnCircle.x, 0f, spawnCircle.y);

                GameObject _newObject = Instantiate(objToSpawn, spawnPos, Quaternion.identity);
                _newObject.name = objBaseName + objID;
                _newObject.transform.localScale = Vector3.one * objScale;

                #region Add Box Collider
                if (addBoxCollider)
                {
                    _newObject.AddComponent<BoxCollider>();
                    _newObject.GetComponent<BoxCollider>().sharedMaterial = boxPhysicsMat;
                    _newObject.GetComponent<BoxCollider>().center = boxCenter;
                    _newObject.GetComponent<BoxCollider>().size = boxSize;
                    if (isBoxTrigger)
                    {
                        _newObject.GetComponent<BoxCollider>().isTrigger = true;
                    }
                }
                #endregion Add Box Collider

                #region Add Sphere Collider
                if (addSphereCollider)
                {
                    _newObject.AddComponent<SphereCollider>();
                    _newObject.GetComponent<SphereCollider>().sharedMaterial = spherePhysMat;
                    _newObject.GetComponent<SphereCollider>().center = sphereCenter;
                    _newObject.GetComponent<SphereCollider>().radius = sphereRadius;
                    if (isSphereTrigger)
                    {
                        _newObject.GetComponent<SphereCollider>().isTrigger = true;
                    }

                }
                #endregion Add Sphere Collider

                #region Add Capsule Collider
                if (addCapsuleCollider)
                {
                    _newObject.AddComponent<CapsuleCollider>();

                    _newObject.GetComponent<CapsuleCollider>().sharedMaterial = capsulePhysMat;
                    _newObject.GetComponent<CapsuleCollider>().center = capsuleCenter;
                    _newObject.GetComponent<CapsuleCollider>().radius = capsuleRadius;
                    _newObject.GetComponent<CapsuleCollider>().height = capsuleHeight;
                    _newObject.GetComponent<CapsuleCollider>().direction = capsuleDirection;
                    if (isCapsuleTrigger)
                    {
                        _newObject.GetComponent<CapsuleCollider>().isTrigger = true;
                    }
                }
                #endregion Add Capsule Collider

                #region Add RigidBody
                if (addRigidBody)
                {
                    if (_newObject.GetComponent<Rigidbody>() == null)
                    {
                        _newObject.AddComponent<Rigidbody>();
                        _newObject.GetComponent<Rigidbody>().mass = rbMass;
                        _newObject.GetComponent<Rigidbody>().drag = rbDrag;
                        _newObject.GetComponent<Rigidbody>().angularDrag = rbAngularDrag;
                        _newObject.GetComponent<Rigidbody>().useGravity = useGravity;
                        _newObject.GetComponent<Rigidbody>().isKinematic = isKinematic;
                        #region rbInterpolate
                        switch (rbInterpolate)
                        {
                            case RigidBodyInterpolate.None:
                                _newObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.None;
                                break;
                            case RigidBodyInterpolate.Interpolate:
                                _newObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Interpolate;
                                break;
                            case RigidBodyInterpolate.Extrapolate:
                                _newObject.GetComponent<Rigidbody>().interpolation = RigidbodyInterpolation.Extrapolate;
                                break;
                            default:
                                break;
                        }
                        #endregion rbInterpolate 

                        #region rbCollisionType
                        switch (rbCollisionType)
                        {
                            case RigidBodyCollsion.Discrete:
                                _newObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Discrete;  // Pretty much voids my collision enum butttt okay :/
                                break;
                            case RigidBodyCollsion.Continuous:
                                _newObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.Continuous;
                                break;
                            case RigidBodyCollsion.ContinuousDynamic:
                                _newObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousDynamic;
                                break;
                            case RigidBodyCollsion.ContinuousSpeculative:
                                _newObject.GetComponent<Rigidbody>().collisionDetectionMode = CollisionDetectionMode.ContinuousSpeculative;
                                break;
                            default:
                                break;
                        }
                        #endregion rbCollisionType

                    }
                    else { Debug.LogError("Already A RigidBody Component Attached"); return; }
                }
                #endregion Add RigidBody

                #region Add Navmesh Agent
                if (addNavMeshAgent)
                {
                    if (_newObject.GetComponent<NavMeshAgent>() == null && _newObject.GetComponent<NavMeshObstacle>() == null)
                    {
                        _newObject.AddComponent<NavMeshAgent>();
                        _newObject.GetComponent<NavMeshAgent>().baseOffset = navBaseOffset;
                        _newObject.GetComponent<NavMeshAgent>().speed = navSpeed;
                        _newObject.GetComponent<NavMeshAgent>().angularSpeed = navAngSpeed;
                        _newObject.GetComponent<NavMeshAgent>().acceleration = navAcceleration;
                        _newObject.GetComponent<NavMeshAgent>().stoppingDistance = navStopDistance;
                        _newObject.GetComponent<NavMeshAgent>().autoBraking = navAutoBrake;
                        _newObject.GetComponent<NavMeshAgent>().radius = navAvoidRadius;
                        _newObject.GetComponent<NavMeshAgent>().height = navAvoidHeight;
                        _newObject.GetComponent<NavMeshAgent>().avoidancePriority = navAvoidPriority;
                        #region Avoidance Quality
                        switch (navAvoidQuality)
                        {
                            case NavAvoidQualityEnum.None:
                                _newObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.NoObstacleAvoidance;
                                break;
                            case NavAvoidQualityEnum.Low:
                                _newObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.LowQualityObstacleAvoidance;
                                break;
                            case NavAvoidQualityEnum.Medium:
                                _newObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.MedQualityObstacleAvoidance;
                                break;
                            case NavAvoidQualityEnum.Good:
                                _newObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.GoodQualityObstacleAvoidance;
                                break;
                            case NavAvoidQualityEnum.High:
                                _newObject.GetComponent<NavMeshAgent>().obstacleAvoidanceType = ObstacleAvoidanceType.HighQualityObstacleAvoidance;
                                break;
                            default:
                                break;
                        }
                        #endregion Avoidance Quality

                    }
                }
                #endregion

                #region Add Navmesh Obstacle
                if (addNavMeshObstacle)
                {
                    if (_newObject.GetComponent<NavMeshAgent>() == null && _newObject.GetComponent<NavMeshObstacle>() == null)
                    {
                        _newObject.AddComponent<NavMeshObstacle>();
                        _newObject.GetComponent<NavMeshObstacle>().center = obsCenter;
                        _newObject.GetComponent<NavMeshObstacle>().size = obsSize;
                        _newObject.GetComponent<NavMeshObstacle>().carving = obsCarve;
                        _newObject.GetComponent<NavMeshObstacle>().carvingMoveThreshold = obsMoveThreshhold;
                        _newObject.GetComponent<NavMeshObstacle>().carvingTimeToStationary = obsTimeToStationary;
                        _newObject.GetComponent<NavMeshObstacle>().carveOnlyStationary = obsCarveStationary;

                        #region Shape Enum
                        switch (obsShape)
                        {
                            case ObsShapeEnum.Capsule:
                                _newObject.GetComponent<NavMeshObstacle>().shape = NavMeshObstacleShape.Capsule;
                                break;
                            case ObsShapeEnum.Box:
                                _newObject.GetComponent<NavMeshObstacle>().shape = NavMeshObstacleShape.Box;
                                break;
                            default:
                                break;
                        }
                        #endregion Shape Enum              
                    }
                }
                #endregion Add NavMesh Obstacle

                #region Add Animator 
                if (addAnimator)
                {
                    if (_newObject.GetComponent<Animator>() == null)
                    {
                        _newObject.AddComponent<Animator>();
                        _newObject.GetComponent<Animator>().runtimeAnimatorController = animController;
                        _newObject.GetComponent<Animator>().avatar = animAvatar;
                        _newObject.GetComponent<Animator>().applyRootMotion = animRootMotion;
                        #region Aniamtor Enums
                        switch (animUpdateMode)
                        {
                            case AnimatorUpdateModeEnum.Normal:
                                _newObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.Normal;
                                break;
                            case AnimatorUpdateModeEnum.Animate:
                                _newObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.AnimatePhysics;
                                break;
                            case AnimatorUpdateModeEnum.Unscaled:
                                _newObject.GetComponent<Animator>().updateMode = AnimatorUpdateMode.UnscaledTime;
                                break;
                            default:
                                Debug.LogError("Warning: Failed To Define Animator Update Mode");
                                break;
                        }
                        switch (animCullMode)
                        {
                            case AnimatorCullingModeEnum.AlwaysAnimate:
                                _newObject.GetComponent<Animator>().cullingMode = AnimatorCullingMode.AlwaysAnimate;
                                break;
                            case AnimatorCullingModeEnum.CullUpdateTransforms:
                                _newObject.GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullUpdateTransforms;
                                break;
                            case AnimatorCullingModeEnum.CullCompletely:
                                _newObject.GetComponent<Animator>().cullingMode = AnimatorCullingMode.CullCompletely;
                                break;
                            default:
                                Debug.LogError("Warning: Failed To Define Animator Culling Mode");
                                break;
                        }
                        #endregion Animator Enums

                    }
                }
                #endregion Add Animator

                #region Add Light
                if (addLight)
                {
                    if (_newObject.GetComponent<Light>() == null)
                    {
                        _newObject.AddComponent<Light>();
                        _newObject.GetComponent<Light>().range = lightRange;
                        _newObject.GetComponent<Light>().color = lightColour;
                        _newObject.GetComponent<Light>().intensity = lightIntensity;
                        _newObject.GetComponent<Light>().bounceIntensity = lightIndirectMultiplier;
                        _newObject.GetComponent<Light>().flare = lightFlare;
                        switch (lightType)
                        {
                            case LightTypeEnum.Spot:
                                _newObject.GetComponent<Light>().type = LightType.Spot;
                                break;
                            case LightTypeEnum.Directional:
                                _newObject.GetComponent<Light>().type = LightType.Directional;
                                break;
                            case LightTypeEnum.Point:
                                _newObject.GetComponent<Light>().type = LightType.Point;
                                break;
                            case LightTypeEnum.Area:
                                _newObject.GetComponent<Light>().type = LightType.Area;
                                break;
                            default:
                                Debug.LogError("Warning: Failed To Define Light Type");
                                break;
                        }
                        switch (lightMode)
                        {
                            case LightModeEnum.Realtime:
                                _newObject.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Realtime;
                                break;
                            case LightModeEnum.Mixed:
                                _newObject.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Mixed;
                                break;
                            case LightModeEnum.Baked:
                                _newObject.GetComponent<Light>().lightmapBakeType = LightmapBakeType.Baked;
                                break;
                            default:
                                Debug.LogError("Warning: Failed To Define Lightmap Bake Type");
                                break;
                        }

                        switch (lightShadow)
                        {
                            case LightShadowTypeEnum.None:
                                _newObject.GetComponent<Light>().shadows = LightShadows.None;
                                break;
                            case LightShadowTypeEnum.Hard:
                                _newObject.GetComponent<Light>().shadows = LightShadows.Hard;
                                break;
                            case LightShadowTypeEnum.Soft:
                                _newObject.GetComponent<Light>().shadows = LightShadows.Soft;
                                break;
                            default:
                                Debug.LogError("Warning: Failed To Define Shadow Type");
                                break;
                        }

                    }
                }
                #endregion Add Light

                objID++; //Used to set Editor default ID +1
            }
            #endregion Component Instantiation Loop
        }
        #endregion Object Spawner Function
    }

}


//EditorWindow.GetWindow(typeof(BasicObjectSpawner)); IF THE EDITOR WINDOW IS OPEN AND THIS IS ANYWHERE IN YOUR CODE BESIDES SHOWWINDOW() THIS WILL BREAK YOUR SHIT

   