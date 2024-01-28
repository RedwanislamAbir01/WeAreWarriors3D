using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    private GameObject currentPlayerPrefab;

    private void OnEnable()
    {
        PlayerManager.OnPlayerSpawned += HandlePlayerSpawned;
    }

    private void OnDisable()
    {
        PlayerManager.OnPlayerSpawned -= HandlePlayerSpawned;
    }

    private void HandlePlayerSpawned(GameObject playerPrefab)
    {
        // Instantiate the player prefab
        if (currentPlayerPrefab != null)
        {
            Destroy(currentPlayerPrefab);
        }

        currentPlayerPrefab = Instantiate(playerPrefab, transform.position, Quaternion.identity);
    }
}
