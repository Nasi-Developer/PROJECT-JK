using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

namespace JK
{
    public class VideoController : MonoBehaviour
    {
        public static VideoController Instance { get; private set; }
        public VideoPlayer videoPlayer;
        private float DelayTime = 20f;

        public System.Action ShowTouchStartText;

        private void Awake()
        {
            videoPlayer = GetComponent<VideoPlayer>();
            Instance = this;
        }


        private void Start()
        {
            videoPlayer.Play();
            StartCoroutine(PlayVideoWait());
        }

        IEnumerator PlayVideoWait()
        {
            yield return new WaitForSeconds(DelayTime);

            ShowTouchStartText?.Invoke();
        }
    }
}
