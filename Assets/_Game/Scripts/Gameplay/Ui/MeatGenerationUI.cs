
using _Game.Managers;
using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class MeatGenerationUI : MonoBehaviour
{
    [Header("Ui")]
    [SerializeField] private Image meatFillImage;
    [SerializeField] private TextMeshProUGUI meatCountText;
    [SerializeField] private Button spawnButton;
    [SerializeField] private Image playerIcon;
    [SerializeField] private TextMeshProUGUI requiredmeatCountText;
    [SerializeField] private float fillRate = 3f;

    private float currentFillRate;
    private int meatCount = 0;

    PlayerManager playerManager;


    private void Start()
    {
        playerManager = PlayerManager.Instance;

        requiredmeatCountText.text = playerManager.playerDataList[playerManager.CurrentPlayerIndex].meatRequirement.ToString();

        UnlockPlayerData(playerManager.CurrentPlayerIndex);

        UpdateUI();

        currentFillRate = fillRate;

        // Load time reduction from PlayerPrefs and adjust fill rate
        float timeReduction = PlayerPrefs.GetFloat("CurrentTimeReduction", 0f);
        currentFillRate -= timeReduction;


    }

    private void Update()
    {
        meatFillImage.fillAmount += Time.deltaTime / currentFillRate;

        if (meatFillImage.fillAmount >= 1f)
        {
            meatCount++;
            meatFillImage.fillAmount = 0f;
            UpdateUI();
        }
    }

    public void OnSpawnButtonClick()
    {
        if (meatCount >= playerManager.playerDataList[playerManager.CurrentPlayerIndex].meatRequirement)
        {
            Debug.Log("Player Spawned!");
            meatCount -= playerManager.playerDataList[playerManager.CurrentPlayerIndex].meatRequirement;
            UpdateUI();
            PlayerData currentPlayerData = playerManager.playerDataList[playerManager.CurrentPlayerIndex];
            GameObject playerPrefab = currentPlayerData.playerPrefab;
            PlayerManager.Instance.SpawnPlayerPrefab(playerPrefab);
        }
    }

    void UpdateUI()
    {
        meatCountText.text = meatCount.ToString();
        spawnButton.interactable = (meatCount >= playerManager.playerDataList[playerManager.CurrentPlayerIndex].meatRequirement);
        playerIcon.sprite = playerManager.playerDataList[playerManager.CurrentPlayerIndex].playerIcon;
    }

    void UnlockPlayerData(int index)
    {
        if (index < playerManager.playerDataList.Count)
        {
            playerManager.CurrentPlayerIndex = index;
        }
    }

}