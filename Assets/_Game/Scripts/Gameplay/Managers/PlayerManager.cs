using UnityEngine;
using UnityEngine.UI;
using System.Collections.Generic;
using TMPro;
using _Tools.Helpers;

public class PlayerManager : Singleton<PlayerManager>   
{
    public List<PlayerData> playerDataList;
    private int currentPlayerIndex = 0;

    public int CurrentPlayerIndex { get => currentPlayerIndex; set => currentPlayerIndex = value; }
}
