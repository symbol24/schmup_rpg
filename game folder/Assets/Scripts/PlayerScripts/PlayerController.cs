using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization;
using System;
using System.Linq;
using UnityEngine.UI; 

[Serializable]
public class PlayerController : MonoBehaviour, IPlayerStats {
	public GameManager m_GameManager;
    private PlayerInfo m_playerInfo;

	//the base values compiled
	[SerializeField] protected float[] m_playerBaseStatValues = new float[7];
	
	//the modifiers compiled
	[SerializeField] protected float[] m_playerStatModifiers = new float[7];

    #region CalculatedPlayerStats
    //the calculated info
    private float _m_playerDamage = 1.0f;
    public float m_playerDamage
    {
        get { return _m_playerDamage; }
        set
        {
            if (_m_playerDamage != value)
            {
                _m_playerDamage = value;
                if (DamageChanged != null) DamageChanged(this, new floatEventArgs {NewValue = value});
            }
        }
    }

    private float _m_playerFireRate = 1.0f;
    public float m_playerFireRate
    {
        get
        {
            return _m_playerFireRate;
        }
        set
        {
            if (_m_playerFireRate != value)
            {
                _m_playerFireRate = value;
                if (FireRateChanged != null) FireRateChanged(this, new floatEventArgs() {NewValue = value});
            }
        }
    }

    private float _m_maxPlayerHP = 1.0f;
    public float m_maxPlayerHP
    {
        get
        {
            return _m_maxPlayerHP;
        }
        set
        {
            if (_m_maxPlayerHP != value)
            {
                _m_maxPlayerHP = value;
                if (MaxHPChanged != null) MaxHPChanged(this, new floatEventArgs {NewValue = value});
            }
        }
    }

    private float _m_playerArmor = 0;
    public float m_playerArmor
    {
        get
        {
            return _m_playerArmor;
        }
        set
        {
            if (_m_playerArmor != value)
            {
                _m_playerArmor = value;
                if (ArmorChanged != null) ArmorChanged(this, new floatEventArgs {NewValue = value});
            }
        }
    }

    private float _m_playerMouvementSpeed = 5.0f;
    public float m_playerMouvementSpeed
    {
        get
        {
            return _m_playerMouvementSpeed;
        }
        set
        {
            if (_m_playerMouvementSpeed != value)
            {
                _m_playerMouvementSpeed = value;
                if (MouvementSpeedChanged != null) MouvementSpeedChanged(this, new floatEventArgs {NewValue = value});
            }
        }
    }

    private float _m_maxPlayerEnergy = 10.0f;
    public float m_maxPlayerEnergy
    {
        get
        {
            return _m_maxPlayerEnergy;
        }
        set
        {
            if (_m_maxPlayerEnergy != value)
            {
                _m_maxPlayerEnergy = value;
                if (MaxEnergyChanged != null) MaxEnergyChanged(this, new floatEventArgs {NewValue = value});
            }
        }
    }

    private float _m_maxPlayerShield = 10.0f;
    public float m_maxPlayerShield
    {
        get { return _m_maxPlayerShield; }
        set
        {
            if (_m_maxPlayerShield != value)
            {
                _m_maxPlayerShield = value;
                if (MaxShieldChanged != null) MaxShieldChanged(this, new floatEventArgs {NewValue = value});
            }
        }
    }

    private float _m_regenerationDelay;
    public float m_regenerationDelay
    {
        get { return _m_regenerationDelay; }
        set
        {
            if (_m_regenerationDelay != value)
            {
                _m_regenerationDelay = value;
                if (ShieldRechargeDelayChanged != null)
                {
                    ShieldRechargeDelayChanged(this, new floatEventArgs { NewValue = value });
                }
            }
        }
    }

