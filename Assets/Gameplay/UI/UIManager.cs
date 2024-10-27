using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Upgrades _upgrades;

    [SerializeField] private RectTransform _startSpawnBuffOrDebuffBarPosition;

    [SerializeField] private GameObject _dashForwardButton;
    [SerializeField] private GameObject _buffBarPrefab;
    [SerializeField] private GameObject _debuffBarPrefab;

    [SerializeField] private List<GameObject> _gameUi = new List<GameObject>();
    [SerializeField] private List<GameObject> _buffOrDebuffBars = new List<GameObject>();
    [SerializeField] private List<GameObject> _lobbyUI = new List<GameObject>();

    private void Start()
    {
        GameUIDisable();
    }
    public void GameUiEnable()
    {
        if (_upgrades.DashForwardLevel > 0)
        {
            _dashForwardButton.SetActive(true);
        }

        foreach (GameObject Uielement in _gameUi)
        {
            Uielement.SetActive(true);
        }

    }

    public void GameUIDisable()
    {
        _dashForwardButton?.SetActive(false);
        _dashForwardButton.GetComponent<Image>().fillAmount = 1;

        foreach (GameObject Uielement in _gameUi)
        {
            Uielement.SetActive(false);
        }

    }

    public int CreateBuffOrDebuffSlider(bool isBuff)
    {
        int barIndex;
        if (_buffOrDebuffBars.Count == 0)
        {
            if (isBuff)
            {
                _buffOrDebuffBars.Add(Instantiate(_buffBarPrefab));
                _buffOrDebuffBars[_buffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = _startSpawnBuffOrDebuffBarPosition.anchoredPosition;

            }
            else
            {
                _buffOrDebuffBars.Add(Instantiate(_debuffBarPrefab));
                _buffOrDebuffBars[_buffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = _startSpawnBuffOrDebuffBarPosition.anchoredPosition;
            }

        }
        else
        {
            if (isBuff)
            {
                _buffOrDebuffBars.Add(Instantiate(_buffBarPrefab));
                Vector2 newPosition = _buffOrDebuffBars[_buffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(200, 0);
                _buffOrDebuffBars[_buffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = newPosition;

            }
            else
            {
                _buffOrDebuffBars.Add(Instantiate(_debuffBarPrefab));
                Vector2 newPosition = _buffOrDebuffBars[_buffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(200, 0);
                _buffOrDebuffBars[_buffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = newPosition;
            }
        }
        barIndex = _buffOrDebuffBars.Count - 1;
        return barIndex;
    }

    public void LobbyUiSetActive(bool value)
    {
        foreach (var uiElement in _lobbyUI)
        {
            uiElement.SetActive(value);
        }
    }
}
