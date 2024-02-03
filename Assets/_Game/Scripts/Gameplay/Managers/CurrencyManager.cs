using _Tools.Helpers;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class CurrencyManager : Singleton<CurrencyManager>
{
    [SerializeField] private TextMeshProUGUI coinText;

    private const string CoinsKey = "PlayerCoins";

    private int coins;

    private void Start()
    {
        GetCoins();
        UpdateCoinText();
    }

    private void SaveCoins()
    {
        PlayerPrefs.SetInt(CoinsKey, coins);
        PlayerPrefs.Save();
    }
    public int GetCoins()
    {
        coins = PlayerPrefs.GetInt(CoinsKey);
        return coins;
    }
    public void AddCoins(int amount)    // for convenience coins will always be saved , won't wait for game to end 
    {
        coins += amount;
        SaveCoins();
        Debug.Log("Coins added: " + amount + ". Total coins: " + coins);
        UpdateCoinText();
    }
    public void UpdateCoinText()
    {
       
        if (coinText != null)
        {
            coinText.text = coins.ToString();
        }

    }

}
