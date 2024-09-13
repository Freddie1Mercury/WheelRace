using UnityEngine;

public class EndGame : MonoBehaviour
{
    private Vector3 _startPosition;
    private Quaternion _startRotation;
    public Vector3 StartPosition { get { return _startPosition; } }
    [SerializeField] private WheelController _wheelController;
    [SerializeField] private GameObject _deathPanel;
    [SerializeField] private GameObject _wheel;

    private void Awake()
    {
        _startRotation = _wheel.transform.rotation;
        _startPosition = _wheel.transform.position;
    }

    private void Update()
    {
        if (_wheelController.WheelIsLive == false)
        {
            if (_wheel.transform.position != _startPosition)
            {
                _deathPanel.SetActive(true);
                _wheel.transform.GetComponent<Rigidbody>().isKinematic = true;
                _wheel.transform.position = _startPosition;
                _wheel.transform.rotation = _startRotation;
            }
        }
    }
}
