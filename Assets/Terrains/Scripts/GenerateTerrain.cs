using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.Pool;

public class GenerateTerrain : MonoBehaviour
{
    [SerializeField] private WheelController _wheelController;
    [SerializeField] private List<Terrain> terrainPrefabs;
    [SerializeField] private int gridSize = 5;
    [SerializeField] private float terrainSize = 200f;
    [SerializeField] private float iterSteps = 3;
    [SerializeField] private int poolSize = 10;  // Размер пула
    private float multiplier = 0;
    // Пул объектов Terrain
    public ObjectPool<Terrain> terrainPool;

    private List<Terrain> activeTerrains;  // Список активных террейнов

    private void Start()
    {
        activeTerrains = new List<Terrain>();
        // Инициализация ObjectPool с кастомными методами
        terrainPool = new ObjectPool<Terrain>(
            CreateTerrain,     // Метод для создания террейнов
            OnGetTerrain,      // Что делать при получении террейна из пула
            OnReleaseTerrain,  // Что делать при возврате террейна в пул
            OnDestroyTerrain,  // Что делать при уничтожении террейна, если превышен пул
            false,             // Если false, объекты не уничтожаются при переполнении пула
            poolSize,          // Максимальное количество террейнов в пуле
            poolSize           // Изначальное количество террейнов
        );
    }

    private Terrain CreateTerrain()
    {
        return Instantiate(terrainPrefabs[Random.Range(0, terrainPrefabs.Count)]);
    }

    private void OnGetTerrain(Terrain terrain)
    {
        terrain.gameObject.SetActive(true);
    }

    public void OnReleaseTerrain(Terrain terrain)
    {
        terrain.gameObject.SetActive(false);
    }

    private void OnDestroyTerrain(Terrain terrain)
    {
        Destroy(terrain.gameObject);
    }

    public IEnumerator Generate()
    {
        if (terrainPool != null)
        {
            Terrain[] terrains = FindObjectsOfType<Terrain>();
            foreach (Terrain terrain in terrains)
                if (terrain.gameObject.tag == "TempTerrains")
                    terrainPool.Release(terrain);
        }

        multiplier = 0;
        while (_wheelController.WheelIslive)
        {
            Debug.Log("multik befor all" + multiplier);
            for (int x = 0; x < gridSize * 200; x += 200)
            {
                Terrain terrain = terrainPool.Get();  // Берем террейн из пула
                Vector3 spawnPosition = new Vector3(-(x + terrainSize + 360) + multiplier, 0, 0);
                terrain.transform.position = spawnPosition;
                activeTerrains.Add(terrain);
                yield return new WaitForSeconds(0.5f);
            }

            yield return new WaitWhile(() => (activeTerrains.Last().transform.position.x + 400 < _wheelController.transform.position.x));

            multiplier += -(gridSize * 200);

            foreach (Terrain terrain in activeTerrains)
            {
                if (terrain == activeTerrains[(int)(activeTerrains.Count() / 2f) + 1])
                    break;
                if (terrain != null || terrain.gameObject.tag == "TempTerrains")
                    terrainPool.Release(terrain);
            }
            //activeTerrains.Clear();
            activeTerrains.RemoveRange(0, (int)(activeTerrains.Count() / 2f));
        }
    }
}


//using System.Collections;
//using System.Collections.Generic;
//using UnityEngine;

//public class GenerateTerrain : MonoBehaviour
//{
//    [SerializeField]
//    private WheelController _wheelController;
//    [SerializeField]
//    private List<Terrain> terrainPrefabs;
//    [SerializeField]
//    private int gridSize = 1;
//    [SerializeField]
//    private float terrainSize = 200f;
//    [SerializeField]
//    private float iterSteps = 3;

//    public IEnumerator Generate()
//    {
//        float multiplier = 0;
//        while (_wheelController.WheelIsLive == true)
//        {

//            for (int x = 0; x < gridSize * 200; x += 200)
//            {
//                Terrain terrainPrefab = terrainPrefabs[Random.Range(0, terrainPrefabs.Count)];
//                Vector3 spawnPosition = new Vector3(-(x + terrainSize + 360) + multiplier, 0, 0);
//                Instantiate(terrainPrefab, spawnPosition, Quaternion.identity);
//            }
//            yield return new WaitForSeconds(1f);
//            multiplier += -(gridSize * 200);
//        }
//        Terrain[] terrains = FindObjectsOfType<Terrain>();
//        foreach (Terrain terrain in terrains)
//            if (terrain.gameObject.tag == "TempTerrains")
//                Destroy(terrain.gameObject);
//        StopCoroutine(Generate());
//    }
//}
