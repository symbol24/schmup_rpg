using UnityEngine;
using System.Collections;
using UnityEngine.UI;

public class NavigationButton : MonoBehaviour
{
    private MissionGenerator m_generator;
    private Text[] m_texts = new Text[5];
    private MissionData m_myMission;

    public void Init()
    {
        m_generator = FindObjectOfType<MissionGenerator>();
        m_texts[0] = GameObject.Find("coordinates").GetComponent<Text>();
        m_texts[1] = GameObject.Find("Difficulty").GetComponent<Text>();
        m_texts[2] = GameObject.Find("expValue").GetComponent<Text>();
        m_texts[3] = GameObject.Find("creditValue").GetComponent<Text>();
        m_texts[4] = GameObject.Find("reward").GetComponent<Text>();
        m_myMission = GetMission();
        SetOnClick();
    }

    public void UpdateInfo()
    {
        m_texts[0].text = m_myMission.m_missionCoordinates;

        string difficulty = m_myMission.m_difficulty.ToString();
        string exp = m_myMission.m_experienceValue.ToString();
        string credit = m_myMission.m_creditReward.ToString();
        string reward;



        if (m_myMission.m_rewardEquipment.Length > 0)
            reward = m_myMission.m_rewardEquipment[0].m_equipmentName;
        else
            reward = "None";

        if (m_myMission.m_MissionType == MissionController.MissionType.exploration)
        {
            difficulty = "unknown";
            exp = "unknown";
            credit = "unknown";
            reward = "unknown";
        }
        
        m_texts[1].text = difficulty;
        m_texts[2].text = exp;
        m_texts[3].text = credit;
        m_texts[4].text = reward;
    }

    private MissionData GetMission()
    {
        MissionData ret = null;

        foreach (MissionData md in m_generator.m_missions)
        {
            if (md.m_missionCoordinates == gameObject.name) ret = md;
        }

        return ret;
    }

    private void SelectMission()
    {
        m_generator.SetSelectedMission(m_myMission);
        Application.LoadLevel("phil_test");
    }

    private void SetOnClick()
    {
        gameObject.GetComponent<Button>().onClick.AddListener(SelectMission);
    }

}