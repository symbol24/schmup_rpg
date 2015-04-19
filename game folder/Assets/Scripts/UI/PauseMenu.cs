using UnityEngine;
using System.Collections;

public class PauseMenu : Menu {
    public MenuController m_menuController;
    public GameManager m_gameManager;

    void Start()
    {
        m_menuController = FindObjectOfType<MenuController>();
        m_gameManager = FindObjectOfType<GameManager>();
    }


    public void ReturnToHub()
    {
        //return to hub
        Application.LoadLevel("Hub");
    }

    public void CloseApplication()
    {
        Application.Quit();
    }

    public void UnPause()
    {
        m_gameManager.UpdateGameState(GameManager.gameState.playing);
        m_menuController.HideMenu(this);
    }

}
