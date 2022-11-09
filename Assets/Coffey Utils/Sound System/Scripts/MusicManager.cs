using UnityEngine;

namespace SoundSystem.Scripts
{
    public class MusicManager : MonoBehaviour
    {
        private AudioSource _source1;
        private AudioSource _source2;

        public void BuildMusicPlayers(string playerName)
        {
            _source1 = new GameObject(playerName, typeof(AudioSource)).GetComponent<AudioSource>();
            _source2 = new GameObject(playerName, typeof(AudioSource)).GetComponent<AudioSource>();
        }
    }
}