    private float _m_timeToFullShieldRecharge;
    public float m_TimeToFullShieldRecharge
    {
        get { return _m_timeToFullShieldRecharge; }
        set
        {
            if (_m_timeToFullShieldRecharge != value)
            {
                _m_timeToFullShieldRecharge = value;
                if (ShieldRechargeTimeChanged != null)
                {
                    ShieldRechargeTimeChanged(this, new floatEventArgs { NewValue = value });
                }
            }
        }
    }

	private float _m_experience;
	public float m_experience
	{
		get { return _m_experience; }
		set
		{
			if (_m_experience != value)
			{
				_m_experience = value;
				if (ExperienceChanged != null)
				{
					ExperienceChanged(this, new floatEventArgs { NewValue = value });
				}
			}
		}
	}

	private float _m_credits;
	public float m_credits
	{
		get { return _m_credits; }
		set
		{
			if (_m_credits != value)
			{
				_m_credits = value;
				if (CreditsChanged != null)
				{
					CreditsChanged(this, new floatEventArgs { NewValue = value });
				}
			}
		}
	}

	private float _m_level;
	public float m_level
	{
		get { return _m_level; }
		set
		{
			if (_m_level != value)
			{
				_m_level = value;
				if (LevelChanged != null)
				{
					LevelChanged(this, new floatEventArgs { NewValue = value });
				}
			}
		}
	}

    #region IPlayerStats Implementation
    public float Damage { get { return m_playerDamage; } }
    public event EventHandler<floatEventArgs> DamageChanged;
    public float FireRate { get { return m_playerFireRate; } }
    public event EventHandler<floatEventArgs> FireRateChanged;
    public float MaxHP { get { return m_maxPlayerHP; } }
    public event EventHandler<floatEventArgs> MaxHPChanged;
    public float Armor { get { return m_playerArmor; } }
    public event EventHandler<floatEventArgs> ArmorChanged;
    public float MouvementSpeed { get { return m_playerMouvementSpeed; } }
    public event EventHandler<floatEventArgs> MouvementSpeedChanged;
    public float MaxEnergy { get { return m_maxPlayerEnergy; } }
    public event EventHandler<floatEventArgs> MaxEnergyChanged;
    public float MaxShield { get { return m_maxPlayerShield; } }
    public event EventHandler<floatEventArgs> MaxShieldChanged;
    public float ShieldRechargeTime { get { return m_regenerationDelay; } }
    public event EventHandler<floatEventArgs> ShieldRechargeTimeChanged;
    public float ShieldRechargeDelay { get { return m_regenerationDelay; } }
	public event EventHandler<floatEventArgs> ShieldRechargeDelayChanged;
	public float Experience { get { return m_experience; } }
	public event EventHandler<floatEventArgs> ExperienceChanged;
	public float Credits { get { return m_credits; } }
	public event EventHandler<floatEventArgs> CreditsChanged;
	public float Level { get { return m_level; } }
	public event EventHandler<floatEventArgs> LevelChanged;

    #endregion IPlayerStats Implementation

    #endregion CalculatedPlayerStats

    private float currentLevelExp = 0.0f;

	//boxcollider
	private BoxCollider2D m_myCol;

	//screen limiters
	public float horLimit = 0.0f;
	public float vertLimit = 0.0f;
	private Vector2 velocity = Vector2.zero;

	//death state
	private bool isDead = false;
	private Vector3 startingPosition;

	//the explosion to use when a bullet hits
	public GameObject pinkExplosionPrefab;

	//the cannons!
	public GameObject cannonPoint;
	private CannonController[] instantiatedCannons;
	public CannonController currentCannon;
	public int cannonID;
	public float cannonSelectionDelay = 0.2f;
	private float cannonSelectionTimer = 0.0f;
	private int cannonSelectionDirection = 0;

	//equipment prefabs!
	private EquipmentController[] m_instantiatedEquipment;
	private ShieldController m_instantiatedShield;
    private ChassisController m_instatiatedChassis;
    private List<EquipmentController> m_allEquips = new List<EquipmentController>();

    //ship Sprites
    private Sprite[] m_shipSprites;
    private SpriteRenderer m_spriteRender;

