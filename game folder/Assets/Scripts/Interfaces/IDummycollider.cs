using System;
using UnityEngine;
using System.Collections;

public interface IDummyCollider 
{
    Collider2D theCollider { get; }
    event EventHandler<Collider2DEventArgs> ColliderEntered;
}
