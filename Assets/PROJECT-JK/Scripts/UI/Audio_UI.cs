using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace JK
{

    public class Audio_UI : MonoBehaviour
    {
        public AudioClip[] audioClips;
        public AudioSource AudioSource;

        private void Awake()
        {
            AudioSource = GetComponent<AudioSource>();
        }

        private void Start()
        {
            if (Title_UI.Instance != null)
            {
                Title_UI.Instance.PlayStartSound += PlayAudio;
            }
        }

        public void PlayAudio()
        {
            int index = Random.Range(0, audioClips.Length);

            AudioSource.clip = audioClips[index];
            AudioSource.Play();
        }
    }
}