    #region ChangedForHPController
    //UI energy, health and shield!
	/*
    public EnergySystemController m_energyBar;
	public HPSystemController m_HPBar;
	public ShieldHPSystemController m_shieldBar;*/
    #endregion ChangedForHPController
    public IHPController m_HPController;


    

    //hit comparerererer
	public string target;

	void Start(){

		//this = SaveLoad.LoadPlayer ();
		//SaveLoad.SavePlayer (this);
		//m_instantiatedEquipment.First().GetSavableObject().SaveObject ("saveTest.xml");
		//SaveLoad.Save ();
	}

	public void Init(){
		startingPosition = transform.position;
		m_GameManager = FindObjectOfType<GameManager> ();
        m_playerInfo = FindObjectOfType<PlayerInfo>();
		horLimit = m_GameManager.m_limiterX - 0.2f;
		m_myCol = GetComponent<BoxCollider2D> ();

        //setup equipment
        SetupCannons();
		SetupEquipment ();
		CalculateStats ();
        UpdatePlayerInfo();
        m_spriteRender = GetComponent<SpriteRenderer>();
        m_spriteRender.sprite = m_shipSprites[1];

        //HP
	    m_HPController = GetComponent<IHPController>();
        if(m_HPController == null) Debug.LogError("No IHPControllerFound in " + gameObject.name);
	    m_HPController.Init(this, m_instantiatedShield, m_instatiatedChassis);
        m_HPController.Died += m_HPController_Died;

	    #region ChangedForHPController

	    /*
		m_energyBar = FindObjectOfType<EnergySystemController> ();
		m_HPBar = FindObjectOfType<HPSystemController> ();
		m_shieldBar = FindObjectOfType<ShieldHPSystemController> ();
		m_energyBar.SetStartValues (m_maxPlayerEnergy);
		m_HPBar.SetStartValues (m_maxPlayerHP);
		m_shieldBar.SetStartValues (m_maxPlayerShield);*/

	    #endregion ChangedForHPController
	}

    public void UpdatePlayerInfo()
    {
        m_playerInfo.UpdateStats(this);
        m_playerInfo.UpdateEquipmentNames(m_allEquips);
    }

    
	
	void Update () {
		if(m_GameManager.m_CurrentState == GameManager.gameState.playing && !isDead){

			if(InputManager.instance.m_firebutton > 0){
				currentCannon.FireForEachReference(m_playerDamage, m_playerFireRate);
			}

			//cannon selection
			cannonSelectionDirection = -1;
            if (InputManager.instance.m_switchButtons < 0)
            {
				if(Time.time >= cannonSelectionTimer){
					cannonSelectionDirection = 0;
					cannonSelectionTimer = Time.time + cannonSelectionDelay;
				}
			}else if(InputManager.instance.m_switchButtons > 0){
				if(Time.time >= cannonSelectionTimer){
					cannonSelectionDirection = 1;
					cannonSelectionTimer = Time.time + cannonSelectionDelay;
				}
			}
			if(cannonSelectionDirection != -1) ChangeSelectedCannon(cannonSelectionDirection);

			//move
			velocity.x = InputManager.instance.m_HorValue * m_playerMouvementSpeed * Time.deltaTime;
            velocity.y = InputManager.instance.m_VertValue * m_playerMouvementSpeed * Time.deltaTime;

			//changing the ship from side to side and idle
            if (InputManager.instance.m_HorValue > 0.0f){
                //anim.SetInteger("direction",1);
                m_spriteRender.sprite = m_shipSprites[2];
			} else if (InputManager.instance.m_HorValue < 0.0f) {
                //anim.SetInteger("direction", -1);
                m_spriteRender.sprite = m_shipSprites[0];
			}else{
                //anim.SetInteger("direction", 0);
                m_spriteRender.sprite = m_shipSprites[1];
			}

			//move the ship, cannon and shields
			//velocity = Vector2.ClampMagnitude(velocity, m_playerMouvementSpeed * Time.deltaTime);
			transform.Translate (velocity, Space.World);

			//clamping to screen size
			float clampedLimitX = Mathf.Clamp(transform.position.x, -horLimit, horLimit);
			float clampedLimitY = Mathf.Clamp(transform.position.y, -vertLimit, vertLimit);			
			transform.position = new Vector3 (clampedLimitX, clampedLimitY, 0.0f);
		}
	}

