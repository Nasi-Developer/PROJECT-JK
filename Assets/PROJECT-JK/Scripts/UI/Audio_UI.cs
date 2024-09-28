using System.Collections;
using System.Collections.Generic;
using UnityEngine;

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
        Title_UI.Instance.PlayStartSound += PlayStartSound;
    }

    public void PlayStartSound()
    {
        int index = Random.Range(0, audioClips.Length);

        AudioSource.clip = audioClips[index];
        AudioSource.Play();
    }
}
