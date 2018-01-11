using System;
using UnityEngine;

namespace GameInput
{
    public abstract class CustomInput
    {
        protected ushort currentPlayer;
        protected string CurrentPlayer
        {
			get { return string.Format("Player{0}", currentPlayer);}
        }
        public abstract UserInput GetInput();

        public void Destroy()
        {
        }

        public CustomInput(ushort playerId)
        {
            currentPlayer = playerId;
        }
    }
}

