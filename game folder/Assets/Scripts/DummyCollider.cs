using System;
using UnityEngine;
using System.Collections;

[RequireComponent(typeof(Collider2D))]
public class DummyCollider : MonoBehaviour
{
    private Collider2D theCollider;
    public event EventHandler<Collider2DEventArgs> ColliderEntered;
	// Use this for initialization
	void Start ()
	{
	    theCollider = GetComponent<Collider2D>();
	}

    private void OnTriggerEnter2D(Collider2D coll)
    {
        if (ColliderEntered != null) ColliderEntered(this, new Collider2DEventArgs {Collider = coll});
    }

}
