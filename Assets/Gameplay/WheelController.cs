using UnityEngine;
using UnityEngine.UI;

public class WheelController : MonoBehaviour
{
    private int _dashForwardForce;
    private float _cooldownDashForward;
    private float _remainingTimeUntilDashForward;
    public bool WheelIslive = false;

    public float CooldownDashForward { get => _cooldownDashForward; set => _cooldownDashForward = value < 0 ? 0 : value; }
    public int DashForwardForce { get => _dashForwardForce; set => _dashForwardForce = value < 0 ? 0 : value; }
    public int StartForce { get => _startForce; set => _startForce = value < 0 ? 0 : value; }
    public Rigidbody RigidbodyWheel;

    [SerializeField] private int _startForce;
    [SerializeField] private GameObject _dashForwardButton;
    [SerializeField] private Image _dashForwardImage;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private SoundClips _soundClips;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private EndGame _endGame;
    [SerializeField] private GenerateTerrainPool _terrainPool;
    [SerializeField] private UIManager _uiManager;

    private void Start()
    {
        _cooldownDashForward = 15;
        _remainingTimeUntilDashForward = _cooldownDashForward;
        StartCoroutine(_endGame.CheckDeath());
        _startForce = 30;
    }

    private void Update()
    {
        if (_dashForwardImage.fillAmount == 1)
        {
            _remainingTimeUntilDashForward = _cooldownDashForward;
        }
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

    public void AddStartForce()
    {
        _uiManager.GameUiEnable();
        transform.Rotate(0, 0, 1000);
        _terrainPool.InitTerrains();
        WheelIslive = true;
        RigidbodyWheel.isKinematic = false;
        RigidbodyWheel.AddForce(new Vector3(-_startForce, 0, 0), ForceMode.Impulse);

    }

    public void DashLeft()
    {
        RigidbodyWheel.AddForce(new Vector3(0, 0, -20), ForceMode.Impulse);
        _audioManager.PlaySound(audioClip: _soundClips.WhooshingSound);
    }

    public void DashRight()
    {
        RigidbodyWheel.AddForce(new Vector3(0, 0, 20), ForceMode.Impulse);
        _audioManager.PlaySound(audioClip: _soundClips.WhooshingSound);
    }

    public void DashForward()
    {
        if (_dashForwardImage.fillAmount == 1)
        {
            _remainingTimeUntilDashForward = 0;
            _dashForwardImage.fillAmount = _remainingTimeUntilDashForward / _cooldownDashForward;
            RigidbodyWheel.AddForce(new Vector3(-_dashForwardForce, 0, 0), ForceMode.Impulse);
        }
    }
}
