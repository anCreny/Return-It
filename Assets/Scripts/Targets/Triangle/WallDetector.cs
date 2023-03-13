using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WallDetector : MonoBehaviour
{
    private BlackHore _blackHore;

    private void Awake()
    {
        _blackHore = GetComponentInParent<BlackHore>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _blackHore.TouchWall();
    }
}
