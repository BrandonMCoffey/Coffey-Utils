using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem.Scripts
{
    public struct SfxProperties
    {
        public bool Null;
        public AudioClip Clip;
        public bool Loop;

        public float Volume;
        public float Pitch;

        public float StereoPan;
        public float SpatialBlend;
        public float ReverbZoneMix;

        public AudioMixerGroup MixerGroup;

        public Vector3 Position;
        public Transform Parent;

        public SfxProperties AddProperties(SfxProperties other)
        {
            // Set Clip
            if (Clip == null) Clip = other.Clip;
            if (Clip != null) Null = false;
            // Set Loop
            Loop = Loop || other.Loop;
            // Set Position and Parent
            Volume *= other.Volume;
            Pitch *= other.Pitch;
            // Set Stereo Pan, Spatial Blend, and Reverb Mix
            StereoPan += other.StereoPan;
            if (SpatialBlend == 0) SpatialBlend = other.SpatialBlend;
            ReverbZoneMix *= other.ReverbZoneMix;
            // Set Position and Parent
            if (Position == Vector3.zero) Position = other.Position;
            if (Parent == null) Parent = other.Parent;
            // Return this struct
            return this;
        }

        public SfxProperties AddPosition(Vector3 position)
        {
            Position = position;
            return this;
        }

        public SfxProperties AddParent(Transform parent)
        {
            Parent = parent;
            return this;
        }

        public SfxProperties(bool invalid)
        {
            Null = invalid;
            Clip = null;
            Loop = false;
            Volume = 1;
            Pitch = 1;
            StereoPan = 0;
            SpatialBlend = 0;
            ReverbZoneMix = 1;
            MixerGroup = null;
            Position = default;
            Parent = null;
        }

        public SfxProperties(AudioClip clip)
        {
            if (clip == null) {
                Null = true;
                Clip = null;
            } else {
                Null = false;
                Clip = clip;
            }
            Loop = false;
            Volume = 1;
            Pitch = 1;
            StereoPan = 0;
            SpatialBlend = 0;
            ReverbZoneMix = 1;
            MixerGroup = null;
            Position = default;
            Parent = null;
        }

        public SfxProperties(bool loop, float volume, float pitch, float stereoPan, float spatialBlend, float reverbMix, AudioMixerGroup mixerGroup)
        {
            Null = true;
            Clip = null;
            Loop = loop;
            Volume = volume;
            Pitch = pitch;
            StereoPan = stereoPan;
            SpatialBlend = spatialBlend;
            ReverbZoneMix = reverbMix;
            MixerGroup = mixerGroup;
            Position = default;
            Parent = null;
        }

        public void Print()
        {
            string first = "Sfx: " + (Clip == null ? "Null" : Clip.name) + (Null ? " is null. " : ". ") + (Loop ? " Looping. " : "");
            string second = "Volume: " + Volume.ToString("F2") + ". Pitch: " + Pitch.ToString("F2") + ". ";
            string third = (SpatialBlend == 0 ? "2D. " : "3D at " + SpatialBlend.ToString("F2") + ". ");
            string fourth = (Position != Vector3.zero ? "Position: " + Position : "") + (Parent != null ? "Parent: " + Parent : "");
            Debug.Log(first + second + third + fourth);
        }
    }
}