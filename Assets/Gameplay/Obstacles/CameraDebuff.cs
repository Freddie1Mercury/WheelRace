using Cinemachine;
using System.Collections;
using UnityEngine;

public class CameraDebuff : Debuff
{
     private CinemachineVirtualCamera _baseCamera;

    private void Start()
    {
        _baseCamera = GameObject.Find("BaseCamera").GetComponent<CinemachineVirtualCamera>();
        StartCoroutine(Spin());
    }
    private void OnTriggerEnter(Collider other)
    {
       transform.GetComponent<Renderer>().enabled = false;
        _debuffTime = 10;
        _baseCamera.Priority -= 1;
        StartCoroutine(WaitEndDebuff());
    }

    private IEnumerator WaitEndDebuff()
    {
        yield return new WaitForSeconds(_debuffTime);
        _baseCamera.Priority += 1;
        transform.gameObject.SetActive(false);
    }
}
