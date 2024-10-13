using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AntiGravity : Buff
{
    private void OnTriggerEnter(Collider other)
    {
        OffObstacle();
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        StartCoroutine(WaitEndBaff(other.GetComponent<Rigidbody>()));
        _buffBarIndex = _buffAndDebuffBarsPool.GetPool(true);
        _remainingTimeUntilEndBuff = _buffTime;


    }

    private void Start()
    {
        StartCoroutine(Spin());
        _buffAndDebuffBarsPool = GameObject.Find("BuffAndDebuffBarsPool").GetComponent<BuffAndDebuffBarsPool>();

    }

    private void Update()
    {
        if (transform.GetComponent<Renderer>().enabled == false)
        {
            _remainingTimeUntilEndBuff -= Time.deltaTime;
            _buffAndDebuffBarsPool.BuffBars[_buffBarIndex].GetComponent<Image>().fillAmount = _remainingTimeUntilEndBuff / _buffTime;
            if (_buffAndDebuffBarsPool.BuffBars[_buffBarIndex].GetComponent<Image>().fillAmount == 0)
            {
                _buffAndDebuffBarsPool.ReleasePool(true, _buffBarIndex);
            }
        }
    }

    private IEnumerator WaitEndBaff(Rigidbody rigidbody)
    {
        yield return new WaitForSeconds(_buffTime);
        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
            OnnObstacle();
        }
    }
}
