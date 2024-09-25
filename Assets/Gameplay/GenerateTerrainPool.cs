using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrainsPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> _terrainsPool = new List<GameObject>();
    [SerializeField] private Transform _terrainStartPosition;
    [SerializeField] private Transform _wheelPosition;

    private void Start()
    {
       InitTerrains();
    }
    private void InitTerrains()
    {
        foreach (var terrain in _terrainsPrefab)
        {
            _terrainsPool.Add(terrain);
        }
    }

    private void GetTerrain()
    {

    }

    private void ReleasePool()
    {
        if (_wheelPosition.position.x < _terrainStartPosition.position.x - 600)
        {
            for (int i = 0; i < _terrainsPool.Count; i++)
            {
                if (_terrainsPool[i].transform.position.x > _wheelPosition.position.x)
                {
                    _terrainsPool[i].SetActive(true);
                }
            }
            _terrainStartPosition.position = new Vector3(_terrainStartPosition.position.x - 600,0,0);
        }
    }
}
