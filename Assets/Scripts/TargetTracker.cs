using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class TargetTracker : MonoBehaviour
    {
        [SerializeField] private Transform _target;
        [SerializeField] private float _maxSpeed;
        [SerializeField] private float _minSpeed;

        [SerializeField] private float _maxDistance;

        private float _currentSpeed;
        private float _deltaSpeed;

        private void Start()
        {
            _deltaSpeed = _maxSpeed - _minSpeed;
        }

        private void Update()
        {
            _currentSpeed = _minSpeed + 
                _deltaSpeed * (Vector3.Distance(_target.position, transform.position) / _maxDistance);

            transform.position = Vector3.MoveTowards(transform.position,
                _target.position, _currentSpeed * Time.deltaTime);
        }
    }
}