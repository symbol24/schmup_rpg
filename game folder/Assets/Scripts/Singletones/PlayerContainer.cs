using UnityEngine;
using System.Collections;

public class PlayerContainer : MonoBehaviour, iPlayerContainer {
	private static iPlayerContainer pcInstance;
	
	public static iPlayerContainer instance{
		get{ return pcInstance ?? (pcInstance = new PlayerContainerDummy());}
	}
	
	//credits and level stuff
	public float m_currentCredits = 0.0f;
	public float m_experiencePoints = 0.0f;
	public int m_playerLevel = 1;
	
	public CannonController m_usedCannon1;
	public CannonController m_usedCannon2;
	public ChassisController m_usedChassis;
	public HullController m_usedHull;
	public EngineController m_usedEngine;
	public ShieldController m_usedShield;

	void Awake(){
		if (pcInstance == null || pcInstance is PlayerContainerDummy) {
			pcInstance = this;
			
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy(this.gameObject);
		}
	}
}

public interface iPlayerContainer{

}

public class PlayerContainerDummy: iPlayerContainer{


	public float m_currentCredits = 0.0f;
	public float m_experiencePoints = 0.0f;
	public int m_playerLevel = 1;
	
	public CannonController m_usedCannon1;
	public CannonController m_usedCannon2;
	public ChassisController m_usedChassis;
	public HullController m_usedHull;
	public EngineController m_usedEngine;
	public ShieldController m_usedShield;

}
