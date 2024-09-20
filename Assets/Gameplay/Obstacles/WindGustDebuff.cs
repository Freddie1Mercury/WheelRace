using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WindGustDebuff : Debuff
{
    private Rigidbody _wheelRigidbody;
    private int _force = 600;
    private bool _isForceRight;
    private System.Random _random = new System.Random();
    private void OnTriggerEnter(Collider other)
    {
        if (other.GetComponent<Rigidbody>() != null)
        {
            _debuffTime = 7;
            _debuffIsActive = true;
            _wheelRigidbody = other.GetComponent<Rigidbody>();
        }
        
        _isForceRight = _random.Next(0, 2) == 1;
        StartCoroutine(WaitEndDebuff());
    }

    private IEnumerator WaitEndDebuff() 
    {
        yield return new WaitForSeconds(_debuffTime);
        _debuffIsActive = false;
    }

    private void FixedUpdate()
    {
        if (_isForceRight)
        {
        if (_debuffIsActive) { _wheelRigidbody.AddForce(new Vector3(0, 0, _force * Time.deltaTime)); }
        }
        else
        {
            if (_debuffIsActive) { _wheelRigidbody.AddForce(new Vector3(0, 0, -_force * Time.deltaTime)); }
        }
    }
}
