using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FollowingBehaviour : MonoBehaviour
{
    private SphereBehaviour _sphere;
    
    private void Awake()
    {
        _sphere = GetComponentInParent<SphereBehaviour>();
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _sphere.HasTouched();
    }

}
