using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class FinalVIdeoPlay : MonoBehaviour
{
    public GameObject rawImage;
    public RenderTexture rend;

    public UnityEngine.Video.VideoClip finalVideoClip;
    public UnityEngine.Video.VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        StartCoroutine(SkipVideo());
        videoPlayer=GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.targetTexture=rend;
        videoPlayer.clip=finalVideoClip;
        videoPlayer.isLooping=false;
        videoPlayer.loopPointReached+=EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp){
        SceneManager.LoadScene(0);
    }

    IEnumerator SkipVideo(){
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            yield return null;
        }
        videoPlayer.Pause();
        SceneManager.LoadScene(0);
    }
}
