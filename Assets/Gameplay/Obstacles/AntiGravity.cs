using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravity : Buff
{
    private void OnTriggerEnter(Collider other)
    {
        int delayInSecond = 5;
        if (other.gameObject.GetComponent<Rigidbody>() != null)
        {
            other.gameObject.GetComponent<Rigidbody>().useGravity = false;
        }
        StartCoroutine(WaitEndBaff(delayInSecond, other.gameObject.GetComponent<Rigidbody>()));



    }

    private IEnumerator WaitEndBaff(int delay, Rigidbody rigidbody)
    {
        yield return new WaitForSeconds(delay);
        if (rigidbody != null)
        {
            rigidbody.useGravity = true;
            transform.gameObject.SetActive(false);
        }
    }
}
