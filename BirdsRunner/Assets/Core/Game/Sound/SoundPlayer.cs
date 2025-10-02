using UnityEngine;
using System.Collections.Generic;

namespace Game.Sound
{
    public class SoundPlayer : MonoBehaviour
    {
        [SerializeField] private AudioSource audioSource;
        [SerializeField] private List<AudioClip> clips = new();
        [SerializeField] private float minPitch;
        [SerializeField] private float maxPitch;
        [SerializeField] private float minVolume;
        [SerializeField] private float maxVolume;

        public void PlaySound(bool ignorePlayingSound)
        {
            if (audioSource == null || clips.Count == 0) return;
            if (!ignorePlayingSound && audioSource.isPlaying) return;

            int index = Random.Range(0, clips.Count);
            float randPitch = Random.Range(minPitch, maxPitch);
            float randVolume = Random.Range(minVolume, maxVolume);
            AudioClip randomClip = clips[index];

            audioSource.clip = randomClip;
            audioSource.pitch = randPitch;
            audioSource.volume = randVolume;
            audioSource.Play();
        }

        public void StopSound()
        {
            audioSource.Stop();
        }

        public void SetMusic(AudioClip clip)
        {
            clips[0] = clip;
        }
    }
}
