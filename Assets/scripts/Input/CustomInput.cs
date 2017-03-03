using System;
using UnityEngine;

namespace GameInput
{
    public abstract class CustomInput
    {
        protected static ushort playersCount;
        protected static ushort currentPlayer;
        protected string CurrentPlayer
        {
            get { return "Player" + currentPlayer;}
        }
        public abstract UserInput GetInput();

        public void Destroy()
        {
            playersCount--;
        }

        public CustomInput()
        {
            playersCount++;
            currentPlayer = playersCount;
        }
    }
}

