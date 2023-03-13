using Aditionals;
using UnityEngine;

public class CollisionDetector : MonoBehaviour
{
    private void OnCollisionEnter2D(Collision2D col)
    {
        GetComponentInParent<ICollisionNeeded>().DetectCollision(col);
    }
}
