using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GenerateTerrain : MonoBehaviour
{
    [SerializeField]
    private WheelController _wheelController;
    [SerializeField]
    private List<Terrain> terrainPrefabs;
    [SerializeField]
    private int gridSize = 10;
    [SerializeField]
    private float terrainSize = 200f;
    [SerializeField]
    private float iterSteps = 3;

    public IEnumerator Generate()
    {
        float multiplier = 0;
        while (_wheelController.WheelIsLive == true)
        {

            for (int x = 0; x < gridSize * 200; x += 200)
            {
                Terrain terrainPrefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Count)];
                Vector3 spawnPosition = new Vector3(-(x + terrainSize + 360) + multiplier, 0, 0);
                Instantiate(terrainPrefab, spawnPosition, Quaternion.identity);
            }
            yield return new WaitForSeconds(1f);
            multiplier += -(gridSize * 200);
        }
        Terrain[] terrains = FindObjectsOfType<Terrain>();
        foreach (Terrain terrain in terrains)
            if (terrain.gameObject.tag == "TempTerrains")
                Destroy(terrain.gameObject);
        StopCoroutine(Generate());
    }
}
