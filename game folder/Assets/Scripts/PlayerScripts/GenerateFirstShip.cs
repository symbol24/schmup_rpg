using UnityEngine;
using System.Collections;

public class GenerateFirstShip : MonoBehaviour {
    [SerializeField]
    private string m_cannon1;
    [SerializeField]
    private string m_bullet1;
    [SerializeField]
    private string m_cannon2;
    [SerializeField]
    private string m_bullet2;
    [SerializeField]
    private string m_chassis;
    [SerializeField]
    private string m_hull;
    [SerializeField]
    private string m_engine;
    [SerializeField]
    private string m_shield;

	// Use this for initialization
	void Start () {
        if (!PlayerContainer.instance.M_isFirstShipGenerated)
        {
            PlayerContainer.instance.M_Cannons = GenerateCannons();
            PlayerContainer.instance.M_Shield = ItemGenerator.Shield(PlayerContainer.instance.M_level);
            PlayerContainer.instance.M_chassis = ItemGenerator.ChassisWithSpecificPrefab(PlayerContainer.instance.M_level, m_chassis);
            PlayerContainer.instance.M_OtherEquipment = GenerateOtherEquipment();
            PlayerContainer.instance.M_isFirstShipGenerated = true;
        }
	}

    CannonData[] GenerateCannons()
    {
        CannonData[] ret = new CannonData[2];

        ret[0] = ItemGenerator.CannonWithSpecificPrefab(PlayerContainer.instance.M_level, m_cannon1, m_bullet1);
        ret[1] = ItemGenerator.CannonWithSpecificPrefab(PlayerContainer.instance.M_level, m_cannon2, m_bullet2);

        return ret;
    }

    EquipmentData[] GenerateOtherEquipment()
    {
        EquipmentData[] ret = new EquipmentData[2];

        ret[0] = ItemGenerator.Engine(PlayerContainer.instance.M_level);
        ret[1] = ItemGenerator.Hull(PlayerContainer.instance.M_level);

        return ret;
    }
}
