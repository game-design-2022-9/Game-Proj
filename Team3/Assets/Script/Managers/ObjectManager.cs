using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectManager : MonoBehaviour,iSaveable
{
    private Dictionary<ItemName, bool> itemAvailableDict = new Dictionary<ItemName, bool>();
    private Dictionary<string, bool> interactiveStateDict = new Dictionary<string, bool>();
    private void OnEnable()
    {
        EventHandler.BeforeSceneUnloatEvent += OnBeforeSceneUnloatEvent;
        EventHandler.AfterSceneLoadedEvent += OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;

    }

    private void OnDisable()
    {
        EventHandler.BeforeSceneUnloatEvent -= OnBeforeSceneUnloatEvent;
        EventHandler.AfterSceneLoadedEvent -= OnAfterSceneLoadedEvent;
        EventHandler.UpdateUIEvent += OnUpdateUIEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void Start()
    {
        iSaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnStartNewGameEvent(int obj)
    {
        itemAvailableDict.Clear();
        interactiveStateDict.Clear();
    }

    private void OnBeforeSceneUnloatEvent()
    {
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                interactiveStateDict[item.name] = item.isDone;
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    private void OnAfterSceneLoadedEvent()
    {
        //如果已经在字典中则更新显示状态，不在则添加
        foreach (var item in FindObjectsOfType<Item>())
        {
            if (!itemAvailableDict.ContainsKey(item.itemName))
                itemAvailableDict.Add(item.itemName, true);
            else
                item.gameObject.SetActive(itemAvailableDict[item.itemName]);
        }

        foreach (var item in FindObjectsOfType<Interactive>())
        {
            if (interactiveStateDict.ContainsKey(item.name))
                item.isDone = interactiveStateDict[item.name];
            else
                interactiveStateDict.Add(item.name, item.isDone);
        }
    }

    private void OnUpdateUIEvent(ItemDetails itemDetails , int arg2)
    {
        if (itemDetails != null)
        {
            itemAvailableDict[itemDetails.itemName] = false;
        }
    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.itemAvailableDict = this.itemAvailableDict;
        saveData.interactiveStateDict = this.interactiveStateDict;

        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.itemAvailableDict = saveData.itemAvailableDict;
        this.interactiveStateDict = saveData.interactiveStateDict;
    }
}
