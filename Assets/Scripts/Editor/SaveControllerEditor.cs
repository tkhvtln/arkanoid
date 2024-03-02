using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(SaveController))]
public class SaveControllerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        if (GUILayout.Button("Reset save"))
            PlayerPrefs.DeleteAll();    
    }
}
