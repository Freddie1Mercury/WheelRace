using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    [SerializeField] private Upgrades _upgrades;

    [SerializeField] private GameObject _dashForwardButton;
    [SerializeField] private RectTransform _startSpawnBuffOrDebuffBarPosition;
    [SerializeField] private GameObject _buffBarPrefab;
    [SerializeField] private GameObject _debuffBarPrefab;

    [SerializeField] private List<GameObject> _gameUi = new List<GameObject>();

    [SerializeField] private List<GameObject> BuffOrDebuffBars = new List<GameObject>();

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
        if (BuffOrDebuffBars.Count == 0)
        {
            if (isBuff)
            {
                BuffOrDebuffBars.Add(Instantiate(_buffBarPrefab));
                BuffOrDebuffBars[BuffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = _startSpawnBuffOrDebuffBarPosition.anchoredPosition;
                
            }    
            else
            {
                BuffOrDebuffBars.Add(Instantiate(_debuffBarPrefab));
                BuffOrDebuffBars[BuffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = _startSpawnBuffOrDebuffBarPosition.anchoredPosition;
            }

        }
        else
        {
            if (isBuff)
            {
                BuffOrDebuffBars.Add(Instantiate(_buffBarPrefab));
                Vector2 newPosition = BuffOrDebuffBars[BuffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(200, 0);
                BuffOrDebuffBars[BuffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = newPosition;

            }
            else
            {
                BuffOrDebuffBars.Add(Instantiate(_debuffBarPrefab));
                Vector2 newPosition = BuffOrDebuffBars[BuffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition + new Vector2(200, 0);
                BuffOrDebuffBars[BuffOrDebuffBars.Count - 1].GetComponent<RectTransform>().anchoredPosition = newPosition;
            }
        }
        barIndex = BuffOrDebuffBars.Count - 1;
        return barIndex;
    }
}
