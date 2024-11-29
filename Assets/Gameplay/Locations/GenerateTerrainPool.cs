using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrainPool : MonoBehaviour
{
    [SerializeField] private List<GameObject> _terrainsPrefab = new List<GameObject>();
    
    [SerializeField] private List<TerrainData> _terrainsPool = new List<TerrainData>();

    [SerializeField] private Transform _terrainStartSpawnPosition;
    [SerializeField] private Transform _wheelPosition;

    private Vector3 _terrainStartPosition;

    private bool _isPoolReleased = false;

    private void FixedUpdate()
    {
        if (_isPoolReleased)
        {
            GetTerrainPool();
        }

        if (_wheelPosition.position.x < _terrainStartSpawnPosition.position.x - 600)
        {
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
            GameObject tempObject = Instantiate(item, temp, terrainStartRotation);
            TerrainData terrainData = new TerrainData(tempObject);
            _terrainsPool.Add(terrainData);
            temp = new Vector3(temp.x - 200, temp.y, temp.z);
            item.SetActive(true);
            RandomiseTerrain();
        }
    }

    private void GetTerrainPool()
    {
        RandomiseTerrain();
        Vector3 lastTerrainPosition;
        lastTerrainPosition = _terrainsPool[0].TerrainTransform.position;

        foreach (var item in _terrainsPool)
        {
            if (item.TerrainTransform.position.x < lastTerrainPosition.x)
            {
                lastTerrainPosition = item.TerrainTransform.position;

            }
        }

        foreach (var item in _terrainsPool)
        {
            if (!item.TerrainComponent.enabled)
            {
                item.TerrainTransform.position = new Vector3(lastTerrainPosition.x - 200, lastTerrainPosition.y, lastTerrainPosition.z);
                item.TerrainComponent.enabled = true;
                lastTerrainPosition = item.TerrainTransform.position;
            }
        }
        _isPoolReleased = false;
    }

    private void ReleasePool()
    {
            for (int i = 0; i < _terrainsPool.Count; i++)
            {
                if (_terrainsPool[i].TerrainTransform.position.x > _wheelPosition.position.x)
                {
                    _terrainsPool[i].TerrainComponent.enabled = false;
                }
            }
            _terrainStartSpawnPosition.position = new Vector3(_terrainStartSpawnPosition.position.x - 600, 0, 0);
            _isPoolReleased = true;
    }

    public void ClearPool()
    {
        foreach (var item in _terrainsPool)
        {
            Destroy(item.TerrainGameObject);
        }
        _terrainsPool.Clear();
        _terrainStartSpawnPosition.position = _terrainStartPosition;
    }

    private void RandomiseTerrain()
    {
        for (int i = 0; i < _terrainsPool.Count; i++)
        {
            int randomIndex = Random.Range(0, _terrainsPool.Count);
            TerrainData temp = _terrainsPool[i];
            _terrainsPool[i] = _terrainsPool[randomIndex];
            _terrainsPool[randomIndex] = temp;
        }
    }
}
