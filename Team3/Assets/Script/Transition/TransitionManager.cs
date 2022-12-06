using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class TransitionManager : Singleton<TransitionManager>,iSaveable
{

    [SceneName] public string startScene;

    public CanvasGroup fadeCanvasGroup;

    public float fadeDuration;

    private bool isFade;

    private bool canTransition;

    private void OnEnable()
    {
        EventHandler.GameStateChangeEvent += OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent += OnStartNewGameEvent;
    }

    private void OnDisable()
    {
        EventHandler.GameStateChangeEvent -= OnGameStateChangeEvent;
        EventHandler.StartNewGameEvent -= OnStartNewGameEvent;
    }

    private void OnStartNewGameEvent(int obj)
    {
        StartCoroutine(TranstionToScene("Menu", startScene));
    }


    private void Start()
    {
        iSaveable saveable = this;
        saveable.SaveableRegister();
    }

    private void OnGameStateChangeEvent(GameState gameState)
    {
        canTransition = gameState == GameState.GamePlay;
    }

    public void Transtion(string from, string to)
    {
        if (!isFade && canTransition)
        {
            StartCoroutine(TranstionToScene(from, to));
        }
    }


    private IEnumerator TranstionToScene(string from, string to)
    {


        yield return Fade(1);
        if (from != string.Empty)
        {
            EventHandler.CallBeforeSceneUnloadEvent();

            yield return SceneManager.UnloadSceneAsync(from);
        }
        yield return SceneManager.LoadSceneAsync(to,LoadSceneMode.Additive);

        //设置新场景为激活场景
        Scene newScene = SceneManager.GetSceneAt(SceneManager.sceneCount - 1);
        SceneManager.SetActiveScene(newScene);


        EventHandler.CallAfterSceneLoadedEvent();
        yield return Fade(0);
    } 


    /// <summary>
    /// 淡入淡出场景
    /// </summary>
    /// <param name="targetAlpha">1是黑 0是透明</param>
    /// <returns></returns>
    private IEnumerator Fade(float targetAlpha)
    {
        isFade = true;

        fadeCanvasGroup.blocksRaycasts = true;

        float speed = Mathf.Abs(fadeCanvasGroup.alpha - targetAlpha) / fadeDuration;

        while (!Mathf.Approximately(fadeCanvasGroup.alpha, targetAlpha))
        {
            fadeCanvasGroup.alpha +=  speed * Time.deltaTime;
            print("alpha "+fadeCanvasGroup.alpha);
            fadeCanvasGroup.alpha=Mathf.Clamp(fadeCanvasGroup.alpha, fadeCanvasGroup.alpha,targetAlpha);
            yield return null;

        }
        fadeCanvasGroup.blocksRaycasts = false;

        isFade = false;

    }

    public GameSaveData GenerateSaveData()
    {
        GameSaveData saveData = new GameSaveData();
        saveData.currentScene = SceneManager.GetActiveScene().name;
        return saveData;
    }

    public void RestoreGameData(GameSaveData saveData)
    {
        Transtion("Menu", saveData.currentScene);
    }
}
