using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CharacterCoinController
{
    private uint coinCount;
    public uint CoinCount { get { return coinCount / 3;} }

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
