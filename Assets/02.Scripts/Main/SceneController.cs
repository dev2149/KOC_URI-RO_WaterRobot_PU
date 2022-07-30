using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using UnityEngine.Video;

public class SceneController : MonoBehaviour
{
    public RawImage mScreen = null;
    public VideoPlayer mVideoPlayer = null;
    private AudioSource music_play;
    public AudioClip clip_item;
    private void Start()
    {
        ChildLoad();
    }
    void ChildLoad()
    {
        mScreen = GameObject.Find("Canvas").transform.Find("RawImage").GetComponent<RawImage>();
        mVideoPlayer = GameObject.Find("Canvas").transform.Find("RawImage").GetComponent<VideoPlayer>();
        music_play = GetComponent<AudioSource>();
        play_sound(clip_item, music_play);
        if (mScreen != null && mVideoPlayer != null)
        {
            StartCoroutine(PrepareVideo());
        }
    }
    protected IEnumerator PrepareVideo()
    {
        mVideoPlayer.Prepare();
        while (!mVideoPlayer.isPrepared)
        {
            yield return new WaitForSeconds(0.5f);
        }
        mScreen.texture = mVideoPlayer.texture;
    }

    public void PlayVideo()
    {
        if (mVideoPlayer != null && mVideoPlayer.isPrepared)
        {
            mVideoPlayer.Play();
        }
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            mVideoPlayer.Stop();
            SceneManager.LoadScene(1);
        }
    }
    public void play_sound(AudioClip _clip, AudioSource _music_play)
    {
        _music_play.Stop();
        _music_play.clip = _clip;
        _music_play.time = 0.0f;
        _music_play.Play();
    }
}
