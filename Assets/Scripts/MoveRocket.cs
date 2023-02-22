
using System;
using UnityEngine;

public class MoveRocket : MonoBehaviour
{
    [SerializeField]
    private Transform rocket;

    private Vector3 _mousePos;
    private float speed = 20f;
    private void OnMouseDrag(){
        var currentMousePos = Camera.main.ScreenToWorldPoint (Input.mousePosition);
        rocket.position = Vector2.MoveTowards(rocket.position, new Vector2(currentMousePos.x,currentMousePos.y),speed*Time.deltaTime);
        var currentVector = new Vector2(currentMousePos.x - _mousePos.x, currentMousePos.y - _mousePos.y).normalized;
        var angle = Vector3.Angle(rocket.TransformDirection(Vector3.up), currentVector);
        Debug.Log(rocket.eulerAngles);
        if (angle != 0)
        {
            var vectorRotation = new Vector3(0, 0, angle);
            rocket.eulerAngles = vectorRotation; 
        }
        
        _mousePos = currentMousePos;
    }

    // private void Update()
    // {
    //
    // }
}