	public void SetCannons(CannonController[] newCannons){
		instantiatedCannons = newCannons;
	}

	public void SetOtherEquipmenet(EquipmentController[] newEquip){
		m_instantiatedEquipment = newEquip;
	}

    public void SetChassis(ChassisController newChassis)
    {
        m_instatiatedChassis = newChassis;
        m_shipSprites = newChassis.m_shipSprites;
    }

	public void SetShield(ShieldController newShield){
		m_instantiatedShield = newShield;
		//m_shieldBar = FindObjectOfType<ShieldHPSystemController> ();
	}	


    void m_HPController_Died(object sender, DeathReasonEventArgs e)
    {
        StartCoroutine(m_GameManager.DeathExplosion(this, m_HPController.CurrentHP));
    }

    #region ChangedforHPController
    //checking health to change amount of shields and changing amount of lives if needed
	public void CheckHealth(){
		if(m_HPController.CurrentHP <= 0){
			//m_HPBar.SetCurrentValue(m_maxPlayerHP);
			StartCoroutine(m_GameManager.DeathExplosion(this, m_HPController.CurrentHP));
		}
	}
    #endregion ChangedforHPController

    public void RepositionShip(){
		transform.position = startingPosition;
		currentCannon.transform.position = cannonPoint.transform.position;
	}

	//changing the equipped gun with the rpess of a button!
	private void ChangeSelectedCannon(int direction){
		int previousID = cannonID;
		bool foundCannon = false;
		if (direction == 0 && cannonID > 0) {
			while(!foundCannon && cannonID > 0){
				cannonID--;
				if(instantiatedCannons[cannonID].m_IsAvailable) foundCannon = true;
			}
			if(!foundCannon) cannonID = previousID;
		}else if(direction == 1 && cannonID < instantiatedCannons.Length-1){
			while(!foundCannon && cannonID < (instantiatedCannons.Length-1)){
				cannonID++;
				if(instantiatedCannons[cannonID].m_IsAvailable) foundCannon = true;
			}
			if(!foundCannon) cannonID = previousID;
		}
		if (cannonID != previousID) {
			currentCannon.gameObject.SetActive(false);
			currentCannon = instantiatedCannons[cannonID];
			currentCannon.gameObject.SetActive(true);
			WeaponSwitchUpdateValuesModifiers(instantiatedCannons[previousID], currentCannon);
            SetBulletImg();
		}
	}

//	public void ActivateCannon(int newCannonID){
//		instantiatedCannons [newCannonID].m_IsAvailable = true;
//		cannonID = newCannonID;
//		currentCannon.gameObject.SetActive(false);
//		currentCannon = instantiatedCannons[cannonID];
//		currentCannon.gameObject.SetActive(true);
//	}

	public void SetupCannons(){
		//set the first cannon
		for (int i = 0; i < instantiatedCannons.Length; i++) {
			instantiatedCannons[i].transform.parent = transform;
			instantiatedCannons[i].Init(this);
            m_allEquips.Add(instantiatedCannons[i]);
			if(i == cannonID) {
				currentCannon = instantiatedCannons[i];
				currentCannon.m_IsAvailable = true;
				UpdateValuesModifiers(currentCannon);
			}else {
				instantiatedCannons[i].gameObject.SetActive(false);
				instantiatedCannons[i].m_IsAvailable = true;
			}
		}
	}

    public void SetBulletImg()
    {
        Image img = GameObject.Find("projectileImg").GetComponent<Image>();
        SpriteRenderer cannonSprite = currentCannon.m_ProjectileToShootPrefab.GetComponent<SpriteRenderer>();
        img.sprite = cannonSprite.sprite;
    }

