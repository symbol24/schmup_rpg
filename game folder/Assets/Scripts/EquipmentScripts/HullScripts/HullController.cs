using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class HullController : EquipmentController, ISavable<EquipmentData> {
	public override void Init(PlayerController player){
		base.Init (player);
	}

	#region ISavable implementation
	
	public EquipmentData GetSavableObject ()
	{
		return GetSavableObjectInternal<EquipmentData> ();
	}
	
	public void LoadFrom (EquipmentData data)
	{
		LoadFromInternal (data);
	}
	
	#endregion
}
