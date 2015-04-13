using UnityEngine;
using System.Collections;

public abstract class SpriteScaleBarBase : MonoBehaviour 
{
    public GameObject _objectToScale;
    private Vector3 originalObjectScale;
    protected void Init()
    {
        originalObjectScale = _objectToScale.transform.localScale.Clone();
    }

    protected void UpdateBar(float currentValue, float maxValue)
    {
        float percent = (currentValue / maxValue) * originalObjectScale.x;
        var newScale = new Vector3(percent, _objectToScale.transform.localScale.y, _objectToScale.transform.localScale.z);
        _objectToScale.transform.localScale = newScale;
    }
}
