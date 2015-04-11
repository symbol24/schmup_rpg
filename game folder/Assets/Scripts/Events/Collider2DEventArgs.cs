using System;
using UnityEngine;
using System.Collections;

public class Collider2DEventArgs : EventArgs
{
    public Collider2D Collider { get; set; }
}
