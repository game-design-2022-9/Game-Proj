using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Video;
using UnityEngine.SceneManagement;

public class checkAndDestroy : MonoBehaviour
{
    // Start is called before the first frame update
    public VideoPlayer v;
    public GameObject video;

    void Start()
    {
        v.loopPointReached += CheckOver;
    }

    public void CheckOver(VideoPlayer source)
    {
        //SceneManager.LoadScene("Menu");
        Destroy(gameObject);
        //v.targetTexture.Release();
        //VideoPlayer.targetTexture.Release();
    }
}