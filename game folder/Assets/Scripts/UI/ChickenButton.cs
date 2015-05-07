using UnityEngine;
using System.Collections;

public class ChickenButton : MonoBehaviour {

    public GameObject chickenImg;

    public void GiveChicken()
    {
        chickenImg.SetActive(true);
        CannonData chicken = ItemGenerator.CannonWithSpecificPrefab(PlayerContainer.instance.M_level, "cannon_Test_type_1", "Player_turkey_bullet");
        PlayerContainer.instance.M_inventory.Add(chicken);
    }
}
