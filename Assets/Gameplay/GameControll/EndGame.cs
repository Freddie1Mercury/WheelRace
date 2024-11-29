using System.Collections;
using UnityEngine;
using YG;

public class EndGame : MonoBehaviour
{
    private Vector3 _startPosition;

    private Quaternion _startRotation;

    public Vector3 StartPosition { get { return _startPosition; } }

    [SerializeField] private WheelController _wheelController;
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private DistanceCounter _distanceCounter;
    [SerializeField] private GenerateTerrainPool _terrainPool;
    [SerializeField] private UIManager _uiManager;
    [SerializeField] private Obstacle _obstacle;
    [SerializeField] private AudioManager _audioManager;

    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private GameObject _wheel;


    private void Awake()
    {
        _startRotation = _wheel.transform.rotation;
        _startPosition = _wheel.transform.position;
    }

    public void Death()
    {
        if (_wheel.transform.position != _startPosition && !_wheelController.WheelIslive)
        {
            _deathPanel.SetActive(true);
            _wheel.transform.GetComponent<Rigidbody>().isKinematic = true;
            _wheel.transform.position = _startPosition;
            _wheel.transform.rotation = _startRotation;
            _terrainPool.ClearPool();
            _moneyManager.AddMoneyForLastSession();
            _uiManager.GameUIDisable();
            _obstacle.ClearBuffAndDebuff();
            YandexGame.FullscreenShow();
        }
    }

    public IEnumerator CheckDeath()
    {
        while (true)
        {
            if (_wheelController.RigidbodyWheel.velocity.x >= -0.2 && transform.position != StartPosition)
            {
                yield return new WaitForSeconds(2);
                if (_wheelController.RigidbodyWheel.velocity.x >= -0.2 && transform.position != StartPosition)
                {
                   _wheelController.WheelIslive = false;
                    yield return new WaitForSeconds(0.1f);
                    Death();
                }
            }
            else
            {
                yield return new WaitForSeconds(0.5f);
            }
        }
    }
}
