using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class VideoPlay : MonoBehaviour
{
    public GameObject rawImage;
    public RawImage texture;
    public RenderTexture rend1;
    public RenderTexture rend2;
    public UnityEngine.Video.VideoClip intro;
    public UnityEngine.Video.VideoClip startGame;
    public UnityEngine.Video.VideoPlayer videoPlayer;
    // Start is called before the first frame update
    void Start()
    {
        texture= rawImage.GetComponent<RawImage>();
        texture.texture=rend1;
        videoPlayer=GetComponent<UnityEngine.Video.VideoPlayer>();
        videoPlayer.targetTexture=rend1;
        videoPlayer.clip=intro;
        videoPlayer.isLooping=false;
        videoPlayer.loopPointReached+=EndReached;
    }

    void EndReached(UnityEngine.Video.VideoPlayer vp){
        Debug.Log("END!!!");
        rawImage.SetActive(false);
    }

    public void StartGame(){
        StartCoroutine(SkipVideo());
        videoPlayer.targetTexture=rend2;
        rawImage.SetActive(true);
        texture.texture=rend2;
        //videoPlayer.targetTexture=rend2;
        videoPlayer.clip=startGame;
        videoPlayer.Play();
        videoPlayer.loopPointReached+=NextScene;

    }

    void NextScene(UnityEngine.Video.VideoPlayer vp){
        Debug.Log("NEXT!!");
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }

    IEnumerator SkipVideo(){
        while (!Input.GetKeyDown(KeyCode.Escape))
        {
            yield return null;
        }
        videoPlayer.Pause();
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex+1);
    }
}
