using System.Collections;
using System.Collections.Generic;
using UnityEngine;


//这里放想储存的所有东西 往里加就完事了
public class GameSaveData 
{
    public int gameWeek;

    public string currentScene;

    public Dictionary<string, bool> miniGameStateDict;

    public Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();

    public Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();

    public List<ItemName> itemList;

}
