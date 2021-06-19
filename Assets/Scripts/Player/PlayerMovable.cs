using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovable : MonoBehaviour
    {
        [SerializeField] private Transform _targetBone;
        [SerializeField] private Transform _forwardPoint;

        private Camera _mainCamera;
        private Vector2 _playerScreenPosition;
        private Vector2 _forwardAngleAxis;
        private Vector3 _currentRotation;

        private void Start()
        {
            _mainCamera = Camera.main;
            _playerScreenPosition = _mainCamera.WorldToScreenPoint(transform.position);
            _forwardAngleAxis = (Vector2)_mainCamera.WorldToScreenPoint(_forwardPoint.position) - _playerScreenPosition;
            _forwardAngleAxis = _forwardAngleAxis.normalized;
        }

        private void LateUpdate()
        {
            Vector2 needRotation = (Vector2)Input.mousePosition - _playerScreenPosition;
            float rotationAngle = Vector2.Angle(needRotation, _forwardAngleAxis);
            if (Vector3.Cross(needRotation ,_forwardAngleAxis).z < 0)
            {
                rotationAngle *= -1;
            }

            _currentRotation = _targetBone.eulerAngles;
            _currentRotation.y += rotationAngle;
            _targetBone.eulerAngles = _currentRotation;
        }
    }
}