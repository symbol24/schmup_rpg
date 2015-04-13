using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class DummyCollider : MonoBehaviour, IDummyCollider
{
    private Collider2D _thecollider;
    public Collider2D theCollider
    {
        get
        {
            if (_thecollider == null)
            {
                _thecollider = GetComponent<Collider2D>();
                if (_thecollider == null) Debug.LogError(string.Format("NO COLLIDER FOUND{0}", gameObject.name));
            }
            return _thecollider;
        }
    }

    public event EventHandler<Collider2DEventArgs> ColliderEntered;

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (ColliderEntered != null) ColliderEntered(this, new Collider2DEventArgs {Collider = coll});
    }

}
