using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.Scripts.Player
{
    public class PlayerAttacking : MonoBehaviour
    {
        [SerializeField] private PlayerMovable _playerMovable;
        [SerializeField] private Animator _animator;
        [SerializeField] private AnimationEventsHandler _animationEventsHandler;
        [SerializeField] private float _animationAccelerationMultiplier;
        [SerializeField] private float _speedOnAttack;
        [SerializeField] private float _attackDistance;
        [SerializeField] private Transform _topBone;
        [SerializeField] private Transform _root;
        [SerializeField] private GameObject _weapon;
        [SerializeField] private GameObject _sword;
        [SerializeField] private Text _promt;

        private int _startAttackHashCode = Animator.StringToHash("startAttack");
        private int _hitAttackHashCode = Animator.StringToHash("hitAttack");

        private EnemyProximity _focusedEnemy;
        private EnemyHealth _attackedEnemy;
        private bool _readyFinishing = false;
        private bool _attack = false;

        private void Start()
        {
            _animationEventsHandler.Hitted += OnHit;
            _animationEventsHandler.EndedAttack += EndAttack;
            _promt.enabled = false;
            Application.targetFrameRate = 100;
        }

        private void Update()
        {
            if(_readyFinishing && !_attack)
            {
                if(Input.GetKeyDown(KeyCode.Space))
                {
                    Attack();
                }
            }
        }

        private void Attack()
        {
            _promt.gameObject.SetActive(false);
            _weapon.SetActive(false);
            _sword.SetActive(true);

            _attack = true;
            _playerMovable.enabled = false;

            _attackedEnemy = _focusedEnemy.Enemy;
            StartCoroutine(ApproachToEnemy());
        }

        private IEnumerator ApproachToEnemy()
        {
            _promt.enabled = false;
            _readyFinishing = false;

            Vector3 lookedEnemy = _attackedEnemy.transform.position;
            lookedEnemy.y = transform.position.y;
            _root.LookAt(lookedEnemy);
            _topBone.eulerAngles = Vector3.zero;

            if(Vector3.Distance(transform.position, _attackedEnemy.transform.position) > _attackDistance)
            {
                _animator.SetTrigger(_startAttackHashCode);
                _animator.speed = _animationAccelerationMultiplier;

            }

            while (Vector3.Distance(transform.position, _attackedEnemy.transform.position) > _attackDistance)
            {
                transform.position = Vector3.MoveTowards(transform.position,
                       _attackedEnemy.transform.position, _speedOnAttack * Time.deltaTime);
                yield return null;
            }

            _animator.speed = 1;
            _animator.SetTrigger(_hitAttackHashCode);
        }

        private void OnHit()
        {
            _attackedEnemy.QuickKill();
        }

        private void EndAttack()
        {
            _sword.SetActive(false);
            _weapon.SetActive(true);

            _attack = false;
            _playerMovable.enabled = true;

            _promt.gameObject.SetActive(true);
        }

        private void OnTriggerEnter(Collider other)
        {
            if (other.TryGetComponent(out _focusedEnemy))
            {
                _promt.enabled = true;
                _readyFinishing = true;
            }
        }

        private void OnTriggerExit(Collider other)
        {
            if (other.TryGetComponent(out EnemyProximity enemy))
            {
                if (enemy == _focusedEnemy)
                {
                    _focusedEnemy = null;
                    _promt.enabled = false;
                    _readyFinishing = false;
                }
            }
        }
    }
}