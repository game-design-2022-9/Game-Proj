using System.Collections;
using System.Collections.Generic;
using UnityEngine;


/// <summary>
/// 这是一个来制作从哪个场景到哪个场景的方法
/// </summary>
public class Teleport : MonoBehaviour
{
    [SceneName]public string sceneFrom;

    public AudioSource audioSource;

    [SceneName]public string sceneToGO;

    public void TeleportToScene()
    {
        audioSource.Play();
        TransitionManager.Instance.Transtion(sceneFrom, sceneToGO);
    }
}
