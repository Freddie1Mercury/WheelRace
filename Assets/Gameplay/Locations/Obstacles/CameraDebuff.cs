using Cinemachine;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class CameraDebuff : Debuff
{
    private CinemachineVirtualCamera _baseCamera;
    private void Start()
    {
        _buffAndDebuffBarsPool = GameObject.Find("BuffAndDebuffBarsPool").GetComponent<BuffAndDebuffBarsPool>();
        _baseCamera = GameObject.Find("BaseCamera").GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(Spin());
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

    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WheelController>() != null)
        {
            OffObstacle();
            _debuffTime = 10;
            _baseCamera.Priority = -_priority;
            Debug.Log("BaseCamera Priority OnnTriggerEnter " + _baseCamera.Priority);
            StartCoroutine(WaitEndDebuff());
            _debuffBarIndex = _buffAndDebuffBarsPool.GetPool(false);
            _remainingTimeUntilEndDebuff = _debuffTime;
        }
    }

    private IEnumerator WaitEndDebuff()
    {
        yield return new WaitForSeconds(_debuffTime);
        _baseCamera.Priority = _priority;
        Debug.Log("BaseCamera Priority WaitEndDebuff " + _baseCamera.Priority);
        OnnObstacle();
    }
}
