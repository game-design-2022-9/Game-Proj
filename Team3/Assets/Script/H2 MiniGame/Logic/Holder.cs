using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Holder : Interactive
{

    public BallName matchBall;

    public Ball currentBall;

    public HashSet<Holder> linkHolders = new HashSet<Holder>();

    public AudioSource audioSource1;


    public bool isEmpty;

    public void CheckBall(Ball ball)
    {
        currentBall = ball;
        if (ball.ballDetails.ballName == matchBall)
        {
            currentBall.isMatch = true;
            currentBall.SetRight();
        }
        else
        {
            currentBall.isMatch = false;
            currentBall.SetWrong();
        }
       
    }

    public override void EmptyClicked()
    {
        foreach (var holder in linkHolders)
        {
            if (holder.isEmpty)
            {
                //移动球
                audioSource1.Play();
                currentBall.transform.position = holder.transform.position;
                currentBall.transform.SetParent(holder.transform);

                //交换球
                holder.CheckBall(currentBall);
                this.currentBall = null;

                //改变状态
                this.isEmpty = true;
                holder.isEmpty = false;


                EventHandler.CallCheckGameStateEvent();
            }
        }

    }
}
