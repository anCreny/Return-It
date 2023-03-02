using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

public class WallPosition : MonoBehaviour
{
    [SerializeField] 
    private Transform wall1;
    [SerializeField] 
    private Transform wall2;
    
    void Start()
    { 
        var screenPosLeft = Camera.main.ScreenToWorldPoint( new Vector3(0,0,0)).x-0.5f;
        wall1.position = new Vector2(screenPosLeft, wall1.position.y);
        var screenPosRight = Camera.main.ScreenToWorldPoint( new Vector3(Camera.main.pixelWidth,0,0)).x+0.5f;
        wall2.position = new Vector2(screenPosRight, wall2.position.y);
    }

}
