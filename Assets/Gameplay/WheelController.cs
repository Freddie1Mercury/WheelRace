using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private bool _leftRotateButtonIsPressed = false;
    private bool _rightRotateButtonIsPressed = false;
    public void StartForce()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(-30, 0, 0), ForceMode.Impulse);
    }

    public void DashLeft()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -20), ForceMode.Impulse);
    }

    public void DashRight()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 20), ForceMode.Impulse);
    }
}
