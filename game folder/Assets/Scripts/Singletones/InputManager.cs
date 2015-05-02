using UnityEngine;
using System.Collections;

public class InputManager : MonoBehaviour, iInputManager
{
    private static iInputManager iInstance;

    public static iInputManager instance
    {
        get { return iInstance ?? (iInstance = new DummyInputManager()); }
    }

    void Awake()
    {
        if (iInstance == null || iInstance is DummyInputManager)
        {
            iInstance = this;

            DontDestroyOnLoad(this.gameObject);
        }
        else
        {
            Destroy(this.gameObject);
        }
    }

    public float M_VertValue;
    public float M_HorValue;
    public float M_firebutton;
    public float M_altFireButton;
    public float M_switchButtons;
    public float M_pauseButton;
    public float M_backButton;

	
	// Update is called once per frame
	void Update () {
        //get both controller and keyboard axis's
        m_VertValue = Input.GetAxis("Vertical");
        m_HorValue = Input.GetAxis("Horizontal");
        m_firebutton = Input.GetAxis("Fire");
        m_altFireButton = Input.GetAxis("Alt Fire");
        m_switchButtons = Input.GetAxis("Switch");
        m_pauseButton = Input.GetAxis("Pause");
        m_backButton = Input.GetAxis("Back");
	}

    public float m_VertValue
    {
        get
        {
            return M_VertValue;
        }
        set
        {
            M_VertValue = value;
        }
    }

    public float m_HorValue
    {
        get
        {
            return M_HorValue;
        }
        set
        {
            M_HorValue = value;
        }
    }

    public float m_firebutton
    {
        get
        {
            return M_firebutton;
        }
        set
        {
            M_firebutton = value;
        }
    }

    public float m_altFireButton
    {
        get
        {
            return M_altFireButton;
        }
        set
        {
            M_altFireButton = value;
        }
    }

    public float m_switchButtons
    {
        get
        {
            return M_switchButtons;
        }
        set
        {
            M_switchButtons = value;
        }
    }

    public float m_pauseButton
    {
        get
        {
            return M_pauseButton;
        }
        set
        {
            M_pauseButton = value;
        }
    }

    public float m_backButton
    {
        get
        {
            return M_backButton;
        }
        set
        {
            M_backButton = value;
        }
    }
}

public interface iInputManager
{
    float m_VertValue { get; set; }
    float m_HorValue { get; set; }
    float m_firebutton { get; set; }
    float m_altFireButton { get; set; }
    float m_switchButtons { get; set; }
    float m_pauseButton { get; set; }
    float m_backButton { get; set; }
}

public class DummyInputManager : iInputManager
{

    public float m_VertValue
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float m_HorValue
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float m_firebutton
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float m_altFireButton
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float m_switchButtons
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float m_pauseButton
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }

    public float m_backButton
    {
        get
        {
            throw new System.NotImplementedException();
        }
        set
        {
            throw new System.NotImplementedException();
        }
    }
}