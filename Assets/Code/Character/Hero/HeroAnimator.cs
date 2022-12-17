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

        private readonly int Move_b = Animator.StringToHash("Move");
        private readonly int Direction_f = Animator.StringToHash("Direction");
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

            _animator.SetBool(Move_b,false);
            }

            PlayCrouch();
        }

        private void PlayMove()
        {
            _animator.SetFloat(Direction_f, _hero.directionX);
            _animator.SetBool(Move_b, _hero.directionX != 0);
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