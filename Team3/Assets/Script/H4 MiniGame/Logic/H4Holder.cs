using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class H4Holder : Interactive
{

    public H4BallName matchH4Ball;

    public AudioSource audioSource;

    public H4ball currentH4Ball;

    public HashSet<H4Holder> linkH4Holders = new HashSet<H4Holder>();

    public bool isEmpty;

    public void CheckH4Ball(H4ball H4ball)
    {
        currentH4Ball = H4ball;
        if (H4ball.H4ballDeatils.H4ballName == matchH4Ball)
        {
            currentH4Ball.isMatch = true;
            currentH4Ball.SetRight();
        }
        else
        {
            currentH4Ball.isMatch = false;
            currentH4Ball.SetWrong();
        }
    }

    public override void EmptyClicked()
    {
        foreach (var H4holder in linkH4Holders)
        {
            if (H4holder.isEmpty)
            {
                //移动球
                audioSource.Play();
                currentH4Ball.transform.position = H4holder.transform.position;
                currentH4Ball.transform.SetParent(H4holder.transform);

                //交换球
                H4holder.CheckH4Ball(currentH4Ball);
                this.currentH4Ball = null;

                //改变状态
                this.isEmpty = true;
                H4holder.isEmpty = false;


                EventHandler.CallCheckGameStateEvent();
            }
        }

    }
}
