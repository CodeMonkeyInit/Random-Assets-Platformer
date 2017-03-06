using System;
using UnityEngine;

namespace PlatformerEnemies
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