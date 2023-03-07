using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SphereFollowing : MonoBehaviour
{
    [SerializeField]
    private Transform followTarget;
    
    
    void LateUpdate()
    {
        transform.position = Vector2.MoveTowards(transform.position, followTarget.position, 2 * Time.deltaTime);
    }
}
