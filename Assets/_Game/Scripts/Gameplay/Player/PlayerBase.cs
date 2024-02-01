using UnityEngine;

public class PlayerBase : MonoBehaviour
{
    public float spawnRadius = 5f; // Adjust this radius as needed
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

        Vector2 randomCircle = Random.insideUnitCircle.normalized * spawnRadius;
        Vector3 spawnPosition = new Vector3(randomCircle.x, transform.position.y, randomCircle.y) + transform.position;
        Instantiate(playerPrefab, spawnPosition, Quaternion.identity);
    }


    // Draw gizmos for visualization
    private void OnDrawGizmosSelected()
    {
        Gizmos.color = Color.yellow;
        Gizmos.DrawWireSphere(transform.position, spawnRadius);
    }
}
