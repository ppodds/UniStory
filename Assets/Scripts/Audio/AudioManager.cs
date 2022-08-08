using UnityEngine;

namespace Audio
{
    public class AudioManager : MonoBehaviour
    {
        private AudioSource _audioSource;

        private void Awake()
        {
            _audioSource = GetComponent<AudioSource>();
        }

        public void PlayBGM(AudioClip bgm)
        {
            _audioSource.clip = bgm;
            _audioSource.Play();
        }
    }
}
