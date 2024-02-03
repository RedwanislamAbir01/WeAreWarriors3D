using System.Collections;
using DG.Tweening;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.UI;

namespace _Game.UI
{
    public class MissionPanelBottomButtons : MonoBehaviour
    {
        [SerializeField] private RectTransform _shopPanel;
        [SerializeField] private RectTransform _missionPanel;
        [SerializeField] private RectTransform _otherPanel;

        [Space(20)][SerializeField] private Button _missionButton;
        [SerializeField] private Button _shopButton;
        [SerializeField] private Button _otherButton;

       // [Space(20)][SerializeField] private Color _selectedColor;

        [Space(20)][SerializeField] private float _transitionDuration = 1.0f;

        private Button _previousSelectedButton;
        private RectTransform _previousSelectedPanel;

        private int _buttonClickOrder;
        private int _previousButtonClickOrder = 1;

        private bool _isTransitionCompleted = true;

        private void Start()
        {
            _missionButton.onClick.AddListener(MissionButtonCallBack);
            _shopButton.onClick.AddListener(SkinButtonCallBack);
            _otherButton.onClick.AddListener(WeaponButtonCallBack);

            _previousButtonClickOrder = 1;
            _previousSelectedButton = _shopButton;
            _previousSelectedPanel = _missionPanel;
            SelectButton(_missionButton);
        }

        private void MissionButtonCallBack()
        {
            _buttonClickOrder = 1;
            StartCoroutine(TransitionPanel(_missionPanel, _missionButton));
            SoundManager.Instance.PlaySound(SoundManager.Instance._audioClipRefsSO.btnPress);
        }

        private void SkinButtonCallBack()
        {
            _buttonClickOrder = 0;
            StartCoroutine(TransitionPanel(_shopPanel, _shopButton));
            SoundManager.Instance.PlaySound(SoundManager.Instance._audioClipRefsSO.btnPress);
        }

        private void WeaponButtonCallBack()
        {
            _buttonClickOrder = 2;
            StartCoroutine(TransitionPanel(_otherPanel, _otherButton));
            SoundManager.Instance.PlaySound(SoundManager.Instance._audioClipRefsSO.btnPress);

        }


        IEnumerator TransitionPanel(RectTransform currentPanel, Button currentButton)
        {
            if (currentPanel == _previousSelectedPanel || !_isTransitionCompleted) yield break;

            _isTransitionCompleted = false;

            int direction = _buttonClickOrder > _previousButtonClickOrder ? 1 : -1;

            SelectButton(currentButton);

            if (DOTween.IsTweening(currentPanel) || DOTween.IsTweening(_previousSelectedPanel))
            {
                yield return null;
            }

            Vector2 rectSize = _previousSelectedPanel.rect.size;
            currentPanel.anchoredPosition = new Vector2(rectSize.x * direction, 0);
            currentPanel.gameObject.SetActive(true);

            var currentTween = currentPanel.DOAnchorPos(new Vector2(0, 0), _transitionDuration);
            var previousTween = _previousSelectedPanel.DOAnchorPos(new Vector2(rectSize.x * -direction, 0), _transitionDuration);

            yield return new WaitForSeconds(_transitionDuration); // Wait for the transition duration

            if (currentTween != null) currentTween.Kill(); // Kill the tween if it's not null
            if (previousTween != null) previousTween.Kill(); // Kill the tween if it's not null

            _previousSelectedPanel.anchoredPosition = Vector2.zero;
            _previousSelectedPanel.gameObject.SetActive(false);
            _previousSelectedPanel = currentPanel;

            if (currentButton == _previousSelectedButton)
            {
                _isTransitionCompleted = true;
                yield break;
            }

            _isTransitionCompleted = true;
        }


        private void SelectButton(Button button)
        {
            if (button == _previousSelectedButton) return;
            _previousButtonClickOrder = _buttonClickOrder;

            if (_previousSelectedButton)
            {
               
                RectTransform previousButtonRectTransform = _previousSelectedButton.GetComponent<RectTransform>();
                DOTween.To(() => previousButtonRectTransform.anchoredPosition,
                            x => previousButtonRectTransform.anchoredPosition = x,
                            new Vector2(previousButtonRectTransform.anchoredPosition.x, 0),
                            _transitionDuration);
            }

           
            _previousSelectedButton = button;

            RectTransform currentButtonRectTransform = button.GetComponent<RectTransform>();
            DOTween.To(() => currentButtonRectTransform.anchoredPosition,
                        x => currentButtonRectTransform.anchoredPosition = x,
                        new Vector2(currentButtonRectTransform.anchoredPosition.x, 50),
                        _transitionDuration);
        }


    }
}