using UnityEngine;
using System.Collections;

public class EnergySystemController : MonoBehaviour {
	private float m_MaxEnergy = 100;
	private float m_CurrentEnergy = 100;
	private float m_originalScaleX = 0.0f;
	private PlayerController m_player;

	// Use this for initialization
	void Start () {
		m_originalScaleX = gameObject.transform.localScale.x;
		m_player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
		m_MaxEnergy = m_player.GetPlayerMaxEnergy ();
	}
	
	// Update is called once per frame
	void Update () {
		m_CurrentEnergy = m_player.GetPlayerCurrentEnergy ();
		float energyPercent = (m_CurrentEnergy / m_MaxEnergy) * m_originalScaleX;
		Vector3 newScale = new Vector3 (energyPercent, transform.localScale.y, transform.localScale.z);
		gameObject.transform.localScale = newScale;
	}

	public void ChangeEnergyTotal(string operation, int value){
		if(operation=="add"){
			if(m_CurrentEnergy + value <= m_MaxEnergy) m_CurrentEnergy += value;
			else m_CurrentEnergy = m_MaxEnergy;
		}else if(operation=="substract"){
			if(m_CurrentEnergy - value >= 0) m_CurrentEnergy -= value;
			else m_CurrentEnergy = 0;
		}
	}

	public float GetCurrentEnergy(){
		return m_CurrentEnergy;
	}
}
