using System;
using TMPro;
using UnityEngine;

public class DistanceCounter : MonoBehaviour
{
    [SerializeField]
    private TMP_Text textMeshPro;

    private float _startXPosition;
    private float _oldDistanceX = 0;

    void Start()
    {
        _startXPosition = transform.position.x;
    }

    void Update()
    {
        float currentDistanceX = Mathf.Abs(transform.position.x - _startXPosition);
        if (currentDistanceX > 0 && currentDistanceX > _oldDistanceX)
            textMeshPro.text = Convert.ToInt64(currentDistanceX).ToString();
        _oldDistanceX = currentDistanceX;
    }
}