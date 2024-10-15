using System.Collections;
using UnityEngine;

public class ForceObstacle : Buff
{
    [SerializeField] private bool applyForceOnX;
    [SerializeField] private bool applyForceOnY;
    [SerializeField] private int _forceOnX = 30;
    [SerializeField] private int _forceOnY = 30;

    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Renderer>().enabled = false;
        if (applyForceOnX)
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-_forceOnX, 0, 0), ForceMode.Impulse);
        if (applyForceOnY)
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0, _forceOnY, 0), ForceMode.Impulse);
        StartCoroutine(ResetBuff());
    }

    private void Start()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator ResetBuff()
    {
        yield return new WaitForSeconds(3);
        transform.GetComponent<Renderer>().enabled = true;
    }

}
