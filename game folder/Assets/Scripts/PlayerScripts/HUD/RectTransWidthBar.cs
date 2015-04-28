using UnityEngine;
using System.Collections;

public class RectTransWidthBar : MonoBehaviour
{
    public UIBarType barType;
    public int maxWidth = 225;
    private RectTransform _rectTransform;
    private RectTransform RecTransform
    {
        get
        {
            if (_rectTransform == null)
            {
                _rectTransform = GetComponent<RectTransform>();
            }
            return _rectTransform;
        }
    }
    private Rect Rect { get { return RecTransform.rect; } }
    private IHPController _hpController;

    public void Init(IHPController hpcontroller)
    {
        _hpController = hpcontroller;
        hpcontroller.ValuesChanged += hpcontroller_ValuesChanged;
        hpcontroller.Died += hpcontroller_Died;
    }

    void hpcontroller_Died(object sender, DeathReasonEventArgs e)
    {
        UpdateBar();
    }

    void hpcontroller_ValuesChanged(object sender, System.EventArgs e)
    {
        UpdateBar();
    }

    protected void UpdateBar()
    {
        float normalizedValue = 0f;
        if (barType == UIBarType.HP)
        {
            normalizedValue = (_hpController.CurrentHP*(float) maxWidth/_hpController.PlayerStats.MaxHP);
        }
        else if (barType == UIBarType.Shield)
        {
            normalizedValue = (_hpController.CurrentShield*(float) maxWidth/_hpController.PlayerStats.MaxShield);
        }
        RecTransform.sizeDelta = new Vector2(Mathf.RoundToInt(normalizedValue), Rect.height);

    }
}
