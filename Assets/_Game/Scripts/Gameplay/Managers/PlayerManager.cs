using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using _Tools.Helpers;
using System;

public class PlayerManager : Singleton<PlayerManager>   
{
    public static event Action<GameObject> OnPlayerSpawned;


    public List<PlayerData> playerDataList;
    private int currentPlayerIndex = 0;

    public int CurrentPlayerIndex { get => currentPlayerIndex; set => currentPlayerIndex = value; }

    public void SpawnPlayerPrefab(GameObject playerPrefab) =>  OnPlayerSpawned?.Invoke(playerPrefab);
    
}
