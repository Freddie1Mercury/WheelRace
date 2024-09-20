using Cinemachine;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraDebuff : Debuff
{
    [SerializeField] private CinemachineVirtualCamera _baseCamera;

    private void Start()
    {
        Spin();
    }
    private void OnTriggerEnter(Collider other)
    {
       
        _debuffTime = 10;
        _baseCamera.Priority -= 1;
        StartCoroutine(WaitEndDebuff());
    }

    private IEnumerator WaitEndDebuff()
    {
        yield return new WaitForSeconds(_debuffTime);
        _baseCamera.Priority += 1;
    }
}
