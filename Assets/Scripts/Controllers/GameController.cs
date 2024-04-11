using System.Collections;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.SceneManagement;

public class GameController : MonoBehaviour
{
    public static GameController instance;

    public static UnityEvent OnGame = new UnityEvent();
    public static UnityEvent OnWin = new UnityEvent();

    public bool IsGame { get; private set; }
    public LevelController levelController { get; set; }

    public UIController uiController;
    public SaveController saveController;
    public SoundController soundController;
    public PlayerController playerController;

    private bool _isSceneLoaded;

    void Awake() 
    {
        if (instance == null)
            instance = this;
        else
            Destroy(this);
    }

    void Start()
    {
        saveController.Load();
        soundController.Init();
        uiController.Init();

        LoadCurrentLevel();
    }

    public void Game() 
    {
        IsGame = true;
        OnGame?.Invoke();
        uiController.ShowPanelGame();
        soundController.PlaySound(SoundName.CLICK);

        Cursor.visible = false;
    }

    public void Win()
    {
        IsGame = false;
        OnWin?.Invoke();
        uiController.ShowPanelWin();
        soundController.PlaySound(SoundName.WIN);

        Cursor.visible = true;
    }

    public void Defeat() 
    {
        IsGame = false;
        uiController.ShowPanelDefeat();
        soundController.PlaySound(SoundName.DEFEAT);

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

        ++saveController.data.level;
        saveController.Save();

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
     
        levelController.Init(saveController.data.level);
        playerController.Init();

        uiController.ShowPanelMenu(saveController.data.level);
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
