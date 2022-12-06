using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using DG.Tweening;


public class Clock : Interactive
{
    private Transform ClockSprite1;
    private Transform ClockSprite2;
    private Transform PointSprite;

    private void Awake()
    {
        ClockSprite1 = transform.GetChild(0);
        ClockSprite2 = transform.GetChild(1);
        PointSprite = transform.GetChild(2);
    }

    public override void EmptyClicked()
    {

        ClockSprite1.RotateAround(PointSprite.transform.position,Vector3.up,20*Time.deltaTime);
        ClockSprite2.RotateAround(PointSprite.transform.position, Vector3.up, 20 * Time.deltaTime);

       

    }
}