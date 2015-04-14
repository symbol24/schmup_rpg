using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

[RequireComponent(typeof(DummyCollider))]
public class ShieldController : EquipmentController, ISavable<ShieldData>, IShieldController {
	public float m_shieldArmor = 0;
	public float m_regenerationDelay = 2.0f;
	public float m_timeToFull = 3.0f;
    public float m_elementalFactor = 0f;
    public EnergyType m_energyType;

    #region MovedtoHPController
    /*
	public Collider2D m_collider;


	void OnTriggerEnter2D(Collider2D coll) {
		
		ProjectileController tempBullet = coll.gameObject.GetComponent<ProjectileController>();
		if (tempBullet!= null && tempBullet.m_Target == m_Owner) {
			int tempBulletDmg = tempBullet.m_damageType;
			//m_playerController.m_shieldBar.SetCurrentValue(DamageCalculators.ShieldHit(tempBullet.m_DamageValue, m_playerController.m_shieldBar.m_currentValue, m_shieldArmor, tempBulletDmg, m_damageType));
			//m_playerController.m_shieldBar.CheckShieldHealth ();
			tempBullet.DestroyObjectAndBehaviors();
		}
	}*/
    #endregion MovedtoHPController
    
    #region ISavable implementation

    public ShieldData GetSavableObject ()
	{
		var ret = GetSavableObjectInternal <ShieldData>();
		ret.m_regenerationDelay = m_regenerationDelay;
		ret.m_timeToFull = m_timeToFull;
		return ret;
	}

	public void LoadFrom (ShieldData data)
	{
		LoadFromInternal (data);
		m_regenerationDelay = data.m_regenerationDelay;
		m_timeToFull = data.m_timeToFull;
	}

	#endregion

    private IDummyCollider _collider;
    public IDummyCollider Collider
    {
        get
        {
            if (_collider == null)
            {
                _collider = GetComponent<IDummyCollider>();
            }
            return _collider;
        }
    }

    public EnergyType EnergyType { get { return m_energyType; } }
    public float BonusAtt { get { return m_elementalFactor; } }
    public Renderer[] _visualComponents;
    public Renderer[] VisualComponents { get { return _visualComponents; } }
}
