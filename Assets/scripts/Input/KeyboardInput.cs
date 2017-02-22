using System;
using UnityEngine;
using Character;

namespace GameInput
{
    public class KeyboardInput : CustomInput
    {
        public override UserInput GetInput()
        {
            UserInput input;

            input.move = 0;
            input.status = CharacterStatus.Normal;

            if(Input.GetKey(KeyCode.LeftArrow))
            {
                input.move = -1f; 
            }
            else if(Input.GetKey(KeyCode.RightArrow))
            {
                input.move = 1f;
            }

            if(Input.GetKey(KeyCode.DownArrow))
            {
                input.status = CharacterStatus.Crouched;
            } 
            else if(Input.GetKey(KeyCode.Space))
            {
                input.status = CharacterStatus.Jumping;
            }

            return input;
        }

    }
}

