using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem.Scripts
{
    [RequireComponent(typeof(AudioSource))]
    public class AudioSourceController : MonoBehaviour
    {
        private Transform _parent;
        private AudioSource _source;
        private float _originalVolume;

        public bool Claimed { get; set; } = false;

        private AudioSource Source
        {
            get
            {
                if (_source == null) {
                    _source = GetComponent<AudioSource>();
                    if (_source == null) {
                        _source = gameObject.AddComponent<AudioSource>();
                    }
                }
                return _source;
            }
        }

        private void Awake()
        {
            _source = GetComponent<AudioSource>();
        }

        private void LateUpdate()
        {
            if (!Claimed) return;
            if (_source.isPlaying == false) {
                Stop();
            } else if (_parent != null) {
                transform.position = _parent.position;
            }
        }

        public void Reset()
        {
            transform.localPosition = Vector3.zero;
            _parent = null;
            Claimed = false;
            Source.outputAudioMixerGroup = SoundManager.Instance.SfxGroup;
        }

        public void SetSourceProperties(AudioClip clip, float volume, float pitch, bool loop, float spacialBlend)
        {
            var source = Source;
            source.clip = clip;
            source.volume = volume;
            source.pitch = pitch;
            source.loop = loop;
            source.spatialBlend = spacialBlend;

            _originalVolume = volume;
        }

        public void SetSourceProperties(SfxProperties properties)
        {
            var source = Source;
            source.clip = properties.Clip;
            source.volume = properties.Volume;
            source.pitch = properties.Pitch;
            source.loop = properties.Loop;
            source.spatialBlend = properties.SpatialBlend;

            _originalVolume = properties.Volume;
        }

        public void SetMixer(AudioMixerGroup mixerGroup)
        {
            Source.outputAudioMixerGroup = mixerGroup;
        }

        public void SetCustomVolume(float volume)
        {
            if (volume < 0) return;
            Source.volume = _originalVolume * volume;
        }

        public void SetPosition(Vector3 position)
        {
            transform.position = position;
        }

        public void SetParent(Transform parent)
        {
            _parent = parent;
        }

        public void Play()
        {
            var source = Source;
            if (source.isPlaying) source.Stop();
            source.Play();
        }

        public void Stop()
        {
            Source.Stop();
            Reset();
            SoundManager.Instance.ReturnController(this);
        }
    }
}