using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AirResistanceDebuff : Debuff
{
    private float dragAddition = 0.2f;
    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Renderer>().enabled = false;
        other.GetComponent<Rigidbody>().drag += dragAddition;
        StartCoroutine(WaitEndDebuf(other.GetComponent<Rigidbody>()));
    }

    private void Start()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator WaitEndDebuf(Rigidbody rigedbody)
    {
        yield return new WaitForSeconds(5);
        rigedbody.drag -= dragAddition;
        transform.gameObject.SetActive(false);
    }
}
