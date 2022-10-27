using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Script.Infrastructure
{
    public class GameBootstrapper : MonoBehaviour
    {
        private Game _game;

        private void Awake()
        {
            _game = new Game();
            DontDestroyOnLoad(this);
        }
    }
}