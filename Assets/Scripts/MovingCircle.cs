
using System.Collections;
using System.Collections.Generic;
using UnityEngine;



public class MovingCircle : MonoBehaviour
{
    private int _frameLimit = 0;
    private int _frameLimit2 = 0;
    private Vector2 randomPoint = new Vector2(0,0);
    private void Update()
    {
        
        if (_frameLimit == 20)
        {
            var randomAndgle = Random.Range(-30,30);
            transform.eulerAngles = new Vector3(0, 0, randomAndgle + transform.eulerAngles.z);
            _frameLimit = 0;
            
        }

        if (_frameLimit2 == 1)
        {
            var hitCentre= Physics2D.Raycast(transform.position,transform.right,0.5f);
            var hitRight = Physics2D.Raycast(transform.position + transform.up * transform.localScale.x/2,transform.right,0.3f);
            var hitLeft = Physics2D.Raycast(transform.position + transform.up * -transform.localScale.x/2,transform.right,0.3f);
            
            if (hitCentre.point != Vector2.zero || hitRight.point != Vector2.zero || hitLeft.point != Vector2.zero);
            {
                while (hitCentre.point != Vector2.zero || hitRight.point != Vector2.zero || hitLeft.point != Vector2.zero)
                {
                    var angle = Random.Range(-180, 180);
                    transform.eulerAngles = new Vector3(0, 0, angle + transform.eulerAngles.z);
                    hitCentre = Physics2D.Raycast(transform.position, transform.right, 0.5f);
                    hitRight = Physics2D.Raycast(transform.position + transform.up * transform.localScale.x/2,transform.right,0.3f);
                    hitLeft = Physics2D.Raycast(transform.position + transform.up * -transform.localScale.x/2,transform.right,0.3f);
                    
                }
            }

            _frameLimit2 = 0;
        }

       
        transform.position = Vector2.MoveTowards(transform.position, (transform.position+transform.right), 2 * Time.deltaTime);
        
        _frameLimit++;
        _frameLimit2++;
    }
    void Awake()
    {
        Application.targetFrameRate = 60;
    }
}
