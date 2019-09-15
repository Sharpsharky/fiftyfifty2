using UnityEngine;
using UnityEditor;

[CustomEditor( typeof( Tower ) )]
public class PlatformFactoryInspector : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        Tower tower = (Tower) target;
        if (GUILayout.Button( "Re-create" )) {
            tower.RemoveSections();
            tower.Create();
        }
    }
}