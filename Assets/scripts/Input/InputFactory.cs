using System;
using UnityEngine;

namespace GameInput
{
    public static class InputFactory
    {
        public static CustomInput GetInput(int playerId, GameObject mobileInput = null)
        {
            if (Application.platform == RuntimePlatform.OSXEditor
                || Application.platform == RuntimePlatform.OSXPlayer
                || Application.platform == RuntimePlatform.WindowsEditor
                || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return new PhysicalInput((ushort)playerId);
            }
            else if (Application.platform == RuntimePlatform.Android
                     || Application.platform == RuntimePlatform.IPhonePlayer)
            {
                return new TouchInput((ushort) playerId, mobileInput);
            }

            throw new NotImplementedException("Unsupported Platform");
        }
    }
}