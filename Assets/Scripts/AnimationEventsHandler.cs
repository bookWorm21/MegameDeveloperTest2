using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Assets.Scripts
{
    public class AnimationEventsHandler : MonoBehaviour
    {
        public event System.Action Hitted;
        public event System.Action EndedAttack;

        public void Hit()
        {
            Hitted?.Invoke();
        }

        public void EndAttack()
        {
            EndedAttack?.Invoke();
        }
    }
}