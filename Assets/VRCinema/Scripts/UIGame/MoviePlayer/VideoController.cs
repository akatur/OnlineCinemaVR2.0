using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;

public class VideoController : MonoBehaviour
{
    private VideoPlayer videoPlayer;

    void Start()
    {
        videoPlayer = GetComponent<VideoPlayer>();
    }

    void Update()
    {

        if (Input.GetKeyDown(KeyCode.F))
        {

            if (videoPlayer.isPlaying)
            {
                videoPlayer.Pause();
            }
            else
            {
                videoPlayer.Play();
            }
        }

        //if (Input.GetKeyDown(KeyCode.M))
        //{

        //    if (videoPlayer.isPlaying)
        //    {
        //        videoPlayer.Mute;
        //    }
        //    else
        //    {
        //        videoPlayer.Play();
        //    }
        //}
    }
}
