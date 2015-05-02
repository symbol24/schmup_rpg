using UnityEngine;
using System.Collections;

public class CannonConfirmMenu : Menu {

    CannonData m_toEquip;

    public void Init(EquipmentData toEquip)
    {
        if(toEquip.GetType() == typeof(CannonData))
            m_toEquip = (CannonData)toEquip;
    }

    public void CannonEquip(int cannonId){

    }

}
