using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class shopBotton : MonoBehaviour {
    EquipmentData toDisplay;
    ShopMenu menu;

	// Use this for initialization
	void Start () {
	
	}
	
	// Update is called once per frame
	void Update () {
	
	}

    public void Init(EquipmentData toD)
    {
        toDisplay = toD;
        menu = FindObjectOfType<ShopMenu>();
        SetOnClick();
    }

    private void SetOnClick()
    {
        switch (toDisplay.m_myType)
        {
            case EquipmentController.equipmentType.cannon:
                gameObject.GetComponent<Button>().onClick.AddListener(UpdateInfo);
                break;
            case EquipmentController.equipmentType.chassis:
                gameObject.GetComponent<Button>().onClick.AddListener(UpdateInfo);
                break;
            case EquipmentController.equipmentType.engine:
                gameObject.GetComponent<Button>().onClick.AddListener(UpdateInfo);
                break;
            case EquipmentController.equipmentType.hull:
                gameObject.GetComponent<Button>().onClick.AddListener(UpdateInfo);
                break;
            case EquipmentController.equipmentType.shield:
                gameObject.GetComponent<Button>().onClick.AddListener(UpdateInfo);
                break;
        }
    }
    public void UpdateInfo()
    {
        //menu.CloseConfirmScreens();

        menu.UpdateInfo(toDisplay);
    }

}
