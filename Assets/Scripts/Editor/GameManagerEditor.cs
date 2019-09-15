using UnityEditor;
using UnityEngine;

[CustomEditor( typeof( GameManager ) )]
public class GameManagerEditor : Editor
{
    public override void OnInspectorGUI()
    {
        DrawDefaultInspector();
        GameManager gm = (GameManager) target;
        if (GUILayout.Button( "Force start game" )) {
            GameManager.StartGame();
        }
        if (GUILayout.Button( "Switch inputs" )) {
            GameManager.SwitchInput( 1 );
            GameManager.SwitchInput( 2 );
        }
    }
}