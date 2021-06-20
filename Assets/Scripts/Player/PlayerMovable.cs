using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts.Player
{
    public class PlayerMovable : MonoBehaviour
    {
        [SerializeField] private float _speed;
        [SerializeField] private float _animationRotationYOffset;
        [SerializeField] private Animator _animator;
        [SerializeField] private Transform _targetBone;
        [SerializeField] private Transform _forwardPoint;
        [SerializeField] private Transform _main;

        private int _speedHashCode = Animator.StringToHash("isRun");

        private Vector3 _moveDirection;

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

        private void Update()
        {
            _moveDirection = new Vector3(Input.GetAxis("Horizontal"), 0, Input.GetAxis("Vertical"));
            _moveDirection = _moveDirection.normalized;
            transform.LookAt(transform.position + _moveDirection);
            _main.Translate(_moveDirection * _speed * Time.deltaTime);
            _animator.SetBool(_speedHashCode, _moveDirection.magnitude > 0);
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
            _currentRotation.y += rotationAngle - transform.eulerAngles.y + _animationRotationYOffset;
            _targetBone.eulerAngles = _currentRotation;
        }
    }
}