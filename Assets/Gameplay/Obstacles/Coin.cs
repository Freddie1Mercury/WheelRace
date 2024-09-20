using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Coin : Buff
{
     private MoneyManager _moneyManager;
    private void Start()
    {
        StartCoroutine(Spin());
    }

    private void OnTriggerEnter(Collider other)
    {
        _moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
        int moneyAdditional = 20;
        _moneyManager.AddMoney(moneyAdditional);
        transform.gameObject.SetActive(false);
    }
}
