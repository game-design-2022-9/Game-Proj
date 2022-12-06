using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T4 : Interactive
{
    private Transform piece4;
    private BoxCollider2D coll;

    private void Awake()
    {
        piece4 = transform.GetChild(0);
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
            transform.GetChild(0).gameObject.SetActive(false);

        }
        else
        {
            coll.enabled = false;
        }
    }

    protected override void OnClickedAction()
    {
        transform.GetChild(0).gameObject.SetActive(true);

    }

}


