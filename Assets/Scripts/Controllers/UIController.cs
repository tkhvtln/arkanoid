using UnityEngine;

public class UIController : MonoBehaviour
{
    public PanelMenu panelMenu;
    public PanelGame panelGame;
    public PanelWin panelWin;
    public PanelDefeat panelDefeat;

    public void Init() 
    {
        panelMenu.Init();
        panelGame.Init();
        panelWin.Init();
        panelDefeat.Init();
    }

    public void ShowPanelMenu(int level) 
    {
        Clear();
        panelMenu.Show(level);
    }

    public void ShowPanelGame() 
    {
        Clear();
        panelGame.Show();
    }

    public void ShowPanelWin() 
    {
        Clear();
        panelWin.Show();
    }

    public void ShowPanelDefeat() 
    {
        Clear();
        panelDefeat.Show();
    }

    public void OnButtonPlay() 
    {
        GameController.instance.Game();
    }

    public void OnButtonNextLevel() 
    {
        GameController.instance.LoadNextLevel();
    }

    public void OnButtonRestartLevel() 
    {
        GameController.instance.LoadCurrentLevel();
    }

    public void OnButtonSound()
    {
        GameController.instance.soundController.SwitchSound();
    }

    public void Clear() 
    {
        panelMenu.Hide();
        panelGame.Hide();
        panelWin.Hide();
        panelDefeat.Hide();
    }
}
