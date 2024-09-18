using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected IEnumerable WaitEndBuffOrDebuff(int delayInSecond)
    {
       yield return new WaitForSeconds(delayInSecond);
    }
}
