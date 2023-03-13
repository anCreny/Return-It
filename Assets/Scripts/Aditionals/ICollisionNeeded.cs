using UnityEngine;

namespace Aditionals
{
    public interface ICollisionNeeded
    {
        public void DetectCollision(Collision2D collision);
    }
}