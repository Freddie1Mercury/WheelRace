using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected IEnumerator Spin()
    {
        while (true)
        {
        yield return null;
        transform.Rotate(new Vector3(0,180 * Time.deltaTime,0));
        }
    }
}
