using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using GameInput;
using Character;
using UnityStandardAssets.CrossPlatformInput;

namespace GameInput
{
    public struct TouchConstraints
    {
        public float minX;
        public float maxX;
        public float minY;
        public float maxY;

        public bool TouchIsIn(Touch touch)
        {
            if (touch.position.x > minX &&
                touch.position.x < maxX &&
                touch.position.y > minY &&
                touch.position.y < maxY)
            {
                return true;
            }
            else
            {
                return false;
            }
        }
    }

    public class TouchInput : CustomInput
    {
        private Joystick joystick;

        public TouchConstraints touchConstraints;

        public void ShowStick()
        {
            bool joystickIsActive = false; 

            foreach (Touch touch in Input.touches)
            {
                //FIXME after debug
                if (touchConstraints.TouchIsIn(touch))
                {
                    if (joystick != null)
                    {
                        if (!joystick.gameObject.activeSelf)
                        {
                            joystick.OnEnable();
                            joystick.transform.position = touch.position;
                            joystick.startPos = touch.position.toVector3();
                        }

                        joystickIsActive = true;
                    }
                }
            }
            if (!joystickIsActive)
            {
                joystick.OnDisable();
            }
        }

        public override UserInput GetInput()
        {
            UserInput input;

            input.move = CrossPlatformInputManager.GetAxis("Horizontal" + CurrentPlayer);
            input.status = CharacterStatus.Normal;

            if (CrossPlatformInputManager.GetAxis("Vertical" + CurrentPlayer) < -.7f)
            {
                input.status = CharacterStatus.Crouched;
            }
            else if (CrossPlatformInputManager.GetButtonDown("Jump" + CurrentPlayer))
            {
                input.status = CharacterStatus.Jumping;
            }

            return input;
        }

        public TouchInput(GameObject mobileInput)
        {
            joystick = (GameObject.Instantiate(mobileInput) as GameObject).GetComponentInChildren<Joystick>();
        }
    }
}