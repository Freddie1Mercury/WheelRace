using System;
using TMPro;
using UnityEngine;

public  class DistanceCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text _distanceConterText;
    [SerializeField]
    private WheelController _wheelController;

    private float _startXPosition;
    private float _oldDistanceX = 0;
    private float _currentResult = 0;
    private float _oldResult = 0;

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
            _distanceConterText.text = Convert.ToInt64(_currentResult).ToString();
        _oldDistanceX = _currentResult;
    }

    private void ClearDistance()
    {
        _oldResult = _currentResult;
        _currentResult = 0;
    }
}