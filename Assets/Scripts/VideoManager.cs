using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.UI;

public class VideoManager : MonoBehaviour
{
    public VideoPlayer videoPlayer;
    public GameObject howTo;
    public GameObject scene;
    // Start is called before the first frame update
    void Start()
    {
        videoPlayer.url = System.IO.Path.Combine(Application.streamingAssetsPath, "intro.mp4");
        videoPlayer.Play();
        videoPlayer.loopPointReached += GetNext;
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void GetNext(VideoPlayer videoPlayer)
    {
        
        howTo.SetActive(true);
        scene.SetActive(false);
    }


}
