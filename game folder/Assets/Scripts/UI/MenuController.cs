using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {
    private GameManager m_GameManager;
    private EventSystem m_eventSystem;
    public Menu m_PauseMenu;
    private float delay = 0.5f;
    private float m_time = 0.0f;

    void Start(){
        m_GameManager = FindObjectOfType<GameManager>();
        m_eventSystem = FindObjectOfType<EventSystem>();
    }

    void Update(){
        if (m_GameManager.m_CurrentState == GameManager.gameState.playing && m_GameManager.m_pauseButton > 0 && Time.time >= m_time){
            ShowMenu(m_PauseMenu);
            m_GameManager.UpdateGameState(GameManager.gameState.paused);
            m_time = Time.time + delay;
        }
        else if (m_GameManager.m_CurrentState == GameManager.gameState.paused && m_GameManager.m_pauseButton > 0 && Time.time >= m_time)
        {
            HideMenu(m_PauseMenu);
            m_GameManager.UpdateGameState(GameManager.gameState.playing);
            m_time = Time.time + delay;
        }
    }

    public void ShowMenu(Menu menu){
        menu.gameObject.SetActive(true);
        m_eventSystem.SetSelectedGameObject(menu.m_firstButton);
    }

    public void HideMenu(Menu menu)
    {
        menu.gameObject.SetActive(false);
    }
}
