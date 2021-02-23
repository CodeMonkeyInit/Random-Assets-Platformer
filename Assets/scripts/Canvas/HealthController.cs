using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;
using Checkpoint;
using Character;

namespace Canvas
{
    public class HealthController : MonoBehaviour
    {
        private Text healthCount;

        // Use this for initialization
        void Start()
        {
            healthCount = GetComponent<Text>();
        }

        // Update is called once per frame
        void Update()
        {
            int? lives = CustomCharacterController.activeCharacters?.Sum(character => character.Health);

            if (healthCount)
                healthCount.text = lives.ToString();
        }
    }
}