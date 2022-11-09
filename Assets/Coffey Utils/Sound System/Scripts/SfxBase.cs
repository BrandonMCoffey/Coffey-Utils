using UnityEngine;

namespace SoundSystem.Scripts
{
    public abstract class SfxBase : ScriptableObject
    {
        public void Play()
        {
            var sourceProperties = GetSourceProperties();
            AudioHelper.PlayClip(sourceProperties);
        }

        public void PlayAtPosition(Vector3 position)
        {
            var sourceProperties = GetSourceProperties();
            AudioHelper.PlayClip(sourceProperties.AddPosition(position));
        }

        public void PlayWithParent(Transform parent)
        {
            var sourceProperties = GetSourceProperties();
            AudioHelper.PlayClip(sourceProperties.AddParent(parent));
        }

        public void Play(AudioSourceController controller)
        {
            var sourceProperties = GetSourceProperties();
            controller.SetSourceProperties(sourceProperties);
            controller.Play();
        }

        public abstract SfxProperties GetSourceProperties();

        public void Play(AudioSource source)
        {
            var controller = source.gameObject.AddComponent<AudioSourceController>();
            Play(controller);
        }
    }
}