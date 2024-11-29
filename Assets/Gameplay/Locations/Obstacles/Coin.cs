using System.Collections;
using UnityEngine;

public class Coin : Buff
{
    private MoneyManager _moneyManager;
    private Upgrades _upgrades;
    private void Start()
    {
        StartCoroutine(Spin());
        _moneyManager = GameObject.Find("MoneyManager").GetComponent<MoneyManager>();
    }

    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Renderer>().enabled = false;
        float moneyAdditional = 20 * _moneyManager.MoneyMultipier;
        _moneyManager.AddMoney((int)moneyAdditional);
        StartCoroutine(ResetBuff());
    }

    private IEnumerator ResetBuff()
    {
        yield return new WaitForSeconds(3);
        transform.GetComponent<Renderer>().enabled = true;
    }
}
