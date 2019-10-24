using UnityEngine;
using UnityEditor;
using UnityEditor.SceneManagement;

[InitializeOnLoad]
public class AutosaveOnRun: ScriptableObject
{
	static AutosaveOnRun()
	{
		EditorApplication.playmodeStateChanged += () =>
		{
			if(EditorApplication.isPlayingOrWillChangePlaymode && !EditorApplication.isPlaying)
			{
				Debug.Log("Auto-Saving scene before entering Play mode: " + EditorSceneManager.GetActiveScene());

                EditorSceneManager.SaveOpenScenes();
				AssetDatabase.SaveAssets();
			}
		};
	}
}
