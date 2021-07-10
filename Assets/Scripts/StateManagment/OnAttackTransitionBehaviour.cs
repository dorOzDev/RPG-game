using RPG.Combat;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

namespace RPG.StateManagment
{
    public class OnAttackTransitionBehaviour : StateMachineBehaviour
    {

        public override void OnStateEnter(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Fighter fighter = animator.GetComponent<Fighter>();
            fighter.SetWeaponActivity(true);
        }

        public override void OnStateExit(Animator animator, AnimatorStateInfo stateInfo, int layerIndex)
        {
            Fighter fighter = animator.GetComponent<Fighter>();
            fighter.SetWeaponActivity(false);
        }
    }
}

