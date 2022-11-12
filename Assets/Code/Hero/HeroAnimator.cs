using System;
using Code.Logic;
using Test.Platformer_Toolkit_Character_Controller;
using UnityEngine;

namespace Code.Hero
{
    public class HeroAnimator : MonoBehaviour/*, IAnimationStateReader*/
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private characterMovement _hero;

        private readonly int Move_f = Animator.StringToHash("Speed");
        private readonly int Jump_b = Animator.StringToHash("Jump");
        private readonly int Dash_t = Animator.StringToHash("Dash");
        private readonly int Crouch_b = Animator.StringToHash("Crouch");
        private readonly int Attack_t = Animator.StringToHash("Attack");
        private readonly int AttackRanged_b = Animator.StringToHash("RangedAttack");
        private readonly int TopDamage_t = Animator.StringToHash("TopDamage");
        private readonly int Stunned_b = Animator.StringToHash("Stunned");
        private readonly int Death_t = Animator.StringToHash("Death");
        private readonly int DeathInWater_t = Animator.StringToHash("DeathInWater");

        private readonly int GunMode_b = Animator.StringToHash("Gun");
        

        public AnimatorState State { get; private set; }
        public bool IsAttacking => State == AnimatorState.Attack;

        private void Update()
        {
            if (_hero.onGround)
            {
                 _animator.SetFloat(Move_f, Math.Abs(_hero.velocity.x));
                _animator.SetBool(Jump_b,false);
                
            }
            else
            {
                _animator.SetBool(Jump_b,true);
                _animator.SetFloat(Move_f, 0);
            }
        }
        
        
        public void SwitchLayer(bool isReal)
        {
            if(isReal)
            {
                _animator.SetLayerWeight(0, 0);
                _animator.SetLayerWeight(1, 1);
            }
            else
            {
                _animator.SetLayerWeight(1, 0);
                _animator.SetLayerWeight(0, 1);
            }
        }
        
        
        /*
        public event Action<AnimatorState> StateEntered;
        public event Action<AnimatorState> StateExited;

        public void EnteredState(int stateHash)
        {
            throw new NotImplementedException();
        }

        public void ExitedState(int stateHash)
        {
            throw new NotImplementedException();
        }
        */

        /*
        public void PlayHit()
        {
            _animator.SetTrigger(HitHash);
        }

        public void PlayAttack()
        {
            _animator.SetTrigger(AttackHash);
        }

        public void PlayDeath()
        {
            _animator.SetTrigger(DieHash);
        }

        public void ResetToIdle()
        {
            _animator.Play(_idleStateHash, -1);
        }

        public void EnteredState(int stateHash)
        {
            State = StateFor(stateHash);
            StateEntered?.Invoke(State);
        }

        public void ExitedState(int stateHash)
        {
            StateExited?.Invoke(StateFor(stateHash));
        }

        private AnimatorState StateFor(int stateHash)
        {
            AnimatorState state;
            if (stateHash == _idleStateHash)
            {
                state = AnimatorState.Idle;
            }
            else if (stateHash == _attackStateHash)
            {
                state = AnimatorState.Attack;
            }
            else if (stateHash == _walkingStateHash)
            {
                state = AnimatorState.Walking;
            }
            else if (stateHash == _deathStateHash)
            {
                state = AnimatorState.Died;
            }
            else
            {
                state = AnimatorState.Unknown;
            }

            return state;
        }*/
    }
}