using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Xml; 
using System.Xml.Serialization; 
using System.IO; 
using System.Text;

public class SaveLoad : MonoBehaviour {
	
	private static string _FileName = "playerData2.xml";


	public static void Save()
	{
//		var savableObjects = FindObjectsOfType(typeof(ISavable<EquipmentData>)) as ISavable<EquipmentData>[];
//		foreach (var item in savableObjects) {
//			item.GetSavableObject().SaveObject(_FileName);
//		}
	}
//	public static PlayerData _playerData;
//	private static string _FileLocation = "";
//	private static string _FileName = "playerData.xml";
//	private static float[] tempValues = new float[7];
//
//	public static void SavePlayer(PlayerController player){
//		_playerData = new PlayerData();
//		_FileLocation = Application.dataPath + _FileName;
//		_playerData.m_currentCredits = player.m_currentCredits;
//		_playerData.m_experiencePoints = player.m_experiencePoints;
//		_playerData.m_equipments = GetEquipmentListForSave (player);
//		SavePlayerData (_FileLocation, _playerData);
//	}
//
//	public static PlayerController LoadPlayer(){
//		_FileLocation = Application.dataPath + _FileName;
//
//		PlayerData playerData = LoadPlayerData (_FileLocation);
//
//		PlayerController returnPlayer = UpdatePlayerController(playerData);
//		return returnPlayer;
//	}
//
//
//
//	public static EquipmentSaveVersion[] GetEquipmentListForSave(PlayerController player){
//		int amount = player.cannons.Length + player.m_listofEquipmentPrefabs.Length;
//		EquipmentSaveVersion[] equipments = new EquipmentSaveVersion[amount];
//		int x = 0;
//		for(int i = 0; i < player.cannons.Length; i++){
//			equipments[i] = GetCurrentEquipment(player.cannons[i]);
//			x++;
//		}
//
//		int z = 0;
//		for (int i = x; i < amount; i++) {
//			equipments[i] = GetCurrentEquipment(player.m_listofEquipmentPrefabs[z]);
//			z++;
//		}
//
//		return equipments;
//	}
//	
//	public static EquipmentController[] GetCannonListForLoad(PlayerData playerData){
//		EquipmentController[] returnList = new EquipmentController[2];
//
//		for (int i = 0; i < 2; i++) {
//			returnList[i] = GetEquipmentForLoad(playerData.m_equipments[i]);
//		}
//
//		return returnList;
//	}
//
//	public static PlayerController UpdatePlayerController(PlayerData playerData){
//		PlayerController returnPlayer = new PlayerController ();
//
//		returnPlayer.m_currentCredits = playerData.m_currentCredits;
//		returnPlayer.m_experiencePoints = playerData.m_experiencePoints;
//
//		return returnPlayer;
//	}
//
//	public static EquipmentController GetEquipmentForLoad(EquipmentSaveVersion thisEquipment){
//		EquipmentController returnEquipment = new EquipmentController ();
//
//		CannonController c;
//		ChassisController ch;
//		EngineController e;
//		HullController h;
//		ShieldController s;
//
//		returnEquipment.name = thisEquipment.name;
//		returnEquipment.m_myType = thisEquipment.equipType;
//		returnEquipment.m_equipmentLevel = thisEquipment.m_equipmentLevel;
//		returnEquipment.m_creditValue = thisEquipment.m_creditValue;
//		returnEquipment.m_damageType = thisEquipment.m_damageType;
//		returnEquipment.m_Owner = thisEquipment.m_Owner;
//		
//		for (int i = 0; i < thisEquipment.m_baseValues.Length; i++) {
//			returnEquipment.m_baseValues[i] = thisEquipment.m_baseValues[i];
//			returnEquipment.m_ValueModifiers[i] = thisEquipment.m_ValueModifiers[i];
//		}
//
//		switch (thisEquipment.equipType) {
//		case EquipmentController.equipmentType.cannon:
//			c = returnEquipment as CannonController;
//			c.m_ProjectileEnergyValue = thisEquipment.m_ProjectileEnergyValue;
//			//c.m_ProjectileToShootPrefab.name = c.m_ProjectileToShootPrefab.name;
//			returnEquipment = c;
//			break;
//		case EquipmentController.equipmentType.chassis:
//			ch = returnEquipment as ChassisController;
//			break;
//		case EquipmentController.equipmentType.engine:
//			e = returnEquipment as EngineController;
//			break;
//		case EquipmentController.equipmentType.hull:
//			h = returnEquipment as HullController;
//			break;
//		case EquipmentController.equipmentType.shield:
//			s = returnEquipment as ShieldController;
//			s.m_regenerationDelay = thisEquipment.m_regenerationDelay;
//			s.m_timeToFull = thisEquipment.m_timeToFull;
//			break;
//		}
//
//		return returnEquipment;
//
//	}
//
//	public static EquipmentSaveVersion GetCurrentEquipment(EquipmentController thisEquipment){
//		EquipmentSaveVersion returnEquipment = new EquipmentSaveVersion ();
//
//		CannonController c;
//		ChassisController ch;
//		EngineController e;
//		HullController h;
//		ShieldController s;
//
//		switch (thisEquipment.m_myType) {
//		case EquipmentController.equipmentType.cannon:
//			c = thisEquipment as CannonController;
//			returnEquipment.m_ProjectileEnergyValue = c.m_ProjectileEnergyValue;
//			returnEquipment.m_bulletName = c.m_ProjectileToShootPrefab.name;
//			break;
//		case EquipmentController.equipmentType.chassis:
//			ch = thisEquipment as ChassisController;
//			break;
//		case EquipmentController.equipmentType.engine:
//			e = thisEquipment as EngineController;
//			break;
//		case EquipmentController.equipmentType.hull:
//			h = thisEquipment as HullController;
//			break;
//		case EquipmentController.equipmentType.shield:
//			s = thisEquipment as ShieldController;
//			returnEquipment.m_regenerationDelay = s.m_regenerationDelay;
//			returnEquipment.m_timeToFull = s.m_timeToFull;
//			break;
//		}
//
//		returnEquipment.name = thisEquipment.name;
//		returnEquipment.equipType = thisEquipment.m_myType;
//		returnEquipment.m_equipmentLevel = thisEquipment.m_equipmentLevel;
//		returnEquipment.m_creditValue = thisEquipment.m_creditValue;
//		returnEquipment.m_damageType = thisEquipment.m_damageType;
//		returnEquipment.m_Owner = thisEquipment.m_Owner;
//
//		for (int i = 0; i < thisEquipment.m_baseValues.Length; i++) {
//			returnEquipment.m_baseValues[i] = thisEquipment.m_baseValues[i];
//			returnEquipment.m_ValueModifiers[i] = thisEquipment.m_ValueModifiers[i];
//		}
//
//
//
//		return returnEquipment;
//	}
//
//	public static void SavePlayerData(string path, PlayerData playerData)
//	{
//		var serializer = new XmlSerializer(typeof(PlayerData));
//		using(var stream = new FileStream(path, FileMode.Create))
//		{
//			serializer.Serialize(stream, playerData);
//		}
//	}
//
//	public static PlayerData LoadPlayerData(string path){
//
//		if (!File.Exists (path)) print ("NO FILE: " + path);
//
//
//		PlayerData playerData = new PlayerData ();
//
//		string attributeXml = string.Empty;
//		
//		XmlDocument xmlDocument = new XmlDocument();
//		xmlDocument.Load(path);
//		string xmlString = xmlDocument.OuterXml;
//		
//		using (StringReader read = new StringReader(xmlString))
//		{
//			System.Type outType = typeof(PlayerData);
//			
//			XmlSerializer serializer = new XmlSerializer(outType);
//			using (XmlReader reader = new XmlTextReader(read))
//			{
//				playerData = (PlayerData)serializer.Deserialize(reader);
//				reader.Close();
//			}
//			
//			read.Close();
//		}
//
//
//		return playerData;
//	}
//
//
//	public class PlayerData{
//		public float m_currentCredits = 0.0f;
//		public float m_experiencePoints = 0.0f;
//
//		[XmlArray("Equipment")]
//		public EquipmentSaveVersion[] m_equipments;
//	}
//
//	[XmlRoot("PlayerData")]
//	public class DataContainer{
//		public PlayerData playerData = _playerData;
//	}
//
//	public class EquipmentSaveVersion{
//		public string name;
//		public EquipmentController.equipmentType equipType;
//		public int m_equipmentLevel = 1;
//		public float m_creditValue = 1.0f;
//		public int m_damageType = 0;
//		public string m_Owner = "player";
//
//		//for cannons
//		public int m_ProjectileEnergyValue = 0;
//		public string m_bulletName = "";
//
//		//for shield 
//		public float m_regenerationDelay = 0.0f;
//		public float m_timeToFull = 0.0f;
//
//		[XmlArray("BaseValues")]
//		public float[] m_baseValues;
//		[XmlArray("Modifiers")]
//		public float[] m_ValueModifiers;
//
//		public EquipmentSaveVersion(){
//			m_baseValues = new float[7];
//			m_ValueModifiers = new float[7];
//		}
//	}

}