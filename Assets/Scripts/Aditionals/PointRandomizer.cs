using System.Collections.Generic;
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

        private List<ObjectTransform> _excludingZone = new ();

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

        public void ResetExcludingZone()
        {
            _excludingZone = new ();
        }

        public void IncreaseExcludingZone(ObjectTransform newZone)
        {
            _excludingZone.Add(newZone);
        }

        private Vector2 GetNewRandomPoint()
        {
            var Xaxis = Random.Range(_leftBorder, _rightBorder);
            var Yaxis = Random.Range(_bottomBorder, _topBorder);

            var point = _camera.ScreenToWorldPoint(new Vector2(Xaxis, Yaxis));
            return point;

        }

        public Vector2 GetRandomPoint()
        {
            var randomPoint = GetNewRandomPoint();

            while (CheckZone(randomPoint))
            {
                randomPoint = GetNewRandomPoint();
            }

            return randomPoint;
        }

        private bool CheckZone(Vector2 point)
        {
            var result = false;
            foreach (var zone in _excludingZone)
            {
                result = !((point.x > zone.rightBorder || point.x < zone.leftBorder) ||
                           (point.y > zone.topBorder || point.y < zone.bottomBorder));
                if (result)
                {
                    break;
                }
            }

            return result;
        }
    }
}