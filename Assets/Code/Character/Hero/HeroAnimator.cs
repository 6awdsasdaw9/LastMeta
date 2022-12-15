using System;
using Code.Logic;
using UnityEngine;

namespace Code.Character.Hero
{
    [RequireComponent(typeof(Animator), typeof(HeroMovement))]
    public class HeroAnimator : MonoBehaviour
    {
        [SerializeField] private Animator _animator;
        [SerializeField] private HeroMovement _hero;
        [SerializeField] private CharacterCollision _collision;

        private readonly int Move_f = Animator.StringToHash("Speed");
        private readonly int Jump_b = Animator.StringToHash("Jump");
        private readonly int Dash_t = Animator.StringToHash("Dash");
        private readonly int Crouch_b = Animator.StringToHash("Crouch");

        private readonly int GunMode_b = Animator.StringToHash("Gun");


        private void Update()
        {
            if (_collision.onGround)
            {
                PlayMove();
                _animator.SetBool(Jump_b, false);
            }
            else
            {
                _animator.SetBool(Jump_b, true);
                _animator.SetFloat(Move_f, 0);
            }

            PlayCrouch();
        }

        private void PlayMove()
        {
            _animator.SetFloat(Move_f, Math.Abs(_hero.GetVelocity().x));
        }

        private void PlayCrouch()
        {
            if (_hero.isCrouch)
                _animator.SetBool(Crouch_b, true);
            else
                _animator.SetBool(Crouch_b, false);
        }

        public void SwitchLayer(bool isReal)
        {
            if (isReal)
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
    }
}