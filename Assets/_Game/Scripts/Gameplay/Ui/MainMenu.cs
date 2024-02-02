using _Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MainMenu : MonoBehaviour
{

    private void Start()
    {
        GameManager.Instance.OnLevelStart += Hide;
    }
    private void OnDestroy()
    {
        GameManager.Instance.OnLevelStart -= Hide;
    }
    void Hide() => gameObject.SetActive(false); 

    void Show()=> gameObject.SetActive(true);   
}
