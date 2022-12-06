using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement; 

public class GameManager : MonoBehaviour,iSaveable
{
    private Dictionary<string, bool> miniGameStateDict = new Dictionary<string, bool>();

    private GameController currentGame;

    private int gameWeek;

    private void OnEnable()
    {
        EventHandler.AfterSceneLoadedEvent += onAfterSceneLoadedEvent;
        EventHandler.GamePassEvent += onGamePassEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }


    private void OnDisable()
    {
        EventHandler.AfterSceneLoadedEvent -= onAfterSceneLoadedEvent;
        EventHandler.GamePassEvent -= onGamePassEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int gameWeek)
    {
        this.gameWeek = gameWeek;
        miniGameStateDict.Clear();
    }

    void Start()
    {
        SceneManager.LoadScene("Menu", LoadSceneMode.Additive);
        EventHandler.CallGameStateChangeEvent(GameState.GamePlay);

        //保存数据
        iSaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void onAfterSceneLoadedEvent()
    {
        foreach (var miniGame in FindObjectsOfType<MiniGame>())
        {
            if (miniGameStateDict.TryGetValue(miniGame.gameName, out bool isPass))
            {
                miniGame.isPass = isPass;
                miniGame.UpdateMiniGameState();
            }
        }

        currentGame = FindObjectOfType<GameController>();

        currentGame?.SetGameWeekData(gameWeek);
    }


    private void onGamePassEvent(string gameName)
    {
        miniGameStateDict[gameName] = true;

    }


    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.gameWeek = this.gameWeek;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        this.gameWeek = saveData.gameWeek;
    }
}
