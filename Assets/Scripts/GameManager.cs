using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using UnityEngine;
using UnityEngine.Serialization;

public class GameManager : MonoBehaviour
{
    private Vector2 spawnPoint = new (0, 3);
    [FormerlySerializedAs("shadow")] [SerializeField]
    private Animator shadowAnimator;
    [FormerlySerializedAs("ball")] [SerializeField]
    private Animator ballAnimator;
    [SerializeField] 
    private GameObject ball;
    [SerializeField] 
    private GameObject shadow;
    void Start()
    {
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        shadow.transform.position = spawnPoint;
        shadowAnimator.SetBool("StartShadow",true);
        ballAnimator.SetBool("StartFall",true);
        // ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
    }

    private void FixedUpdate()
    {
        ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Static;
        if ((shadowAnimator.GetCurrentAnimatorStateInfo(0).IsName("Shadow")))
        {
            ball.transform.position = spawnPoint;
            // Destroy(shadow);
        }
        if ((ballAnimator.GetCurrentAnimatorStateInfo(0).IsName("Falling")))
        {
            ball.GetComponent<Rigidbody2D>().bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
