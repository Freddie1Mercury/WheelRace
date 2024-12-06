using UnityEngine;
using UnityEngine.UI;
using YG;

public class WheelController : MonoBehaviour
{
    public float CooldownDashForward { get => YandexGame.savesData.CooldownDashForward; set => YandexGame.savesData.CooldownDashForward = value < 0 ? 0 : value; }
    public int DashForwardForce { get => YandexGame.savesData.DashForwardForce; set => YandexGame.savesData.DashForwardForce = value < 0 ? 0 : value; }
    public int StartForce { get => YandexGame.savesData.StartForce; set => YandexGame.savesData.StartForce = value < 0 ? 0 : value; }

    public bool WheelIslive = true;

    public Rigidbody RigidbodyWheel;

    public Image DashForwardImage;

    [SerializeField] private GameObject _dashForwardButton;

    [SerializeField] private AudioManager _audioManager;
    [SerializeField] private SoundClips _soundClips;
    [SerializeField] private Upgrades _upgrades;
    [SerializeField] private EndGame _endGame;
    [SerializeField] private GenerateTerrainPool _terrainPool;
    [SerializeField] private UIManager _uiManager;

    private float _remainingTimeUntilDashForward;

    private void Start()
    {
        _remainingTimeUntilDashForward = CooldownDashForward;
        StartCoroutine(_endGame.CheckDeath());
        YandexGame.SaveProgress();
    }

    private void Update()
    {
        if (DashForwardImage.fillAmount == 1)
        {
            _remainingTimeUntilDashForward = CooldownDashForward;
        }
        if (_remainingTimeUntilDashForward <= CooldownDashForward)
        {
            DashForwardImage.fillAmount = _remainingTimeUntilDashForward / CooldownDashForward;
            _remainingTimeUntilDashForward += Time.deltaTime;
        }
        else if (DashForwardImage.fillAmount >= 0.9)
        {
            DashForwardImage.fillAmount = 1;
        }

        if (Input.GetKeyDown(KeyCode.A))
        {
            DashLeft();
            return;
        }
        if (Input.GetKeyDown(KeyCode.D))
        {
            DashRight();
            return;
        }
        if (Input.GetKeyDown(KeyCode.Space))
        {
            DashForward();
            return;
        }
    }

    public void AddStartForce()
    {
        _uiManager.GameUiEnable();
        transform.Rotate(0, 0, 1000);
        _terrainPool.InitTerrains();
        WheelIslive = true;
        RigidbodyWheel.isKinematic = false;
        RigidbodyWheel.AddForce(new Vector3(-StartForce, 0, 0), ForceMode.Impulse);

    }

    public void DashLeft()
    {
        RigidbodyWheel.AddForce(new Vector3(0, 0, -15), ForceMode.Impulse);
        _audioManager.PlaySound(audioClip: _soundClips.WhooshingSound);
    }

    public void DashRight()
    {
        RigidbodyWheel.AddForce(new Vector3(0, 0, 15), ForceMode.Impulse);
        _audioManager.PlaySound(audioClip: _soundClips.WhooshingSound);
    }

    public void DashForward()
    {
        if (_upgrades.DashForwardLevel > 0)
        {
            if (DashForwardImage.fillAmount == 1)
            {
                _remainingTimeUntilDashForward = 0;
                DashForwardImage.fillAmount = _remainingTimeUntilDashForward / CooldownDashForward;
                RigidbodyWheel.AddForce(new Vector3(-DashForwardForce, 0, 0), ForceMode.Impulse);
                _audioManager.PlaySound(audioClip: _soundClips.WhooshingSound);
            }
        }
    }
}
