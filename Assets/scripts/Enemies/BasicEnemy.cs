using System;
using UnityEngine;
using GameObjects;

namespace Enemies
{
    public class BasicEnemy : InteractableGameObject, IEnemy
    {
        public virtual void Hurt()
        {
            throw new NotImplementedException();
        }
        protected override void OnCollisionEnter2D(Collision2D collision)
        {
            throw new NotImplementedException();
        }
    }
}