using System;
using UnityEngine;

namespace GameInput
{
    public static class InputFactory
    {
        public static CustomInput GetInput(GameObject mobileInput = null)
        {
            if (Application.platform == RuntimePlatform.OSXEditor
                || Application.platform == RuntimePlatform.OSXPlayer
                || Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return new PhysicalInput();
            }
            else if (Application.platform == RuntimePlatform.Android
                     || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return new TouchInput(mobileInput);
            }

            throw new NotImplementedException("UnSupported Platform");
        }
    }
}