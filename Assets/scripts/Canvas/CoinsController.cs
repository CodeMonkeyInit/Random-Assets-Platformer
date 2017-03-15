using UnityEngine;
using Coins;
using UnityEngine.UI;

public class CoinsController : MonoBehaviour
{
    private Text coinCount;

    void Start()
    {
        coinCount = GetComponent<Text>();
    }
    // Update is called once per frame
    void Update()
    {
        coinCount.text = CharacterCoinController.CoinCount.ToString();
    }
}
