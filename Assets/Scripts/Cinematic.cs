// Examples of VideoPlayer function

using System;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Video;

public class Cinematic : MonoBehaviour
{
    public VideoClip videoClip;
    public int nextSceneId;

    private const string hasPlayedFirstTime = "HasPlayedFirstTime";

    void Start()
    {
        var hasPlayed = PlayerPrefs.GetInt(hasPlayedFirstTime, 0);

        if (hasPlayed == 0)
        {
            PlayVideo();
        }
        else
        {
            SceneManager.LoadScene(nextSceneId);
        }
    }

    private void PlayVideo()
    {

        // Will attach a VideoPlayer to the main camera.
        GameObject camera = GameObject.Find("Main Camera");

        // VideoPlayer automatically targets the camera backplane when it is added
        // to a camera object, no need to change videoPlayer.targetCamera.
        var videoPlayer = camera.AddComponent<UnityEngine.Video.VideoPlayer>();

        // Play on awake defaults to true. Set it to false to avoid the url set
        // below to auto-start playback since we're in Start().
        videoPlayer.playOnAwake = false;

        // By default, VideoPlayers added to a camera will use the far plane.
        // Let's target the near plane instead.
        videoPlayer.renderMode = UnityEngine.Video.VideoRenderMode.CameraNearPlane;

        //videoPlayer.targetCameraAlpha = 0.5F;

        videoPlayer.clip = videoClip;

        // Skip the first 100 frames.
        videoPlayer.frame = 100;

        // Restart from beginning when done.
        //videoPlayer.isLooping = true;

        // Each time we reach the end, we slow down the playback by a factor of 10.
        videoPlayer.loopPointReached += EndReached;

        // Start playback. This means the VideoPlayer may have to prepare (reserve
        // resources, pre-load a few frames, etc.). To better control the delays
        // associated with this preparation one can use videoPlayer.Prepare() along with
        // its prepareCompleted event.
        videoPlayer.Play();
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp)
    {
        PlayerPrefs.SetInt(hasPlayedFirstTime, 1);
        SceneManager.LoadScene(nextSceneId);
    }
}

