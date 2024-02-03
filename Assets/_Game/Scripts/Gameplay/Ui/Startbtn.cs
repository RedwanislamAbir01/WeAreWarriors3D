using _Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Startbtn : MonoBehaviour
{
  
    public void StartGame()
    {
        GameManager.Instance.LevelStart();
        SoundManager.Instance.PlaySound(SoundManager.Instance._audioClipRefsSO.battleStart);
    }
}
