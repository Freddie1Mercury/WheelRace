using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class AirResistanceDebuff : Debuff
{
    private float dragAddition = 0.2f;

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WheelController>() != null)
        {
            OffObstacle();
            other.GetComponent<Rigidbody>().drag += dragAddition;
            StartCoroutine(WaitEndDebuf(other.GetComponent<Rigidbody>()));
            _debuffBarIndex = _buffAndDebuffBarsPool.GetPool(false);
            _remainingTimeUntilEndDebuff = _debuffTime;
        }
    }

    private void Update()
    {
        if (transform.GetComponent<Renderer>().enabled == false)
        {
            _remainingTimeUntilEndDebuff -= Time.deltaTime;
            _buffAndDebuffBarsPool.DebuffBars[_debuffBarIndex].GetComponent<Image>().fillAmount = _remainingTimeUntilEndDebuff / _debuffTime;
            if (_buffAndDebuffBarsPool.DebuffBars[_debuffBarIndex].GetComponent<Image>().fillAmount == 0)
            {
                _buffAndDebuffBarsPool.ReleasePool(false, _debuffBarIndex);
            }
        }
    }
    private void Start()
    {
        StartCoroutine(Spin());
        _buffAndDebuffBarsPool = GameObject.Find("BuffAndDebuffBarsPool").GetComponent<BuffAndDebuffBarsPool>();
    }

    private IEnumerator WaitEndDebuf(Rigidbody rigedbody)
    {
        yield return new WaitForSeconds(5);
        rigedbody.drag -= dragAddition;
        OnnObstacle();
    }
}
