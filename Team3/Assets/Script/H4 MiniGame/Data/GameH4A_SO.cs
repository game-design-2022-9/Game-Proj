using System.Collections;
using System.Collections.Generic;
using UnityEngine;



[CreateAssetMenu(fileName = "GameH4A_SO", menuName = "Mini Game Data/GameH4A_SO")]

public class GameH4A_SO : ScriptableObject
{

    [SceneName] public string gameName;

    [Header("球的名字和对应图片")]

    public List<H4BallDetails> H4ballDataList;


    [Header("游戏逻辑数据")]
    public List<H4Conections> lineConections;

    public List<H4BallName> startBallOrder;

    public H4BallDetails GetBallDetails(H4BallName H4ballName)
    {
        return H4ballDataList.Find(h => h.H4ballName == H4ballName);
    }

}



[System.Serializable]

public class H4BallDetails
{
     public H4BallName H4ballName;

     public Sprite wrongSprite;

     public Sprite rightSprite;
}

[System.Serializable]

public class H4Conections
{
    public int from;

    public int to;
}


