using System;
using UnityEngine;
using Character;

namespace GameInput
{
    public class PhysicalInput : CustomInput
    {
        public override UserInput GetInput()
        {
            UserInput input;

            input.move = Input.GetAxis("Horizontal" + CurrentPlayer);
            input.status = CharacterStatus.Normal;

            if (Input.GetKeyUp(KeyCode.Escape))
                Application.Quit();

            if (Input.GetAxis("Vertical" + CurrentPlayer) < 0)
            {
                input.status = CharacterStatus.Crouched;
            }
            else if (Input.GetButtonDown("Jump" + CurrentPlayer))
            {
                input.status = CharacterStatus.Jumping;
            }

            return input;
        }

        public PhysicalInput(ushort playerId) : base(playerId)
        {
            
        }
    }
}

