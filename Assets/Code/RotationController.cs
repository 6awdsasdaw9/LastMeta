using UnityEngine;

//меняет и отслеживает направление взгляда персонажа

namespace Code
{
    public class RotationController
    {
        private readonly Transform character;

        public RotationController(Transform character)
        {
            this.character = character;
        }

        //направление взгляда
        public float GazeDirection()
        {
            return character.localScale.x / Mathf.Abs(character.localScale.x);
        }

        //управляем направлением взгляда (поворачиваем персонажа)
        internal void Turn(float dir)
        {
            if ((dir > 0 && !IsTurnedRight()) || (dir < 0 && IsTurnedRight()))
            {
                character.localScale *= new Vector2(-1, 1);
            }
        }

        //смотрит ли персонаж вправо
        bool IsTurnedRight()
        {
            if (GazeDirection() == 1)
                return true;
            else
                return false;
        }

        //поворачивает персонажа в сторону, противоположную от текущей
        internal void Flip()
        {
            Turn(GazeDirection() * -1);
        }
    }
}
