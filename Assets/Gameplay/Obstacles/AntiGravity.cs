using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class AntiGravity : Obstacle
{
    private void OnCollisionEnter(Collision collision)
    {
        int delayInSecond = 5;
        collision.rigidbody.useGravity = false;
        WaitEndBuffOrDebuff(delayInSecond);
        collision.rigidbody.useGravity = true;
    }


}
