using System;
using UnityEngine;

namespace Enemies
{
    public class Frog : MovingEnemy
    {
        [SerializeField]
        private float jumpForce;

        protected override void Move()
        {
            throw new NotImplementedException();
        }
    }
}

