using System;
using SoundSystem.Scripts;
using UnityEngine;

namespace SoundSystem
{
    [Serializable]
    public class SfxReference
    {
        // Uses a custom editor to allow user to insert an AudioClip or a variant of SfxBase
        public bool UseConstant;
        public AudioClip Clip;
        public SfxBase Base;

        // Default constructor
        public SfxReference()
        {
            UseConstant = true;
            Clip = null;
        }

        // Play the clip
        public void Play()
        {
            if (NullTest()) return;
            if (UseConstant) {
                AudioHelper.PlayClip(new SfxProperties(Clip));
            } else {
                Base.Play();
            }
        }

        public void Play(AudioSourceController sourceController)
        {
            if (NullTest()) return;
            if (UseConstant) {
                sourceController.SetSourceProperties(Clip, 1, 1, true, 0);
                sourceController.Play();
            } else {
                Base.Play(sourceController);
            }
        }

        public void PlayAtPosition(Vector3 position)
        {
            if (NullTest()) return;
            if (UseConstant) {
                var sourceProperties = new SfxProperties(Clip);
                AudioHelper.PlayClip(sourceProperties.AddPosition(position));
            } else {
                Base.PlayAtPosition(position);
            }
        }

        public void PlayWithParent(Transform parent)
        {
            if (NullTest()) return;
            if (UseConstant) {
                var sourceProperties = new SfxProperties(Clip);
                AudioHelper.PlayClip(sourceProperties.AddParent(parent));
            } else {
                Base.PlayWithParent(parent);
            }
        }

        public SfxProperties GetSourceProperties()
        {
            if (NullTest()) return new SfxProperties(true);
            return UseConstant ? new SfxProperties(Clip) : Base.GetSourceProperties();
        }

        public bool NullTest()
        {
            if (UseConstant) {
                return Clip == null;
            }
            return Base == null;
        }

        public bool TestSame(SfxBase other)
        {
            if (!UseConstant) {
                return Base == other;
            }
            return false;
        }
    }
}