using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    private int _dashForwardForce;
    private float _cooldownDashForward;
    private float _remainingTimeUntilDashForward;
    private bool _wheelIslive = false;

    public float CooldownDashForward { get => _cooldownDashForward; set => _cooldownDashForward = value < 0 ? 0 : value; }
    public int DashForwardForce { get => _dashForwardForce; set => _dashForwardForce = value < 0 ? 0 : value; }
    public bool WheelIsLive => _wheelIslive;

    [SerializeField] private EndGame _endGame;
    [SerializeField] private Rigidbody _rigedbodyWheel;
    [SerializeField] private int _startForce;
    [SerializeField] private GameObject _dashForwardButton;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private Image _dashForwardImage;

    private void Start()
    {
        _cooldownDashForward = 20;
        _remainingTimeUntilDashForward = _cooldownDashForward;
        StartCoroutine(CheckDeath());
        _startForce = 30;
    }

    private void Update()
    {
        if (_remainingTimeUntilDashForward <= _cooldownDashForward)
        {
            _dashForwardImage.fillAmount = _remainingTimeUntilDashForward / _cooldownDashForward;
            _remainingTimeUntilDashForward += Time.deltaTime;
        }
        else if (_dashForwardImage.fillAmount >= 0.9)
        {
            _dashForwardImage.fillAmount = 1;
        }
    }

    private IEnumerator CheckDeath()
    {
        while (true)
        {
            if (_rigedbodyWheel.velocity.x >= -0.2 && transform.position != _endGame.StartPosition)
            {
                yield return new WaitForSeconds(2);
                if (_rigedbodyWheel.velocity.x >= -0.2 && transform.position != _endGame.StartPosition)
                {
                    _wheelIslive = false;
                    // костыль
                    yield return new WaitForSeconds(0.1f);
                    // костыль закончился
                    _endGame.Death();
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    public void StartForce()
    {
        if (_upgrades.DashForwardLevel > 0) { _dashForwardButton.SetActive(true); }

        _wheelIslive = true;
        _rigedbodyWheel.isKinematic = false;
        _rigedbodyWheel.AddForce(new Vector3(-_startForce, 0, 0), ForceMode.Impulse);
    }

    public void DashLeft()
    {
        _rigedbodyWheel.AddForce(new Vector3(0, 0, -20), ForceMode.Impulse);
    }

    public void DashRight()
    {
        _rigedbodyWheel.AddForce(new Vector3(0, 0, 20), ForceMode.Impulse);
    }

    public void DashForward()
    {

        if (_dashForwardImage.fillAmount == 1)
        {
            _remainingTimeUntilDashForward = 0;
            _rigedbodyWheel.AddForce(new Vector3(-_dashForwardForce, 0, 0), ForceMode.Impulse);
        }
    }
}
