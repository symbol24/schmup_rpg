using UnityEngine;
using System.Collections;
using UnityEngine.UI;
using UnityEngine.EventSystems;

public class PlayerStatusMenu : Menu {
    private PlayerController m_playerController;

	// Use this for initialization
	void Init () {
        m_playerController = FindObjectOfType<PlayerController>();
	}
	
	// Update is called once per frame
	void Update () {
	
	}
}
