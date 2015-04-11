using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class DummyCollider : MonoBehaviour
{
    public Collider2D theCollider { get; private set; }
    public event EventHandler<Collider2DEventArgs> ColliderEntered;
	// Use this for initialization
	void Start ()
	{
	    theCollider = GetComponent<Collider2D>();
	    if (theCollider == null) Debug.LogError(string.Format("NO COLLIDER FOUND{0}", gameObject.name));
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (ColliderEntered != null) ColliderEntered(this, new Collider2DEventArgs {Collider = coll});
    }

}
