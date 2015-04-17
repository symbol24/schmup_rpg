using UnityEngine;
using System.Collections;

public class PlayerContainer : MonoBehaviour, iPlayerContainer {
	private static iPlayerContainer pcInstance;
	
	public static iPlayerContainer instance{
		get{ return pcInstance ?? (pcInstance = new PlayerContainerDummy());}
	}
	
	//credits and level stuff
	public float m_credits = 0.0f;
	public float m_experience = 0.0f;
	public int m_level = 1;

	public CannonData[] m_Cannons;
	public EquipmentData[] m_OtherEquipment;
	public ShieldData m_Shield;
    public EquipmentData[] m_inventory;

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
	//so lonely
}

public class PlayerContainerDummy: iPlayerContainer{


	public float m_currentCredits = 0.0f;
	public float m_experiencePoints = 0.0f;
	public int m_playerLevel = 1;
	
	public CannonData[] m_Cannons;
	public EquipmentData[] m_OtherEquipment;
	public ShieldData m_Shield;

}