	public void SetupEquipment(){
		for(int i = 0; i < m_instantiatedEquipment.Length; i++){
            if (m_instantiatedEquipment[i] != null)
            {
                m_instantiatedEquipment[i].transform.parent = transform;
                UpdateValuesModifiers(m_instantiatedEquipment[i]);
                m_instantiatedEquipment[i].Init(this);
                int pos = GetPositionInAllEquips(m_instantiatedEquipment[i].m_myType);
                UpdateAllEquips(pos, m_instantiatedEquipment[i]);
            }
		}

        SetupChassis();

        SetupShield();
	}

    public void SetupChassis()
    {
        m_instatiatedChassis.transform.parent = transform;
        UpdateValuesModifiers(m_instatiatedChassis);
        m_instatiatedChassis.Init(this);
        int pos = GetPositionInAllEquips(m_instatiatedChassis.m_myType);
        UpdateAllEquips(pos, m_instatiatedChassis);
    }

    public void SetupShield()
    {
        m_instantiatedShield.transform.parent = transform;
        UpdateValuesModifiers(m_instantiatedShield);
        m_instantiatedShield.Init(this);
        int pos = GetPositionInAllEquips(m_instantiatedShield.m_myType);
        UpdateAllEquips(pos, m_instantiatedShield);
    }

    public void UpdateAllEquips(int id, EquipmentController newEquip)
    {
        if (id == -1)
            m_allEquips.Add(newEquip);
        else
            m_allEquips[id] = newEquip;
    }

    public int GetPositionInAllEquips(EquipmentController.equipmentType type)
    {
        for(int i = 0; i < m_allEquips.Count; i++)
        {
            if (m_allEquips[i].m_myType == type) return i;
        }

        return -1;
    }

    public int GetPositionInOtherEquips(EquipmentController.equipmentType type)
    {
        for (int i = 0; i < m_instantiatedEquipment.Length; i++)
        {
            if (m_instantiatedEquipment[i].m_myType == type) return i;
        }

        return -1;
    }

	public void UpdateCollider(bool status){
		m_myCol.enabled = status;
	}

	public void UpdateValuesModifiers(EquipmentController thisEquipment){
		for(int i = 0; i < m_playerBaseStatValues.Length; i++){
			m_playerBaseStatValues[i] += thisEquipment.m_baseValues[i];
			m_playerStatModifiers[i] += thisEquipment.m_ValueModifiers[i];
		}
	}
	
	public void CalculateStats(){
		m_playerDamage = m_playerBaseStatValues[0] + (m_playerBaseStatValues[0] * m_playerStatModifiers[0]);
		m_playerFireRate =  m_playerBaseStatValues[1] + (m_playerBaseStatValues[1] * m_playerStatModifiers[1]);
		m_maxPlayerHP =  m_playerBaseStatValues[2] + (m_playerBaseStatValues[2] * m_playerStatModifiers[2]);
		m_playerArmor =  m_playerBaseStatValues[3] + (m_playerBaseStatValues[3] * m_playerStatModifiers[3]);
		m_playerMouvementSpeed =  m_playerBaseStatValues[4] + (m_playerBaseStatValues[4] * m_playerStatModifiers[4]);
		m_maxPlayerEnergy =  m_playerBaseStatValues[5] + (m_playerBaseStatValues[5] * m_playerStatModifiers[5]);
		m_maxPlayerShield =  m_playerBaseStatValues[6] + (m_playerBaseStatValues[6] * m_playerStatModifiers[6]);
	    m_regenerationDelay = m_instantiatedShield.m_regenerationDelay;

		//PrintStats ();
        m_playerInfo.UpdateStats(this);
	}
	
