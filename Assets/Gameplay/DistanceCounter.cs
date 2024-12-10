using System;
using TMPro;
using UnityEngine;
using YG;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _distanceCounterText;
    [SerializeField]
    private WheelController _wheelController;

    private float _startXPosition;
    private float _oldDistanceX = 0;
    private float _currentResult = 0;
    private float _oldResult = 0;
    private string _leaderboardName = "MaxResult";


    public float MaxResult { get =>YandexGame.savesData.MaxResult; set => YandexGame.savesData.MaxResult = value; }
    public float OldResult => _oldResult;

    void Start()
    {
        _startXPosition = transform.position.x;
    }

    void Update()
    {
        if (_wheelController.WheelIslive == false)
            ClearDistance();

        _currentResult = Mathf.Abs(transform.position.x - _startXPosition);
        if (_currentResult > 0 && _currentResult > _oldDistanceX)
            _distanceCounterText.text = Convert.ToInt64(_currentResult).ToString();
        _oldDistanceX = _currentResult;
    }

    private void ClearDistance()
    {
        _oldResult = _currentResult;
        if (MaxResult < _oldResult)
        {
            MaxResult = _oldResult;
            YandexGame.NewLeaderboardScores(_leaderboardName, (long)MaxResult);
        }
        _currentResult = 0;
    }
}