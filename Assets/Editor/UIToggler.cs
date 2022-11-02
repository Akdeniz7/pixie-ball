using UnityEditor;
using UnityEngine;

public class UIToggler
{
	private const int UI_LAYER = 1 << 5;

	[InitializeOnLoadMethod]
	private static void Init()
	{
#if UNITY_2019_1_OR_NEWER
		SceneView.duringSceneGui -= OnSceneGUI;
		SceneView.duringSceneGui += OnSceneGUI;
#else
		SceneView.onSceneGUIDelegate -= OnSceneGUI;
		SceneView.onSceneGUIDelegate += OnSceneGUI;
#endif
	}

	private static void OnSceneGUI(SceneView sceneView)
	{
		Handles.BeginGUI();

		bool uiVisible = (Tools.visibleLayers & UI_LAYER) == UI_LAYER;
		if (GUI.Button(new Rect(0f, 0f, 125f, 25f), uiVisible ? "Hide Canvas" : "Show Canvas"))
		{
			if (uiVisible)
				Tools.visibleLayers &= ~UI_LAYER;
			else
				Tools.visibleLayers |= UI_LAYER;
		}

		Handles.EndGUI();
	}
}