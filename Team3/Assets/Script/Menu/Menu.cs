using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.EventSystems;
using UnityEngine.Video;

public class Menu : MonoBehaviour
{
    public void QuitGame()
    {
        Application.Quit();
    }

    public void ContinueGame()
    {
        //加载游戏进度
        SaveLoadManager.Instance.Load();
    }

    public void GoBackToMenu()
    {

        var currentScene = SceneManager.GetActiveScene().name;
        TransitionManager.Instance.Transtion(currentScene, "Menu");
        //保存游戏进度
        SaveLoadManager.Instance.Save();
    }


    public void StartGameWeek(int gameWeek)
    {
        EventHandler.CallStartNewGameEvent(gameWeek);
    }
}
