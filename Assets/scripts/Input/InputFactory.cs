using System;
using UnityEngine;

namespace GameInput
{
    public static class InputFactory
    {
        public static CustomInput GetInput()
        {
            if (Application.platform == RuntimePlatform.OSXEditor
               || Application.platform == RuntimePlatform.OSXPlayer
               || Application.platform == RuntimePlatform.WindowsEditor
               || Application.platform == RuntimePlatform.WindowsPlayer)
            {
                return new KeyboardInput();
            }
            throw new NotImplementedException("Not Supported Platform");
        }
    }
}