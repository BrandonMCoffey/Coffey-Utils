using UnityEditor;
using UnityEngine;

namespace SoundSystem.Scripts.Editor
{
    [CustomEditor(typeof(SfxBase), true)]
    public class SfxBaseEditor : UnityEditor.Editor
    {
        private AudioSourceController _audioSourcePreview;

        public void OnEnable()
        {
            _audioSourcePreview = EditorUtility.CreateGameObjectWithHideFlags("Audio Clip Preview", HideFlags.HideAndDontSave, typeof(AudioSourceController)).GetComponent<AudioSourceController>();
        }

        public void OnDisable()
        {
            DestroyImmediate(_audioSourcePreview.gameObject);
        }

        public override void OnInspectorGUI()
        {
            DrawDefaultInspector();

            if (GUILayout.Button("Preview Sfx")) {
                ((SfxBase)target).Play(_audioSourcePreview);
            }
        }
    }
}