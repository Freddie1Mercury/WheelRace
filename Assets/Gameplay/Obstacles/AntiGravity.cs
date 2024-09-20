using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravity : Buff
{
    private void OnTriggerEnter(Collider other)
    {
        transform.GetComponent<Renderer>().enabled = false;
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        StartCoroutine(WaitEndBaff(other.gameObject.GetComponent<Rigidbody>()));



    }

    private void Start()
    {
        StartCoroutine(Spin());
    }

    private IEnumerator WaitEndBaff(Rigidbody rigidbody)
    {
        yield return new WaitForSeconds(_buffTIme);
        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
            transform.gameObject.SetActive(false);
        }
    }
}
