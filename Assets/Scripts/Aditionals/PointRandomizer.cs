using UnityEngine;

namespace Aditionals
{
    public class PointRandomizer
    {
        private float _screenHeight;
        private float _screenWidth;

        private float _leftBorder;
        private float _rightBorder;
        private float _topBorder;
        private float _bottomBorder;

        private Camera _camera;

        public PointRandomizer(Camera camera, float screenHeight, float screenWidth, float top, float bottom, float left, float right)
        {
            _screenHeight = screenHeight;
            _screenWidth = screenWidth;

            _leftBorder = _screenWidth * left;
            _rightBorder = _screenWidth * right;
            _topBorder = _screenHeight * top;
            _bottomBorder = _screenHeight * bottom;

            _camera = camera;
        }

        public Vector2 GetRandomPoint()
        {
            var Xaxis = UnityEngine.Random.Range(_leftBorder, _rightBorder);
            var Yaxis = UnityEngine.Random.Range(_bottomBorder, _topBorder);

            var point = _camera.ScreenToWorldPoint(new Vector2(Xaxis, Yaxis));
            return point;

        }

        public Vector2 GetRandomPointExclude(Transform excluder)
        {
            var randomPoint = GetRandomPoint();

            var leftExcluderBorder = excluder.position.x - excluder.localScale.x;
            var rightExcluderBorder = excluder.position.x + excluder.localScale.x;
            var topExcluderBorder = excluder.position.y + excluder.localScale.y;
            var bottomExcluderBorder = excluder.position.y - excluder.localScale.y;


            
            while (!((randomPoint.x > rightExcluderBorder || randomPoint.x < leftExcluderBorder) ||
                   (randomPoint.y > topExcluderBorder || randomPoint.y < bottomExcluderBorder)))
            {
                randomPoint = GetRandomPoint();
            }

            return randomPoint;
        }
    }
}