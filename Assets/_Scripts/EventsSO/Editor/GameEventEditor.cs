using UnityEditor;
using UnityEngine;

#if UNITY_EDITOR
[CustomEditor(typeof(GameEvent))]
public class GameEventEditor : Editor
{
    public override void OnInspectorGUI()
    {
        base.OnInspectorGUI();

        GUI.enabled = Application.isPlaying;

        GameEvent gameEvent = target as GameEvent;

        if (GUILayout.Button("Raise"))
        {
            gameEvent.Raise();
        }
    }
}
#endif