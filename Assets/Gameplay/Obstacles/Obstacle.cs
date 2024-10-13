using System.Collections;
using UnityEngine;

public class Obstacle : MonoBehaviour
{
    protected BuffAndDebuffBarsPool _buffAndDebuffBarsPool;
    protected IEnumerator Spin()
    {
        while (true)
        {
            yield return null;
            transform.Rotate(new Vector3(0, 180 * Time.deltaTime, 0));
        }
    }

    protected void OnnObstacle()
    {
        if (transform.GetComponent<Renderer>() != null)
        {
            transform.GetComponent<Renderer>().enabled = true;
        }

        if (transform.GetComponent<MeshCollider>() != null)
        {
            transform.GetComponent<MeshCollider>().enabled = true;
        }
        else if (transform.GetComponent<SphereCollider>() != null)
        {
            transform.GetComponent<SphereCollider>().enabled = true;
        }
        else if (transform.GetComponent<BoxCollider>() != null)
        {
            transform.GetComponent<BoxCollider>().enabled = true;
        }
        else if (transform.GetComponent<CapsuleCollider>() != null)
        {
            transform.GetComponent<CapsuleCollider>().enabled = true;
        }

        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Renderer>() != null)
            {
                transform.GetChild(i).GetComponent<Renderer>().enabled = true;
            }

            if (transform.GetChild(i).GetComponent<MeshCollider>() != null)
            {
                transform.GetChild(i).GetComponent<MeshCollider>().enabled = true;
            }
            else if (transform.GetChild(i).GetComponent<SphereCollider>() != null)
            {
                transform.GetChild(i).GetComponent<SphereCollider>().enabled = true;
            }
            else if (transform.GetChild(i).GetComponent<BoxCollider>() != null)
            {
                transform.GetChild(i).GetComponent<BoxCollider>().enabled = true;
            }
            else if (transform.GetChild(i).GetComponent<CapsuleCollider>() != null)
            {
                transform.GetChild(i).GetComponent<CapsuleCollider>().enabled = true;
            }
        }
    }

    protected void OffObstacle()
    {
        if (transform.GetComponent<Renderer>() != null)
        {
        transform.GetComponent<Renderer>().enabled = false;
        }

        if (transform.GetComponent<MeshCollider>() != null)
        {
            transform.GetComponent<MeshCollider>().enabled = false;
        }
        else if (transform.GetComponent<SphereCollider>() != null)
        {
            transform.GetComponent<SphereCollider>().enabled = false;
        }
        else if (transform.GetComponent<BoxCollider>() != null)
        {
            transform.GetComponent<BoxCollider>().enabled = false;
        }
        else if (transform.GetComponent<CapsuleCollider>() != null)
        {
            transform.GetComponent<CapsuleCollider>().enabled = false;
        }
        
        for (int i = 0; i < transform.childCount; i++)
        {
            if (transform.GetChild(i).GetComponent<Renderer>() != null)
            {
                transform.GetChild(i).GetComponent<Renderer>().enabled = false;
            }

            if (transform.GetChild(i).GetComponent<MeshCollider>() != null)
            {
                transform.GetChild(i).GetComponent<MeshCollider>().enabled = false;
            }
            else if (transform.GetChild(i).GetComponent<SphereCollider>() != null)
            {
                transform.GetChild(i).GetComponent<SphereCollider>().enabled = false;
            }
            else if (transform.GetChild(i).GetComponent<BoxCollider>() != null)
            {
                transform.GetChild(i).GetComponent<BoxCollider>().enabled = false;
            }
            else if (transform.GetChild(i).GetComponent<CapsuleCollider>() != null)
            {
                transform.GetChild(i).GetComponent<CapsuleCollider>().enabled = false;
            }
        }

    }

}
