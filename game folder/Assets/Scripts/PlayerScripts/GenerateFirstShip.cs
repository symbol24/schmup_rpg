using UnityEngine;
using System.Collections;

public class GenerateFirstShip : MonoBehaviour {

	// Use this for initialization
	void Start () {
        if (!PlayerContainer.instance.M_isFirstShipGenerated)
        {
            PlayerContainer.instance.M_Cannons = GenerateCannons();
            PlayerContainer.instance.M_Shield = ItemGenerator.Shield(PlayerContainer.instance.M_level);
            PlayerContainer.instance.M_chassis = ItemGenerator.Chassis(PlayerContainer.instance.M_level);
            PlayerContainer.instance.M_OtherEquipment = GenerateOtherEquipment();
            PlayerContainer.instance.M_isFirstShipGenerated = true;
        }
	}

    CannonData[] GenerateCannons()
    {
        CannonData[] ret = new CannonData[2];

        ret[0] = ItemGenerator.Cannon(PlayerContainer.instance.M_level);
        ret[1] = ItemGenerator.Cannon(PlayerContainer.instance.M_level);

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
