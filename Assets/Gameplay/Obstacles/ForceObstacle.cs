using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ForceObstacle : Buff
{
    [SerializeField] private bool applyForceOnX;
    [SerializeField] private bool applyForceOnY;
    [SerializeField] private int _forceOnX;
    [SerializeField] private int _forceOnY;

    private void OnTriggerEnter(Collider other)
    {
        if (applyForceOnX)
        other.GetComponent<Rigidbody>().AddForce(new Vector3(-_forceOnX,0,0), ForceMode.Impulse);
        if (applyForceOnY)
        other.GetComponent<Rigidbody>().AddForce(new Vector3(0,_forceOnY,0), ForceMode.Impulse);
    }
}
