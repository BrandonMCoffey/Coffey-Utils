using UnityEditor;
using UnityEngine;

namespace GameEventSystems.Logic
{
#if UNITY_EDITOR
    [CustomEditor(typeof(GameEvent), true)]
    public class EventEditor : Editor
    {
        public override void OnInspectorGUI()
        {
            base.OnInspectorGUI();

            GUI.enabled = Application.isPlaying;

            GameEvent e = target as GameEvent;
            if (e == null) return;
            if (GUILayout.Button("Invoke")) e.Invoke();
        }
    }
#endif
}