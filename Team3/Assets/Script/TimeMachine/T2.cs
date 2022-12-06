using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class T2 : Interactive
{
    private Transform piece2;
    private BoxCollider2D coll;

    private void Awake()
    {
        piece2 = transform.GetChild(0);
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
