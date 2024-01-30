using UnityEngine;

[CreateAssetMenu(fileName = "PlayerData", menuName = "ScriptableObjects/PlayerData", order = 1)]
public class PlayerData : ScriptableObject
{
    public string playerName;
    public int meatRequirement;
    public int health;
    public int damageAmmount;
    public Sprite playerIcon;
    public GameObject playerPrefab;
}
