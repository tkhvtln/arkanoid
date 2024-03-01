using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController Instance;
    public static UnityEvent OnGame = new UnityEvent();

    public bool IsGame => _isGame;
    public LevelController ControllerLevel { get; set; }

    public UIController ControllerUI;
    public SaveController ControllerSave;
    public SoundController ControllerSound;
    public PlayerController ControllerPlayer;

    private bool _isGame;
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
        _isGame = true;
        OnGame?.Invoke();
        ControllerUI.ShowPanelGame();
        ControllerSound.PlaySound(SoundName.CLICK);

        Cursor.visible = false;
    }

    public void Win()
    {
        _isGame = false;
        ControllerUI.ShowPanelWin();
        ControllerSound.PlaySound(SoundName.WIN);

        Cursor.visible = true;
    }

    public void Defeat() 
    {
        _isGame = false;
        ControllerUI.ShowPanelDefeat();
        ControllerSound.PlaySound(SoundName.DEFEAT);

        Cursor.visible = true;
    }

    public void LoadCurrentLevel() 
    {
        UnloadScene();
        LoadScene();
    }

    public void LoadNextLevel() 
    {
        UnloadScene();

        ControllerSave.DataPlayer.Level = ++ControllerSave.DataPlayer.Level >= SceneManager.sceneCountInBuildSettings ? 1 : ControllerSave.DataPlayer.Level;
        ControllerSave.Save();

        LoadScene();
    }

    private void LoadScene()
    {
        if (!_isSceneLoaded)
        {
            _isSceneLoaded = true;
            SceneManager.LoadSceneAsync(ControllerSave.DataPlayer.Level, LoadSceneMode.Additive);
        }

        ControllerPlayer.Init();
        ControllerUI.ShowPanelMenu();
    }

    private void UnloadScene()
    {
        if (_isSceneLoaded)
        {
            _isSceneLoaded = false;
            SceneManager.UnloadSceneAsync(ControllerSave.DataPlayer.Level);
        }
    }
}
