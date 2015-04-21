using System;
using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

[RequireComponent(typeof(DummyCollider))]
public class ChassisController : EquipmentController, ISavable<ChassisData>, IChassisController
{
    public enum ChassisSize
    {
        small,
        medium,
        large
    }

    public ChassisSize m_chassisSize = ChassisSize.small;

    private IDummyCollider _collider;
    public IDummyCollider Collider
    {
        get
        {
            if (_collider == null)
            {
                _collider = GetComponent<IDummyCollider>();
                if (_collider == null) Debug.Log("DummyCollider not Found in " + gameObject.name);
            }
            return _collider;
        }
    }

    public override void Init(PlayerController player){
		base.Init (player);
	}

	#region ISavable implementation

    public ChassisData GetSavableObject()
	{
		return GetSavableObjectInternal <ChassisData>();
	}

    public void LoadFrom(ChassisData data)
	{
		LoadFromInternal (data);
        //m_chassisSize = data.m_chassisSize;
	}

	#endregion

    
}
