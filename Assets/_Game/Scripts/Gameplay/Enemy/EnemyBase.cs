using _Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyBase : MonoBehaviour
{
    [System.Serializable]
    public class EnemyGroup
    {
        public List<GameObject> enemyPrefabs; // List of possible enemy prefabs for each group
        public int numberOfEnemiesInGroup;
        public float characterSpawnDelay; // Delay between characters within the same group
        public float groupSpawnDelay; // Delay between enemy groups in the wave
    }
    [System.Serializable]
    public class EnemyWave
    {
        public float waveStartDelay;
        public List<EnemyGroup> enemyGroups; // List of enemy groups for each wave

    }

    [Header("Spawn Points")]
    [SerializeField] private Transform _startPoint;

    [Header("Enemy Waves")]
    [SerializeField] private List<EnemyWave> _enemyWaves;


    bool canSpawn = false;


    private void Start()
    {
        GameManager.Instance.OnLevelStart += CanSpawn;
        StartCoroutine(SpawnEnemyWaves());

    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStart -= CanSpawn;
    }

    private IEnumerator SpawnEnemyWaves()
    {
        while (!canSpawn)
        {
            yield return null;
        }

        foreach (EnemyWave wave in _enemyWaves)
        {
            yield return new WaitForSeconds(wave.waveStartDelay);

            foreach (EnemyGroup group in wave.enemyGroups)
            {
                for (int i = 0; i < group.numberOfEnemiesInGroup; i++)
                {
                    SpawnEnemyGroup(group);
                    yield return new WaitForSeconds(group.characterSpawnDelay);
                }

                yield return new WaitForSeconds(group.groupSpawnDelay);
            }
        }
    }

    private void SpawnEnemyGroup(EnemyGroup group)
    {
        foreach (GameObject enemyPrefab in group.enemyPrefabs)
        {
            Instantiate(enemyPrefab, _startPoint.position, Quaternion.identity);
        }
    }

    void CanSpawn() => canSpawn = true;
}

