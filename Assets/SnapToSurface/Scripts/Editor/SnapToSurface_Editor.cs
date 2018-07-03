using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(SnapToSurface))]
public class SnapToSurface_Editor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        SnapToSurface myScripts = (SnapToSurface)target;
        if (GUILayout.Button("Snap To Surface"))
        {
            myScripts.Snap();
        }
    }
}
