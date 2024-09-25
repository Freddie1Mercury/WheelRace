using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGame : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    public Vector3 StartPosition { get { return _startPosition; } }

    [SerializeField] private WheelController _wheelController;
    [SerializeField] private MoneyManager _moneyManager;
    [SerializeField] private DistanceCounter _distanceCounter;

    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private GameObject _wheel;

    [SerializeField] private List<GameObject> _uslessInGameSessionUi = new List<GameObject>();

    private void Awake()
    {
        _startRotation = _wheel.transform.rotation;
        _startPosition = _wheel.transform.position;
    }

    public void Death()
    {
        if (_wheel.transform.position != _startPosition)
        {
            _deathPanel.SetActive(true);
            _wheel.transform.GetComponent<Rigidbody>().isKinematic = true;
            _wheel.transform.position = _startPosition;
            _wheel.transform.rotation = _startRotation;
            // отключения ui который используется в матче
            foreach (var UiElement in _uslessInGameSessionUi)
            {
                UiElement.SetActive(false);
            }
            _moneyManager.AddMoneyForLastSession();
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
                yield return null;
            }
        }
    }
}
