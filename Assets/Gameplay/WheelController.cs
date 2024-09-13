using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private bool _wheelIslive = false;
    public bool WheelIsLive  => _wheelIslive;
    [SerializeField] private EndGame _endGame;
    private void Update()
    {
       if (transform.GetComponent<Rigidbody>().velocity.x >= 1 && transform.position != _endGame.StartPosition)
       {
            _wheelIslive = false;
       }
    }
    public void StartForce()
    {
        _wheelIslive = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
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
