﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Character;
using LevelGenerator;

namespace GameInput
{
    public class CustomCharacterInputController : MonoBehaviour
    {
        private CustomInput input;

        public MainCharacter character;

        private void GetMainCharacter()
        {
            character = GameObject.FindObjectOfType<MainCharacter>();
        }
        // Use this for initialization
        void Start()
        {
            input = InputFactory.GetInput();
            GenerateLevel.OnMainCharacterCreated += new OnCharacterCreation(GetMainCharacter);
        }

        // Update is called once per frame
        void Update()
        {
            if (character != null)
            {
                UserInput userInput = input.GetInput();
                character.Move(userInput.move, userInput.status);
            }
        }
    }

}