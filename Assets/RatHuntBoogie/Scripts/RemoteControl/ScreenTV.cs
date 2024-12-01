using System;
using UnityEngine;
using UnityEngine.Video;

public class ScreenTV : MonoBehaviour {
    [SerializeField] private VideoClip[] videoClips;

    private MeshRenderer meshRenderer;
    private VideoPlayer videoPlayer;
    private AudioSource audioSource;

    private bool isOn = false;
    private int currVideoIndex = 0;
    private double[] videoTimes;

    public void Start() {
        meshRenderer = GetComponent<MeshRenderer>();
        videoPlayer = GetComponent<VideoPlayer>();
        audioSource = GetComponent<AudioSource>();

        videoPlayer.clip = videoClips[currVideoIndex];
        videoPlayer.prepareCompleted += OnVideoPrepared;

        videoTimes = new double[videoClips.Length];
        ResetVideoTimes();
    }

    public void OnOff() {
        isOn = !isOn;
        if (isOn) {
            currVideoIndex = 0;
            videoPlayer.clip = videoClips[currVideoIndex];
            VolumeOn();
            StartVideoPlayer();
        } else {
            StopVideoPlayer();
            ResetVideoTimes();
        }
    }

    public void PrevChannel() {
        if (!isOn) {
            return;
        }

        int lastCurrVideoIndex = currVideoIndex;
        currVideoIndex--;
        if (currVideoIndex == -1) {
            currVideoIndex = videoClips.Length - 1;
        }

        ChangeVideo(lastCurrVideoIndex);
    }

    public void NextChannel() {
        if (!isOn) {
            return;
        }

        int lastCurrVideoIndex = currVideoIndex;
        currVideoIndex++;
        if (currVideoIndex == videoClips.Length) {
            currVideoIndex = 0;
        }

        ChangeVideo(lastCurrVideoIndex);
    }

    public void VolumeOff() {
        if (!isOn) {
            return;
        }

        audioSource.volume = 0;
    }

    public void VolumeOn() {
        if (!isOn) {
            return;
        }

        audioSource.volume = 1;
    }

    private void ChangeVideo(int lastCurrVideoIndex) {
        videoTimes[lastCurrVideoIndex] = videoPlayer.time;
        StopVideoPlayer();
        videoPlayer.clip = videoClips[currVideoIndex];
        StartVideoPlayer();
    }

    private void StopVideoPlayer() {
        meshRenderer.material.color = Color.black;
        videoPlayer.Stop();
    }

    private void StartVideoPlayer() {
        videoPlayer.Prepare();
    }

    private void OnVideoPrepared(VideoPlayer src) {
        videoPlayer.time = videoTimes[currVideoIndex];
        audioSource.time = (float)videoTimes[currVideoIndex];

        videoPlayer.Play();
        meshRenderer.material.color = Color.white;
    }

    private void ResetVideoTimes() {
        for (int i = 0; i < videoTimes.Length; i++) {
            videoTimes[i] = 0;
        }
    }
}
