using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
   public void StartForce()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3 (-30, 0, 0), ForceMode.Impulse);
    }
}
