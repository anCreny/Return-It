using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class FollowingBehaviour : MonoBehaviour
{
    private SphereBehaviour _sphere;

    private void Awake()
    {
        _sphere = GetComponentInParent<SphereBehaviour>();
        var randomAngle = Random.Range(-180, 180);
        transform.eulerAngles = new Vector3(0, 0, randomAngle);
    }

    private void OnCollisionEnter2D(Collision2D col)
    {
        _sphere.HasTouched();
    }

}
