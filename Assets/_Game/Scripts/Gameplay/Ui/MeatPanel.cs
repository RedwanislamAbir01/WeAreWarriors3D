using _Game.Managers;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MeatPanel : MonoBehaviour
{
    [SerializeField] private GameObject _meatGenerationPanel;
  
        private void Awake()
        {
            HidePanel();
        }
        private void OnDestroy()
        {
            GameManager.Instance.OnLevelStart -= ShowPanel;
        }

        private void Start()
        {
            GameManager.Instance.OnLevelStart += ShowPanel;
        }

    void HidePanel() => _meatGenerationPanel.gameObject.SetActive(false);

    void ShowPanel() => _meatGenerationPanel.gameObject.SetActive(true);

}

