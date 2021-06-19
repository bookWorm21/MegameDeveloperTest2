using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyHealth : MonoBehaviour
    {
        [SerializeField] private Transform _main;
        [SerializeField] private Animator _animator;
        [SerializeField] private Collider _mainCollider;
        [SerializeField] private Collider _proximityCollider;
        [SerializeField] private float _respawnTime;

        [SerializeField] private float _maxXSpawn;
        [SerializeField] private float _minXSpawn;
        [SerializeField] private float _maxZSpawn;
        [SerializeField] private float _minZSpawn;

        private Rigidbody[] _rigidbodies;

        private void Start()
        {
            _rigidbodies = GetComponentsInChildren<Rigidbody>();
            SwitchPhysicRagdoll(false);
        }

        private void SwitchPhysicRagdoll(bool active)
        {
            _animator.enabled = !active;
            _mainCollider.enabled = !active;
            foreach(var rigidbody in _rigidbodies)
            {
                rigidbody.isKinematic = !active;
            }
        }

        private IEnumerator Respawn()
        {
            yield return new WaitForSeconds(_respawnTime);
            gameObject.SetActive(false);
            float x = Random.Range(_minXSpawn, _maxXSpawn);
            float z = Random.Range(_minZSpawn, _maxZSpawn);
            _main.position = new Vector3(x, 0, z);
            SwitchPhysicRagdoll(false);
            gameObject.SetActive(true);
        }

        public void QuickKill()
        {
            SwitchPhysicRagdoll(true);
            _proximityCollider.enabled = false;
            StartCoroutine(Respawn());
            _proximityCollider.enabled = true;
        }
    }
}