using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class MenuController : MonoBehaviour {
    private GameManager m_GameManager;
    private EventSystem m_eventSystem;
    private PlayerController m_playerController;
    private float delay = 0.5f;
    private float m_time = 0.0f;
    private Menu m_currentActiveMenu;
    private Menu m_currentActiveInfo;
    public enum MenuType: int
    {
        baseMenu = 0,
        status = 1,
        cannons = 2,
        chassis = 3,
        hull = 4,
        engine = 5,
        shield = 6,
        options = 7
    }

    [SerializeField] private Menu m_PauseMenu;
    [SerializeField] private Menu m_StatusandInventoryMenu;
    [SerializeField] private PlayerStatusMenu m_playerStatusMenu;
    [SerializeField] private EquipmentMenu m_equipmentMenu;
    [SerializeField] private Menu m_cannonInfo;
    [SerializeField] private Menu m_chassisInfo;
    [SerializeField] private Menu m_hullInfo;
    [SerializeField] private Menu m_engineInfo;
    [SerializeField] private Menu m_shieldInfo;
    [SerializeField] private OptionsMenu m_optionsMenu;
    

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
        else if (m_GameManager.m_CurrentState == GameManager.gameState.playing && m_GameManager.m_backButton > 0 && Time.time >= m_time)
        {
            ShowMenu(m_StatusandInventoryMenu);
            CheckIfFirstTimeOpen();
            m_GameManager.UpdateGameState(GameManager.gameState.inventory);
            m_time = Time.time + delay;

        }
        else if (m_GameManager.m_CurrentState == GameManager.gameState.inventory && m_GameManager.m_backButton > 0 && Time.time >= m_time)
        {
            HideMenu(m_StatusandInventoryMenu);
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

    private void CheckIfFirstTimeOpen()
    {
        if (m_currentActiveMenu == null)
        {
            SwitchMenu(1);
        }
    }

    public void SwitchMenu(int type)
    {
        if (m_currentActiveMenu != null)
            m_currentActiveMenu.gameObject.SetActive(false);

        if (m_currentActiveInfo != null)
            m_currentActiveInfo.gameObject.SetActive(false);

        MenuType menuType = (MenuType)type;

        m_playerController = FindObjectOfType<PlayerController>();

        switch (menuType)
        {
            case MenuType.baseMenu:
                break;
            case MenuType.status:
                m_playerStatusMenu.gameObject.SetActive(true);
                m_playerStatusMenu.UpdateLargePlayerInfo(m_playerController);
                m_currentActiveMenu = m_playerStatusMenu;
                break;
            case MenuType.options:
                m_optionsMenu.gameObject.SetActive(true);
                m_currentActiveMenu = m_optionsMenu;
                break;
            default:
                m_equipmentMenu.gameObject.SetActive(true);
                m_equipmentMenu.Init(menuType, m_playerController);
                m_currentActiveMenu = m_equipmentMenu;
                if (menuType == MenuType.cannons)
                {
                    m_cannonInfo.gameObject.SetActive(true);
                    m_currentActiveInfo = m_cannonInfo;
                }
                else if (menuType == MenuType.chassis)
                {
                    m_chassisInfo.gameObject.SetActive(true);
                    m_currentActiveInfo = m_chassisInfo;
                }
                else if (menuType == MenuType.hull)
                {
                    m_hullInfo.gameObject.SetActive(true);
                    m_currentActiveInfo = m_hullInfo;
                }
                else if (menuType == MenuType.engine)
                {
                    m_engineInfo.gameObject.SetActive(true);
                    m_currentActiveInfo = m_engineInfo;
                }
                else if (menuType == MenuType.shield)
                {
                    m_shieldInfo.gameObject.SetActive(true);
                    m_currentActiveInfo = m_shieldInfo;
                }
                break;
        }
    }
}