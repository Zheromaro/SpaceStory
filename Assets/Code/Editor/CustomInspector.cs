using SpaceGame.NotImportant;
using UnityEditor;
using UnityEngine;

[CustomEditor(typeof(Parallax))]
public class CustomInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();

        Parallax parallax = (Parallax)target;
        if(GUILayout.Button("Generate backgrounds"))
        {
            parallax.generateBackgroundsForInspector();
        }

        if (GUILayout.Button("Save backgrounds changes"))
        {
            parallax.saveBackgroundsNewStats();
        }
    }


}
