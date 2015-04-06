using UnityEngine;
using System.Collections;

public class BaseBarSystemController : MonoBehaviour {
	public float m_maxValue = 100;
	public float m_currentValue = 100;
	public float m_originalScaleX = 0.0f;
	public PlayerController m_player;
	public bool m_isRegenarating = false;
	private float m_regenIncrement;
	
	// Use this for initialization
	public virtual void Start(){
		m_originalScaleX = gameObject.transform.localScale.x;
		m_player = FindObjectOfType(typeof(PlayerController)) as PlayerController;
	}
	
	// Update is called once per frame
	public virtual void Update () {
		UpdateBar ();
		if (m_isRegenarating) {
			m_isRegenarating = Regenration(m_regenIncrement);
		}
	}

	private void UpdateBar(){
		float percent = (m_currentValue / m_maxValue) * m_originalScaleX;
		Vector3 newScale = new Vector3 (percent, transform.localScale.y, transform.localScale.z);
		gameObject.transform.localScale = newScale;
	}
	
	public float GetCurrentValue(){
		return m_currentValue;
	}
	
	public void SetCurrentValue(float current){
		m_currentValue = current;
	}

	public void SetStartValues(float value){
		m_currentValue = value;
		m_maxValue = value;
	}

	public bool Regenration(float increment){
		m_regenIncrement = increment;
		if (m_currentValue < m_maxValue) {
			if (m_currentValue + increment >= m_maxValue)
				m_currentValue = m_maxValue;
			else
				m_currentValue += increment;

			return true;
		} else
			return false;
	}
}
