using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class EnemyProximity : MonoBehaviour
    {
        [SerializeField] private EnemyHealth _enemy;

        public EnemyHealth Enemy => _enemy;
    }
}