using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;



public class Rocket : Interactive
{
    private Transform rocket;
    private BoxCollider2D coll;
    public AudioSource audioSource;

    private void Awake()
    {
        rocket = transform.GetChild(0);
        coll = GetComponent<BoxCollider2D>();
    }

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
    }


    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
    }

    private void OnAfterSceneLoadedEvent()
    {
        if (!isDone)
        {
            transform.GetChild(0).gameObject.SetActive(true);

        }

    }
    protected override void OnClickedAction()
    {

        audioSource.Play();
        rocket.DOMoveY(15, 3, false);
        
        
    }




}