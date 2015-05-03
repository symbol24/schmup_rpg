using UnityEngine;
using System.Collections;
using System.Collections.Generic;

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
    public ChassisData m_chassis;
    public List<EquipmentData> m_inventory;
    bool m_isFirstShipGenerated = false;
    public bool M_isFirstShipGenerated { get { return m_isFirstShipGenerated; } set { m_isFirstShipGenerated = value; } }

	void Awake(){
		if (pcInstance == null || pcInstance is PlayerContainerDummy) {
			pcInstance = this;
			
			DontDestroyOnLoad (this.gameObject);
		} else {
			Destroy(this.gameObject);
		}
	}

    #region GetterAndSetters
    public float M_credits
    {
        get
        {
            return m_credits;
        }
        set
        {
            m_credits = value;
        }
    }
    public float M_experience
    {
        get
        {
            return m_experience;
        }
        set
        {
            m_experience = value;
        }
    }
    public int M_level
    {
        get
        {
            return m_level;
        }
        set
        {
            m_level = value;
        }
    }

    public CannonData[] M_Cannons
    {
        get
        {
            return m_Cannons;
        }
        set
        {
            m_Cannons = value;
        }
    }
    public EquipmentData[] M_OtherEquipment
    {
        get
        {
            return m_OtherEquipment;
        }
        set
        {
            m_OtherEquipment = value;
        }
    }
    
    public ShieldData M_Shield
    {
        get
        {
            return m_Shield;
        }
        set
        {
            m_Shield = value;
        }
    }
    public ChassisData M_chassis
    {
        get
        {
            return m_chassis;
        }
        set
        {
            m_chassis = value;
        }
    }
    public List<EquipmentData> M_inventory
    {
        get
        {
            return m_inventory;
        }
        set
        {
            m_inventory = value;
        }
    }
    #endregion GetterAndSetters

    public EquipmentData GetOneEquipment(EquipmentController.equipmentType type)
    {
            foreach(EquipmentData e in m_OtherEquipment){
                if(e.m_myType == type) return e;
            }
            return null;
    }

    public void SetOneEquipment(EquipmentData value)
    {
        for (int i = 0; i < m_OtherEquipment.Length; i++ )
        {
            if (m_OtherEquipment[i].m_myType == value.m_myType) m_OtherEquipment[i] = value;
        }
    }


   
}

public interface iPlayerContainer{
	//so lonely
    float M_credits { get; set; }
    float M_experience { get; set; }
    int M_level { get; set; }
    CannonData[] M_Cannons { get; set; }
    EquipmentData[] M_OtherEquipment { get; set; }
    ShieldData M_Shield { get; set; }
    ChassisData M_chassis { get; set; }
    List<EquipmentData> M_inventory { get; set; }
    EquipmentData GetOneEquipment(EquipmentController.equipmentType type);
    void SetOneEquipment(EquipmentData value);
    bool M_isFirstShipGenerated { get; set; }
}

public class PlayerContainerDummy: iPlayerContainer{


	public float m_currentCredits = 0.0f;
	public float m_experiencePoints = 0.0f;
	public int m_playerLevel = 1;
	
	public CannonData[] m_Cannons;
	public EquipmentData[] m_OtherEquipment;
	public ShieldData m_Shield;


    public float M_credits
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float M_experience
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public int M_level
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public CannonData[] M_Cannons
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public EquipmentData[] M_OtherEquipment
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public ShieldData M_Shield
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public ChassisData M_chassis
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public List<EquipmentData> M_inventory
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public EquipmentData GetOneEquipment(EquipmentController.equipmentType type)
    {
            foreach(EquipmentData e in m_OtherEquipment){
                if(e.m_myType == type) return e;
            }
            return null;
    }
    public void SetOneEquipment(EquipmentData value)
    {
        for (int i = 0; i < m_OtherEquipment.Length; i++)
        {
            if (m_OtherEquipment[i].m_myType == value.m_myType) m_OtherEquipment[i] = value;
        }
    }


    public bool M_isFirstShipGenerated
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }
}
