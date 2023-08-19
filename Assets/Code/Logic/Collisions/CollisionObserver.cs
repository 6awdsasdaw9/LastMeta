using System;
using UnityEngine;

namespace Code.Logic.Collisions
{
    public abstract class CollisionObserver : MonoBehaviour
    {
        public  Action<GameObject> OnEnter;
        public  Action<GameObject> OnExit;
    }
}