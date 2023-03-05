using UnityEngine;

namespace Aditionals
{
    public struct ObjectTransform
    {
        public float leftBorder;
        public float rightBorder;
        public float topBorder;
        public float bottomBorder;
        
        public ObjectTransform(Transform transform)
        {
            var position = transform.position;
            var localScale = transform.localScale;
            
            leftBorder = position.x - localScale.x;
            rightBorder = position.x + localScale.x;
            topBorder = position.y + localScale.y;
            bottomBorder = position.y - localScale.y;
        }

        public ObjectTransform(float scaleX, float scaleY, Vector2 point)
        {
            leftBorder = point.x - scaleX;
            rightBorder = point.x + scaleX;
            topBorder = point.y + scaleY;
            bottomBorder = point.y - scaleY;
        }
    }
}