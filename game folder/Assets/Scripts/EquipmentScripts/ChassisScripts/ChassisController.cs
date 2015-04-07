using UnityEngine;
using System.Collections;
using System.Xml; 
using System.Xml.Serialization; 

public class ChassisController : EquipmentController {

	public override void Init(PlayerController player){
		base.Init (player);
		m_myType = equipmentType.chassis;
	}
}