	public void WeaponSwitchUpdateValuesModifiers(EquipmentController oldWeapon, EquipmentController newWeapon){
		for(int i = 0; i < m_playerBaseStatValues.Length; i++){
			m_playerBaseStatValues[i] -= oldWeapon.m_baseValues[i];
			m_playerStatModifiers[i] -= oldWeapon.m_ValueModifiers[i];
		}
		
		for(int i = 0; i < m_playerBaseStatValues.Length; i++){
			m_playerBaseStatValues[i] += newWeapon.m_baseValues[i];
			m_playerStatModifiers[i] += newWeapon.m_ValueModifiers[i];
		}
		
		CalculateStats ();
	}

	public void SetValuesandModifiers(float[] values, float[] modifiers){
		m_playerBaseStatValues = values;
		m_playerStatModifiers = values;
	}
	
	public void AddExp(float newExp){
		m_experience += newExp;
        currentLevelExp += newExp;
	}

    public void CheckLevel()
    {
        int tempLvl = StatCalculator.GetCurrentLevel(m_experience);
        if(tempLvl > m_level){
            m_level = tempLvl;
            currentLevelExp = 0.0f; //for bar?
            m_playerInfo.UpdateLevel(m_level);
        }
    }

	private void PrintStats(){
		print ("m_level " + m_level);
		print ("m_experience " + m_experience);
		print ("m_credits " + m_credits);
		print ("m_playerDamage: " + m_playerDamage);
		print ("m_playerFireRate: " + m_playerFireRate);
		print ("m_maxPlayerHP: " + m_maxPlayerHP);
		print ("m_playerArmor: " + m_playerArmor);
		print ("m_playerMouvementSpeed: " + m_playerMouvementSpeed);
		print ("m_maxPlayerEnergy: " + m_maxPlayerEnergy);
		print ("m_maxPlayerShield: " + m_maxPlayerShield);
		print ("m_regenerationDelay: " + m_regenerationDelay);
		print ("m_TimeToFullShieldRecharge " + m_TimeToFullShieldRecharge);
	}

    public CannonController[] GetCannons(){
        return instantiatedCannons;
    }

    public EquipmentController[] GetOtherEquipment(){
        return m_instantiatedEquipment;
    }

	/*void OnTriggerEnter2D(Collider2D coll) {
		
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Owner == target) {
			Instantiate (pinkExplosionPrefab, tempBullet.transform.position, tempBullet.transform.rotation);
			m_HPBar.SetCurrentValue(DamageCalculators.Hit(tempBullet.m_DamageValue, m_HPBar.GetCurrentValue(), m_playerArmor));
			CheckHealth();
			tempBullet.DestroyObjectAndBehaviors();
		}
	}*/

    public iPlayerContainer SavePlayer()
    {
        PlayerContainer ret = (PlayerContainer)PlayerContainer.instance;
        CannonData[] tempC = new CannonData[instantiatedCannons.Length];
        for (int i = 0; i < tempC.Length; i++)
        {
            tempC[i] = instantiatedCannons[i].GetSavableObject();
        }
        ret.m_Cannons = tempC;

        EquipmentData[] tempE = new EquipmentData[m_instantiatedEquipment.Length];
        for (int i = 0; i < tempE.Length; i++)
        {
            tempE[i] = m_instantiatedEquipment[i].GetSavableObjectInternal<EquipmentData>();
        }
        ret.m_OtherEquipment = tempE;

        ret.m_Shield = m_instantiatedShield.GetSavableObject();

        ret.m_chassis = m_instatiatedChassis.GetSavableObject();

        return ret;
    }

    public ChassisController GetChassis()
    {
        return m_instatiatedChassis;
    }

    public ShieldController GetShield()
    {
        return m_instantiatedShield;
    }

    public List<EquipmentController> GetAllEquips()
    {
        return m_allEquips;
    }

    public Sprite[] GetShipSprites()
    {
        return m_shipSprites;
    }

    public void UpdateCannonRefs()
    {
        foreach (CannonController c in instantiatedCannons)
        {
            c.UpdateRefs(m_instatiatedChassis.m_cannonRefs);
        }
    }
}
