using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class PlayerBaseHealthUpgrade : MonoBehaviour
{
    public static event System.Action<float> OnHealthUpgrade;


    [SerializeField] private TextMeshProUGUI priceText;
    [SerializeField] private TextMeshProUGUI healthIncreaseAmountText;
    [SerializeField] private Button upgradeButton;

    private int currentPrice = 10; // Initial price
    private float currentHealthIncreaseAmount = 2; // Initial health increase amount

    private int upgradePriceIncrement = 15; // Price increment per upgrade
    private float healthIncreaseAmountIncrement = 2f; // Health increase amount increment per upgrade

    private const string PriceKey = "CurrentHealthUpgradePrice";
    private const string HealthIncreaseAmountKey = "CurrentHealthIncreaseAmount";

    void Start()
    {
        LoadData();
        UpdateUI();
    }

    void Update()
    {

    }

    private void LoadData()
    {
        if (PlayerPrefs.HasKey(PriceKey))
        {
            currentPrice = PlayerPrefs.GetInt(PriceKey);
        }

        if (PlayerPrefs.HasKey(HealthIncreaseAmountKey))
        {
            currentHealthIncreaseAmount = PlayerPrefs.GetFloat(HealthIncreaseAmountKey);
        }
    }

    private void UpdateUI()
    {
        priceText.text = currentPrice.ToString();
        healthIncreaseAmountText.text = currentHealthIncreaseAmount.ToString();
        upgradeButton.interactable = CurrencyManager.Instance.GetCoins() >= currentPrice;

        OnHealthUpgrade?.Invoke(currentHealthIncreaseAmount);
    }

    public void UpgradeHealth()
    {
        if (CurrencyManager.Instance.GetCoins() >= currentPrice)
        {
            CurrencyManager.Instance.AddCoins(-currentPrice);
            currentPrice += upgradePriceIncrement;
            currentHealthIncreaseAmount += healthIncreaseAmountIncrement;
            SaveData();
            UpdateUI();
  
        }
    }

    private void SaveData()
    {
        PlayerPrefs.SetInt(PriceKey, currentPrice);
        PlayerPrefs.SetFloat(HealthIncreaseAmountKey, currentHealthIncreaseAmount);
    }


}
