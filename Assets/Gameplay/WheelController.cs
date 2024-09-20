using System.Collections;
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
    public int StartForce { get => _startForce; set => _startForce = value < 0 ? 0 : value; }
    public bool WheelIsLive => _wheelIslive;

    [SerializeField] private EndGame _endGame;
    [SerializeField] private Rigidbody _rigidbodyWheel;
    [SerializeField] private int _startForce;
    [SerializeField] private GameObject _dashForwardButton;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private Image _dashForwardImage;
    [SerializeField] private GenerateTerrain _generateTerrain;

    private void Start()
    {
        _cooldownDashForward = 20;
        _remainingTimeUntilDashForward = _cooldownDashForward;
        StartCoroutine(CheckDeath());
        _startForce = 30;
        Debug.Log(StartForce);
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
            if (_rigidbodyWheel.velocity.x >= -0.2 && transform.position != _endGame.StartPosition)
            {
                yield return new WaitForSeconds(2);
                if (_rigidbodyWheel.velocity.x >= -0.2 && transform.position != _endGame.StartPosition)
                {
                    _wheelIslive = false;
                    yield return new WaitForSeconds(0.1f);
                    _endGame.Death();
                }
            }
            else
            {
                yield return null;
            }
        }
    }

    public void AddStartForce()
    {
        if (_upgrades.DashForwardLevel > 0) { _dashForwardButton.SetActive(true); }

        _wheelIslive = true;
        _rigidbodyWheel.isKinematic = false;
        _rigidbodyWheel.AddForce(new Vector3(-_startForce, 0, 0), ForceMode.Impulse);

        StopCoroutine(_generateTerrain.Generate());
        StartCoroutine(_generateTerrain.Generate());
    }

    public void DashLeft()
    {
        _rigidbodyWheel.AddForce(new Vector3(0, 0, -20), ForceMode.Impulse);
    }

    public void DashRight()
    {
        _rigidbodyWheel.AddForce(new Vector3(0, 0, 20), ForceMode.Impulse);
    }

    public void DashForward()
    {
        if (_dashForwardImage.fillAmount == 1)
        {
            _remainingTimeUntilDashForward = 0;
            _rigidbodyWheel.AddForce(new Vector3(-_dashForwardForce, 0, 0), ForceMode.Impulse);
        }
    }
}
