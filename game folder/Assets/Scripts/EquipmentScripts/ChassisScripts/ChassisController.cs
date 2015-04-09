using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class ChassisController : EquipmentController, ISavable<EquipmentData> {

	public override void Init(PlayerController player, EquipmentData data){
		base.Init (player, data);
		m_myType = equipmentType.chassis;
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
