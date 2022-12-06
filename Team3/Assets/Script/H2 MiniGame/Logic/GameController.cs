using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameController : Singleton<GameController>
{
    public UnityEvent OnFinish;
    public AudioSource audioSource;

    [Header("游戏数据")]

    public GameH2A_SO gamedata;

    public GameH2A_SO[] gameDataArray;

    public LineRenderer linePrefab;

    public GameObject lineParent;

    public Ball ballPrefab;

    public Transform[] holderTransforms;

    private void OnEnable()
    {
        EventHandler.CheckGameStateEvent += OnCheckGameStateEvent;
    }

    private void OnDisable()
    {
        EventHandler.CheckGameStateEvent -= OnCheckGameStateEvent;
    }

   

    //public void Start()
    //{
    //    DrawLine();
    //    CreateBall();
    //}

    private void OnCheckGameStateEvent()
    {
        foreach (var ball in FindObjectsOfType<Ball>())
        {
            if (!ball.isMatch)
                return;
        }

        Debug.Log("游戏结束");
        foreach (var holder in holderTransforms)
        {
            holder.GetComponent<Collider2D>().enabled = false;
        }
        EventHandler.CallGamePassEvent(gamedata.gameName);
        OnFinish?.Invoke();
    }


    public void ResetGame()
    {
        audioSource.Play();
        for (int i = 0; i < lineParent.transform.childCount; i++)
        {
            Destroy(lineParent.transform.GetChild(i).gameObject);
        }
        foreach (var holder in holderTransforms)
        {
            if (holder.childCount > 0)
                Destroy(holder.GetChild(0).gameObject);
        }
        DrawLine();
        CreateBall();
    }

    public void DrawLine()
    {
        foreach (var conections in gamedata.lineConections)
        {
            var line = Instantiate(linePrefab, lineParent.transform);
            line.SetPosition(0, holderTransforms[conections.from].position);

            line.SetPosition(1, holderTransforms[conections.to].position);


            //创建holder连接关系
            holderTransforms[conections.from].GetComponent<Holder>().linkHolders.Add(holderTransforms[conections.to].GetComponent<Holder>());
            holderTransforms[conections.to].GetComponent<Holder>().linkHolders.Add(holderTransforms[conections.from].GetComponent<Holder>());

        }

    }


    public void CreateBall()
    {
        for (int i = 0; i < gamedata.startBallOrder.Count; i++)
        {
            if (gamedata.startBallOrder[i] == BallName.None)
            {
                holderTransforms[i].GetComponent<Holder>().isEmpty = true;
                continue;
            }

            Ball ball = Instantiate(ballPrefab, holderTransforms[i]);

            holderTransforms[i].GetComponent<Holder>().CheckBall(ball);
            holderTransforms[i].GetComponent<Holder>().isEmpty = false;
            ball.SetupBall(gamedata.GetBallDetails(gamedata.startBallOrder[i]));
        }
    }


    public void SetGameWeekData(int week)
    {
        gamedata = gameDataArray[week];
        DrawLine();
        CreateBall();
    }

}
