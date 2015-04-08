using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class BasePlayerController : MonoBehaviour, IBasePlayerController {
	private static BasePlayerController bpcInstance;

	public static BasePlayerController instance{
		get{ return bpcInstance;}
	}

	//credits and level stuff
	public float m_currentCredits = 0.0f;
	public float m_experiencePoints = 0.0f;
	public int m_playerLevel = 1;

	public CannonController[] m_usedCannons;
	public ChassisController m_usedChassis;
	public HullController m_usedHull;
	public EngineController m_usedEngine;
	public ShieldController m_usedShield;

	void Awake(){
		if (bpcInstance == null) {
			bpcInstance = this;

			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy(this.gameObject);
		}
	}

}
