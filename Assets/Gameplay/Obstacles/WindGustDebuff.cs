using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class WindGustDebuff : Debuff
{
    private Rigidbody _wheelRigidbody;

    private int _force = 600;

    private bool _isForceRight;

    private System.Random _random = new System.Random();



    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject.GetComponent<WheelController>() != null)
        {
            OffObstacle();
            if (other.GetComponent<Rigidbody>() != null)
            {
                _debuffTime = 7;
                _remainingTimeUntilEndDebuff = _debuffTime;
                _debuffIsActive = true;
                _wheelRigidbody = other.GetComponent<Rigidbody>();
            }

            _isForceRight = _random.Next(0, 2) == 1;
            StartCoroutine(WaitEndDebuff());
            _debuffBarIndex = _buffAndDebuffBarsPool.GetPool(false);
            _remainingTimeUntilEndDebuff = _debuffTime;
        }
    }
    private void Start()
    {
        StartCoroutine(Spin());
        _buffAndDebuffBarsPool = GameObject.Find("BuffAndDebuffBarsPool").GetComponent<BuffAndDebuffBarsPool>();

    }

    private void Update()
    {
        if (transform.GetComponent<Renderer>().enabled == false)
        {
            _remainingTimeUntilEndDebuff -= Time.deltaTime;
            _buffAndDebuffBarsPool.DebuffBars[_debuffBarIndex].GetComponent<Image>().fillAmount = _remainingTimeUntilEndDebuff / _debuffTime;
            if (_buffAndDebuffBarsPool.DebuffBars[_debuffBarIndex].GetComponent<Image>().fillAmount == 0)
            {
                _buffAndDebuffBarsPool.ReleasePool(false, _debuffBarIndex);
            }
        }
    }

    private IEnumerator WaitEndDebuff()
    {
        yield return new WaitForSeconds(_debuffTime);
        _debuffIsActive = false;
        OnnObstacle();
    }

    private void FixedUpdate()
    {
        if (_debuffIsActive)
        {
            if (_isForceRight)
            {
                { _wheelRigidbody.AddForce(new Vector3(0, 0, _force * Time.deltaTime)); }
            }
            else
            {
                if (_debuffIsActive) { _wheelRigidbody.AddForce(new Vector3(0, 0, -_force * Time.deltaTime)); }
            }
        }
    }
}
