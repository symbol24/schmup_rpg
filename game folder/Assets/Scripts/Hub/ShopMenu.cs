using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine.UI;

public class ShopMenu : MonoBehaviour
{
    [SerializeField]
    GameObject m_buttonPrefab;

    List<GameObject> m_buttonList;

    List<EquipmentData> toSell = new List<EquipmentData>();

    List<EquipmentData> toBuy = new List<EquipmentData>();

    bool isToBuyCreated = false;

    [SerializeField]
    int amountToCreate;

    EquipmentData toConfirm;

    [SerializeField]
    GameObject btnConfirm;

    bool m_isSell = true;
    bool m_isDisplayed = false;

	// Use this for initialization
	void Start () {
	}

    void CreatEquips()
    {
        if (!isToBuyCreated)
        {
            for (int i = 0; i < amountToCreate; i++)
            {
                EquipmentController.equipmentType rand = StatCalculator.GetRandomValue<EquipmentController.equipmentType>();

                EquipmentData temp = null;

                switch (rand)
                {
                    case EquipmentController.equipmentType.cannon:
                        temp = ItemGenerator.Cannon(PlayerContainer.instance.M_level);
                        break;
                    case EquipmentController.equipmentType.chassis:
                        temp = ItemGenerator.Chassis(PlayerContainer.instance.M_level);
                        break;
                    case EquipmentController.equipmentType.engine:
                        temp = ItemGenerator.Engine(PlayerContainer.instance.M_level);
                        break;
                    case EquipmentController.equipmentType.hull:
                        temp = ItemGenerator.Hull(PlayerContainer.instance.M_level);
                        break;
                    case EquipmentController.equipmentType.shield:
                        temp = ItemGenerator.Shield(PlayerContainer.instance.M_level);
                        break;
                }

                toBuy.Add(temp);
            }
            isToBuyCreated = true;
        }
    }

    void CreateButtons()
    {
        if (m_buttonList != null) m_buttonList.DestroyChildren();

        m_buttonList = new List<GameObject>();

        RectTransform scRectTransform = this.GetComponent<RectTransform>();

        int i = 0;
        foreach (EquipmentData e in toSell)
        {
            GameObject btemp = Instantiate(m_buttonPrefab);
            
            btemp.name = e.m_equipmentName;
            RectTransform temp = btemp.GetComponent<RectTransform>();
            temp.SetParent(scRectTransform, false);
            Text txt = temp.transform.GetComponentInChildren<Text>();
            txt.text = e.m_equipmentName;
            btemp.GetComponent<shopBotton>().Init(e);

            m_buttonList.Add(btemp);

        }
    }

    public void UpdateInfo(EquipmentData toDisplay)
    {
        Text txt = GameObject.Find("equip_name").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentName;

        txt = GameObject.Find("equip_type").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_myType.ToString();

        txt = GameObject.Find("equip_level").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_equipmentLevel.ToString();

        txt = GameObject.Find("equip_price").GetComponentInChildren<Text>();
        txt.text = toDisplay.m_creditValue.ToString();

        toConfirm = toDisplay;


        if (CheckPurchase()) btnConfirm.GetComponent<Button>().interactable = true;
        else btnConfirm.GetComponent<Button>().interactable = false;
    }

    public void ConfirmSale()
    {
        PlayerContainer.instance.M_credits -= toConfirm.m_creditValue;
        PlayerContainer.instance.M_inventory.Add(toConfirm);

        toSell.Remove(toConfirm);

        CreateButtons();
    }

    public void ConfirmSell()
    {
        PlayerContainer.instance.M_credits = toConfirm.m_creditValue;
        PlayerContainer.instance.M_inventory.Remove(toConfirm);

        toSell.Add(toConfirm);

        CreateButtons();
    }

    private bool CheckPurchase(){
        bool ret = false;

        if (toConfirm != null && toConfirm.m_creditValue <= PlayerContainer.instance.M_credits) ret = true;

        return ret;
    }

    public void SwitchShop(bool isBuy)
    {
        if (isBuy)
            CreatEquips();
        else
            toSell = PlayerContainer.instance.M_inventory;

        CreateButtons();
    }
}
