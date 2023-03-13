using UnityEngine;

public class MovingSoul : MonoBehaviour
{
     [SerializeField]
     private Transform target;
     [SerializeField]
     private int speed = 7;
     private void Update()
        { 
        Vector3 vectorToTarget = target.transform.position - transform.position;
        float angle = Mathf.Atan2(vectorToTarget.y, vectorToTarget.x) * Mathf.Rad2Deg ;
        Quaternion q = Quaternion.AngleAxis(angle, Vector3.forward);
        transform.rotation = Quaternion.Lerp(transform.rotation, q, Time.deltaTime * speed);
        transform.position = Vector2.MoveTowards(transform.position, (transform.position+transform.right), speed * Time.deltaTime);
        }
     // private void Update()
     // {
     //     Vector3 targetDirection = target.position - transform.position;

     //     float singleStep = _speed * Time.deltaTime;
     //     Vector3 newDirection = Vector3.RotateTowards(transform.forward, targetDirection, singleStep, 5);
     //     Debug.DrawRay(transform.position, newDirection, Color.red);
    
     //     transform.rotation = Quaternion.LookRotation(newDirection);
     //     transform.position += transform.forward * 0.01f;;
     // }
    
}
