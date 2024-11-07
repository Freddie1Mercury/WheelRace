using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrainsPrefab = new List<GameObject>();
    [SerializeField] private List<GameObject> _terrainsPool = new List<GameObject>();
    [SerializeField] private Transform _terrainStartSpawnPosition;
    [SerializeField] private Transform _wheelPosition;

    private Vector3 _terrainStartPosition;

    private void FixedUpdate()
    {
        if (_terrainsPool.Count > 0)
        {
            RandomiseTerrain();
            GetTerrainPool();
            ReleasePool();
        }
    }

    private void Start()
    {
        _terrainStartPosition = _terrainStartSpawnPosition.position;
    }
    public void InitTerrains()
    {
        Vector3 temp = _terrainStartSpawnPosition.position;
        Quaternion terrainStartRotation = _terrainStartSpawnPosition.rotation;
        temp = new Vector3(_terrainStartSpawnPosition.position.x - 200, _terrainStartSpawnPosition.position.y, _terrainStartSpawnPosition.position.z);


        foreach (var item in _terrainsPrefab)
        {
            _terrainsPool.Add(Instantiate(item, temp, terrainStartRotation));
            temp = new Vector3(temp.x - 200, temp.y, temp.z);
            item.SetActive(true);
        }



    }

    private void GetTerrainPool()
    {
        Vector3 lastTerrainPosition;
        lastTerrainPosition = _terrainsPool[0].GetComponent<Transform>().position;

        foreach (var item in _terrainsPool)
        {
            if (item.transform.position.x < lastTerrainPosition.x)
            {
                lastTerrainPosition = item.transform.position;

            }
        }

        foreach (var item in _terrainsPool)
        {
            if (!item.GetComponent<Terrain>().enabled)
            {
                item.transform.position = new Vector3(lastTerrainPosition.x - 200, lastTerrainPosition.y, lastTerrainPosition.z);
                item.GetComponent<Terrain>().enabled = true;
                lastTerrainPosition = item.transform.position;
            }
        }
    }

    private void ReleasePool()
    {
        if (_wheelPosition.position.x < _terrainStartSpawnPosition.position.x - 600)
        {
            for (int i = 0; i < _terrainsPool.Count; i++)
            {
                if (_terrainsPool[i].transform.position.x > _wheelPosition.position.x)
                {
                    _terrainsPool[i].GetComponent<Terrain>().enabled = false;
                }
            }
            _terrainStartSpawnPosition.position = new Vector3(_terrainStartSpawnPosition.position.x - 600, 0, 0);
        }
    }

    public void ClearPool()
    {
        foreach (var item in _terrainsPool)
        {
            Destroy(item);
        }
        _terrainsPool.Clear();
        _terrainStartSpawnPosition.position = _terrainStartPosition;
    }

    private void RandomiseTerrain()
    {
        for (int i = 0; i < _terrainsPool.Count; i++)
        {
            int randomIndex = Random.Range(0, _terrainsPool.Count);
            GameObject temp = _terrainsPool[i];
            _terrainsPool[i] = _terrainsPool[randomIndex];
            _terrainsPool[randomIndex] = temp;
        }
    }
}
