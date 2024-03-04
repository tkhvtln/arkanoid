using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static UnityEvent OnGame = new UnityEvent();
    public static UnityEvent OnWin = new UnityEvent();

    public bool IsGame { get; private set; }
    public LevelController ControllerLevel { get; set; }

    public UIController ControllerUI;
    public SaveController ControllerSave;
    public SoundController ControllerSound;
    public PlayerController ControllerPlayer;

    private bool _isSceneLoaded;

    void Awake() 
    {
        if (Instance == null)
            Instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        ControllerSave.Load();
        ControllerSound.Init();
        ControllerUI.Init();

        LoadCurrentLevel();
    }

    public void Game() 
    {
        IsGame = true;
        OnGame?.Invoke();
        ControllerUI.ShowPanelGame();
        ControllerSound.PlaySound(SoundName.CLICK);

        Cursor.visible = false;
    }

    public void Win()
    {
        IsGame = false;
        OnWin?.Invoke();
        ControllerUI.ShowPanelWin();
        ControllerSound.PlaySound(SoundName.WIN);

        Cursor.visible = true;
    }

    public void Defeat() 
    {
        IsGame = false;
        ControllerUI.ShowPanelDefeat();
        ControllerSound.PlaySound(SoundName.DEFEAT);

        Cursor.visible = true;
    }

    public void LoadCurrentLevel() 
    {
        UnloadScene();
        StartCoroutine(LoadScene());
    }

    public void LoadNextLevel() 
    {
        UnloadScene();

        ++ControllerSave.DataPlayer.Level;
        ControllerSave.Save();

        StartCoroutine(LoadScene());
    }

    private IEnumerator LoadScene()
    {
        if (!_isSceneLoaded)
        {
            _isSceneLoaded = true;

            AsyncOperation operation = SceneManager.LoadSceneAsync(1, LoadSceneMode.Additive);
            while (!operation.isDone) yield return null;
        }
     
        ControllerLevel.Init(ControllerSave.DataPlayer.Level);
        ControllerPlayer.Init();

        ControllerUI.ShowPanelMenu(ControllerSave.DataPlayer.Level);
    }

    private void UnloadScene()
    {
        if (_isSceneLoaded)
        {
            _isSceneLoaded = false;
            SceneManager.UnloadSceneAsync(1);
        }
    }
}
