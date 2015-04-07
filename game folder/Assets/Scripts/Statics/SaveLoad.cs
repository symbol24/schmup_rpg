using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text;

public class SaveLoad : MonoBehaviour {
	public static PlayerData _playerData;
	private static string _FileLocation = "";
	private static string _FileName = "playerData.xml";
	private static float[] tempValues = new float[7];

	public static void SavePlayer(PlayerController player){
		_playerData = new PlayerData();
		_FileLocation = Application.dataPath + _FileName;
		_playerData.m_currentCredits = player.m_currentCredits;
		_playerData.m_experiencePoints = player.m_experiencePoints;
		_playerData.m_equipments = GetEquipmentList (player);
		Save (_FileLocation, _playerData);
	}

	public static EquipmentSaveVersion[] GetEquipmentList(PlayerController player){
		int amount = player.cannons.Length + player.m_listofEquipmentPrefabs.Length;
		EquipmentSaveVersion[] equipments = new EquipmentSaveVersion[amount];
		int x = 0;
		for(int i = 0; i < player.cannons.Length; i++){
			equipments[i] = GetCurrentEquipment(player.cannons[i]);
			x++;
		}

		int z = 0;
		for (int i = x; i < amount; i++) {
			equipments[i] = GetCurrentEquipment(player.m_listofEquipmentPrefabs[z]);
			z++;
		}

		return equipments;
	}

	public static EquipmentSaveVersion GetCurrentEquipment(EquipmentController thisEquipment){
		EquipmentSaveVersion returnEquipment = new EquipmentSaveVersion ();

		CannonController c;
		ChassisController ch;
		EngineController e;
		HullController h;
		ShieldController s;

		switch (thisEquipment.m_myType) {
		case EquipmentController.equipmentType.cannon:
			c = thisEquipment as CannonController;
			returnEquipment.m_ProjectileEnergyValue = c.m_ProjectileEnergyValue;
			returnEquipment.m_bulletName = c.m_ProjectileToShootPrefab.name;
			break;
		case EquipmentController.equipmentType.chassis:
			ch = thisEquipment as ChassisController;
			break;
		case EquipmentController.equipmentType.engine:
			e = thisEquipment as EngineController;
			break;
		case EquipmentController.equipmentType.hull:
			h = thisEquipment as HullController;
			break;
		case EquipmentController.equipmentType.shield:
			s = thisEquipment as ShieldController;
			returnEquipment.m_regenerationDelay = s.m_regenerationDelay;
			returnEquipment.m_timeToFull = s.m_timeToFull;
			break;
		}

		returnEquipment.name = thisEquipment.name;
		returnEquipment.equipType = thisEquipment.m_myType;
		returnEquipment.m_equipmentLevel = thisEquipment.m_equipmentLevel;
		returnEquipment.m_creditValue = thisEquipment.m_creditValue;
		returnEquipment.m_damageType = thisEquipment.m_damageType;
		returnEquipment.m_Owner = thisEquipment.m_Owner;

		for (int i = 0; i < thisEquipment.m_baseValues.Length; i++) {
			returnEquipment.m_baseValues[i] = thisEquipment.m_baseValues[i];
			returnEquipment.m_ValueModifiers[i] = thisEquipment.m_ValueModifiers[i];
		}



		return returnEquipment;
	}

	public static void Save(string path, PlayerData playerData)
	{
		var serializer = new XmlSerializer(typeof(PlayerData));
		using(var stream = new FileStream(path, FileMode.Create))
		{
			serializer.Serialize(stream, playerData);
		}
	}


	public class PlayerData{
		public float m_currentCredits = 0.0f;
		public float m_experiencePoints = 0.0f;

		[XmlArray("Equipment")]
		public EquipmentSaveVersion[] m_equipments;
	}

	[XmlRoot("PlayerData")]
	public class DataContainer{
		public PlayerData playerData = _playerData;
	}

	public class EquipmentSaveVersion{
		public string name;
		public EquipmentController.equipmentType equipType;
		public int m_equipmentLevel = 1;
		public float m_creditValue = 1.0f;
		public int m_damageType = 0;
		public string m_Owner = "player";

		//for cannons
		public int m_ProjectileEnergyValue = 0;
		public string m_bulletName = "";

		//for shield 
		public float m_regenerationDelay = 0.0f;
		public float m_timeToFull = 0.0f;

		[XmlArray("BaseValues")]
		public float[] m_baseValues;
		[XmlArray("Modifiers")]
		public float[] m_ValueModifiers;

		public EquipmentSaveVersion(){
			m_baseValues = new float[7];
			m_ValueModifiers = new float[7];
		}
	}

}