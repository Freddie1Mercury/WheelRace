using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WheelController : MonoBehaviour
{
    private bool _wheelIslive = false;
    public bool WheelIsLive => _wheelIslive;
    [SerializeField] private EndGame _endGame;
<<<<<<< Updated upstream

    private void Start()
=======
    [SerializeField] private GenerateTerrain _generateTerrain;
    private void Update()
>>>>>>> Stashed changes
    {
      
        StartCoroutine(CheckDeath());
        
    }
    private IEnumerator CheckDeath()
    {
        while (true)
        {
            if (transform.GetComponent<Rigidbody>().velocity.x >= -0.2 && transform.position != _endGame.StartPosition)
            {
                yield return new WaitForSeconds(2);
                if (transform.GetComponent<Rigidbody>().velocity.x >= -0.2 && transform.position != _endGame.StartPosition)
                {
                    _wheelIslive = false;
                    yield return new WaitForSeconds(1);

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
        _wheelIslive = true;
        transform.GetComponent<Rigidbody>().isKinematic = false;
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(-30, 0, 0), ForceMode.Impulse);
        StartCoroutine(_generateTerrain.Generate());
    }

    public void DashLeft()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, -20), ForceMode.Impulse);
    }

    public void DashRight()
    {
        transform.GetComponent<Rigidbody>().AddForce(new Vector3(0, 0, 20), ForceMode.Impulse);
    }
}
