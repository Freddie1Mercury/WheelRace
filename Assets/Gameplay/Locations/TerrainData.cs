using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TerrainData : MonoBehaviour
{
    public GameObject TerrainGameObject; 
    public Terrain TerrainComponent;       
    public Transform TerrainTransform;   

    public TerrainData(GameObject location)
    {
        TerrainGameObject = location;
        TerrainComponent = location.GetComponent<Terrain>();
        TerrainTransform = location.transform;
    }
}
