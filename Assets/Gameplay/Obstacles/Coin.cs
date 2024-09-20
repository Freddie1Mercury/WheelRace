using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Buff
{
    [SerializeField] private MoneyManager _moneyManager;
    private void Start()
    {
    }

    private void Update()
    {
        transform.Rotate(0, 180 * Time.deltaTime, 0);
    }

    private void OnTriggerEnter(Collider other)
    {
        int moneyAdditional = 20;
        _moneyManager.AddMoney(moneyAdditional);
        transform.gameObject.SetActive(false);
    }
}
