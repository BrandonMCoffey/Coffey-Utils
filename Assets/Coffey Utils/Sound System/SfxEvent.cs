using System.Collections.Generic;
using System.Linq;
using CustomFloats;
using SoundSystem.Scripts;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem
{
    [CreateAssetMenu]
    public class SfxEvent : SfxBase
    {
        [Header("Audio Clips")]
        [SerializeField] private List<SfxReference> _clips = new List<SfxReference>();

        [Header("Volume Settings")]
        [SerializeField] [MinMaxRange(0f, 1f)]
        private RangedFloat _volume = new RangedFloat(0.4f, 0.8f);
        [SerializeField] [MinMaxRange(0.25f, 3f)]
        private RangedFloat _pitch = new RangedFloat(0.8f, 1.2f);

        [Header("Spatial Settings")]
        [SerializeField] [MinMaxRange(-1f, 1f)]
        private RangedFloat _stereoPan = new RangedFloat(0f, 0f);
        [SerializeField] [MinMaxRange(0f, 1.1f)]
        private RangedFloat _reverbZoneMix = new RangedFloat(1f, 1f);

        [Header("Override Mixer")]
        [SerializeField] private AudioMixerGroup _mixerGroup = null;

        public override SfxProperties GetSourceProperties()
        {
            // Remove any clips that are null and remove the sfx reference if it is the same as this sfx event (prevent recursion)
            _clips = _clips.Where(clip => clip != null && !clip.NullTest() && !clip.TestSame(this)).ToList();

            // If there are no clips, return an empty reference
            if (_clips.Count == 0) {
                return new SfxProperties(true);
            }

            // Find Reference Source Properties
            var referenceSfx = _clips[Random.Range(0, _clips.Count)];
            var referenceProperties = referenceSfx.GetSourceProperties();

            // Create Current Source Properties
            var myProperties = new SfxProperties(false, _volume.GetRandom(), _pitch.GetRandom(), _stereoPan.GetRandom(), 0, _reverbZoneMix.GetRandom(), _mixerGroup);

            // Add properties together and return
            return referenceProperties.AddProperties(myProperties);
        }
    }
}