using UnityEngine;

public class ForceObstacle : Buff
{
    [SerializeField] private bool applyForceOnX;
    [SerializeField] private bool applyForceOnY;
    [SerializeField] private int _forceOnX = 30;
    [SerializeField] private int _forceOnY = 30;
    
    private void OnTriggerEnter(Collider other)
    {
        if (applyForceOnX)
            other.GetComponent<Rigidbody>().AddForce(new Vector3(-_forceOnX,0,0), ForceMode.Impulse);
        if (applyForceOnY)
            other.GetComponent<Rigidbody>().AddForce(new Vector3(0, _forceOnY, 0), ForceMode.Impulse);
        transform.gameObject.SetActive(false);
    }

    private void Start()
    {
        StartCoroutine(Spin());
    }
}
