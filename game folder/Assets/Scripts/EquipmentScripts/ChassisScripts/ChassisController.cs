using System;
using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

[RequireComponent(typeof(DummyCollider))]
public class ChassisController : EquipmentController, ISavable<EquipmentData>, IChassisController
{
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

	public EquipmentData GetSavableObject ()
	{
		return GetSavableObjectInternal <EquipmentData>();
	}

	public void LoadFrom (EquipmentData data)
	{
		LoadFromInternal (data);
	}

	#endregion

    
}
