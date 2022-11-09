using System.Collections;
using ObjectPoolSystem;
using UnityEngine;
using UnityEngine.Audio;

namespace SoundSystem.Scripts
{
    public class SoundManager : MonoBehaviour
    {
        [SerializeField] private bool _scenePersistent = false;
        [Header("Music Controller")]
        //[SerializeField] private AudioMixerGroup _musicGroup = null;
        [SerializeField] private MusicManager _musicSourceController;
        [Header("Audio Pool")]
        [SerializeField] private AudioMixerGroup _sfxGroup = null;
        [SerializeField] private Transform _poolParent;
        [SerializeField] private int _initialPoolSize = 5;

        private PoolManager<AudioSourceController> _poolManager = new PoolManager<AudioSourceController>();
        private static string _defaultManagerName = "Audio Manager";
        private static string _defaultMusicManagerName = "Music Manager";
        private static string _defaultMusicPlayerName = "Music Player";
        private static string _defaultSfxPoolName = "SFX Pool";
        private static string _defaultSfxPlayerName = "SFX Player";

        public AudioMixerGroup SfxGroup => _sfxGroup;

        #region Singleton

        private static SoundManager _instance;

        public static SoundManager Instance
        {
            get
            {
                if (_instance == null) {
                    _instance = FindObjectOfType<SoundManager>();
                    if (_instance == null) {
                        _instance = new GameObject(_defaultManagerName, typeof(SoundManager)).GetComponent<SoundManager>();
                    }
                }
                return _instance;
            }
        }

        private void Awake()
        {
            transform.SetParent(null);
            if (_instance == null) {
                if (_scenePersistent) {
                    DontDestroyOnLoad(gameObject);
                }
                _instance = this;
            } else if (_instance != this) {
                Destroy(gameObject);
            }

            #endregion

            // Create Music Manager
            if (_musicSourceController == null) {
                _musicSourceController = new GameObject(_defaultMusicManagerName, typeof(MusicManager)).GetComponent<MusicManager>();
                _musicSourceController.transform.SetParent(transform);
            }
            _musicSourceController.BuildMusicPlayers(_defaultMusicPlayerName);
            // Create SFX Pool
            if (_poolParent == null) {
                Transform pool = new GameObject(_defaultSfxPoolName).transform;
                pool.SetParent(transform);
                _poolParent = pool;
            }
            _poolManager.BuildInitialPool(_poolParent, _defaultSfxPlayerName, _initialPoolSize);
        }

        public void PlayMusic(SfxReference musicTrack, float fadeIn)
        {
        }

        public void PlayMusic(SfxVariant musicTrack, float fadeOut, bool crossFade, float fadeIn)
        {
        }

        private IEnumerator FadeIn(AudioSourceController sourceController, float fadeIn)
        {
            for (float t = 0; t < fadeIn; t += Time.deltaTime) {
                sourceController.SetCustomVolume(t / fadeIn);
                yield return null;
            }
            sourceController.SetCustomVolume(1);
        }

        public AudioSourceController GetController()
        {
            return _poolManager.GetObjectFromPool();
        }

        public void ReturnController(AudioSourceController sourceController)
        {
            _poolManager.PutObjectIntoPool(sourceController);
        }
    }
}