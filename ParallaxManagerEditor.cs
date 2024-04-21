//Author: Small Hedge Games
//Date: 21/04/2024

#if UNITY_EDITOR
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(ParallaxManager))]
public class ParallaxManagerEditor : Editor
{
    private void Awake()
    {
        if (!((ParallaxManager)target).gameCamera) ((ParallaxManager)target).gameCamera = Camera.main.transform;
    }

    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();
        ParallaxManager manager = (ParallaxManager)target;
        manager.GetBackgrounds();
        if (GUILayout.Button("Set Intensities")) manager.SetIntensities();
    }
}
#endif