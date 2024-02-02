using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class FoodProdutionUpgrade : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI timeReduceAmountText;
    [SerializeField] private Button upgradeButton;

    public delegate void FillRateUpgrade(float newTimeReduction);
    public static event FillRateUpgrade OnFillRateUpgrade;

    private int currentPrice = 10; // Initial price
    private float currentTimeReduction = 0.2f; // Initial time reduction

    private int upgradePriceIncrement = 10; // Price increment per upgrade
    private float timeReductionIncrement = 0.2f; // Time reduction increment per upgrade

    private const string PriceKey = "CurrentPrice";
    private const string TimeReductionKey = "CurrentTimeReduction";

    private void Start()
    {
        LoadData();
        UpdateUI();
    }
    private void LoadData()
    {
        if (PlayerPrefs.HasKey(PriceKey))
        {
            currentPrice = PlayerPrefs.GetInt(PriceKey);
        }

        if (PlayerPrefs.HasKey(TimeReductionKey))
        {
            currentTimeReduction = PlayerPrefs.GetFloat(TimeReductionKey);
        }
    }
    private void UpdateUI()
    {
        priceText.text = currentPrice.ToString();
        timeReduceAmountText.text = currentTimeReduction.ToString();
        upgradeButton.interactable = CurrencyManager.Instance.GetCoins() >= currentPrice;
    }

    public void UpgradeFillRate()
    {
        if (CurrencyManager.Instance.GetCoins() >= currentPrice)
        {
            CurrencyManager.Instance.AddCoins(-currentPrice);
            currentPrice += upgradePriceIncrement;
            currentTimeReduction += timeReductionIncrement;
            SaveCurrentTimeReduction(); // Save the new time reduction
            SaveData();
            UpdateUI();
            if (OnFillRateUpgrade != null)
                OnFillRateUpgrade(currentTimeReduction); // Raise event with the new time reduction
        }
    }
    private void SaveData()
    {
        PlayerPrefs.SetInt(PriceKey, currentPrice);
        PlayerPrefs.SetFloat(TimeReductionKey, currentTimeReduction);
    }

    private void SaveCurrentTimeReduction()
    {
        PlayerPrefs.SetFloat("CurrentTimeReduction", currentTimeReduction);
    }
}
