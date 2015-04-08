using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class EngineController : EquipmentController {
	public override void Init(PlayerController player, EquipmentData data){
		base.Init (player, data);
		m_myType = equipmentType.engine;
	}
}
