using System;

using _Tools.Helpers;
using UnityEngine;

namespace _Game.Managers
{
    public class GameManager : Singleton<GameManager>
    {
        #region Events

        public event Action OnLevelStart;
        public event Action OnLevelComplete;
        public event Action OnLevelFail;

        #endregion

        #region Variables

        [SerializeField, Range(0f, 5f)] private float _levelLoadDelay = 1f;

        #endregion

        #region Custom Methods

        public void LevelStart() => OnLevelStart?.Invoke();

        public void LevelComplete()
        {
            OnLevelComplete?.Invoke();
        }

        public void LevelFail()
        {
            OnLevelFail?.Invoke();

            if (UIManager.Instance != null ) UIManager.Instance.LevelReloadTransition(_levelLoadDelay, SceneUtils.ReloadScene);
        }

        #endregion
    }
}