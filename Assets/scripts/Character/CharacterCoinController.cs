using System.Collections;
using System.Collections.Generic;
using UnityEngine;


namespace Coins
{
    public class CharacterCoinController
    {
        private static uint coinCount;
        public static uint CoinCount { get { return coinCount / 3;} }

        public void AddCoins(uint count)
        {
            coinCount += count;
        }

        public void Reset()
        {
            coinCount = 0;
        }

        public CharacterCoinController()
        {
            Reset();
        }
    }
}