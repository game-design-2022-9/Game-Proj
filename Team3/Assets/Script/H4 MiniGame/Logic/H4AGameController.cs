using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class H4AGameController : MonoBehaviour
{
    public UnityEvent OnFinish;

    [Header("游戏数据")]

    public GameH4A_SO gamedata;

    public GameObject lineParent;

    public LineRenderer H4linePrefab;

    public H4ball H4ballPrefab;

    public Transform[] H4holderTransforms;

    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
    }


    private void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
    }


    public void Start()
    {
        DrawLine();
        CreateBall();
    }


    private void OnCheckGameStateEvent()
    {
        foreach (var H4ball in FindObjectsOfType<H4ball>())
        {
            if (!H4ball.isMatch)
                return;
        }

        Debug.Log("game over");
        OnFinish?.Invoke();
    }


    public void DrawLine()
    {
        foreach (var conections in gamedata.lineConections)
        {
            var line = Instantiate(H4linePrefab, lineParent.transform);
            line.SetPosition(0, H4holderTransforms[conections.from].position);
            line.SetPosition(1, H4holderTransforms[conections.to].position);


            //创建holder连接关系
            H4holderTransforms[conections.from].GetComponent<H4Holder>().linkH4Holders.Add(H4holderTransforms[conections.to].GetComponent<H4Holder>());
            H4holderTransforms[conections.to].GetComponent<H4Holder>().linkH4Holders.Add(H4holderTransforms[conections.from].GetComponent<H4Holder>());

        }

    }


    public void CreateBall()
    {
        for (int i = 0; i < gamedata.startBallOrder.Count; i++)
        {
            if (gamedata.startBallOrder[i] == H4BallName.None)
            {
                H4holderTransforms[i].GetComponent<H4Holder>().isEmpty = true;
                continue;
            }

            H4ball H4ball = Instantiate(H4ballPrefab, H4holderTransforms[i]);

            H4holderTransforms[i].GetComponent<H4Holder>().CheckH4Ball(H4ball);
            H4holderTransforms[i].GetComponent<H4Holder>().isEmpty = false;
            H4ball.SetupBall(gamedata.GetBallDetails(gamedata.startBallOrder[i]));
        }
    }


    //public void SetGameWeekData(int week)
    //{
    //    gamedata = gameDataArray[week];
    //    DrawLine();
    //    CreateBall();
    //}

}


