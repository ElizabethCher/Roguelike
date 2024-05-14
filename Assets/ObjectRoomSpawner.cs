using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObjectRoomSpawner : MonoBehaviour
{
    [System.Serializable]
    public struct RandomSpawner
    {
        public string name;
        public SpawnerData spawnerData;
    }
    public GridControll grid;
    public RandomSpawner[] spawnerData;
    // Start is called before the first frame update
    void Start()
    {
        //grid = GetComponentInChildren<GridControll>();
    }
    public void InitialiseObjectSpawner()
    {
        foreach(RandomSpawner rs in spawnerData)
        {
            SpawnObjects(rs);
        }
    }
    void SpawnObjects(RandomSpawner data)
    {
        int randomIteration = Random.Range(data.spawnerData.minSpawn, data.spawnerData.maxSpawn + 1);
        for (int i=0 ; i<randomIteration; i++)
        {
            int randomPos = Random.Range(0, grid.avalibletPoints.Count-1);
            GameObject go = Instantiate(data.spawnerData.itemToSpawn, grid.avalibletPoints[randomPos], Quaternion.identity, transform) as GameObject;
            grid.avalibletPoints.RemoveAt(randomPos);
            Debug.Log("Spawned Object!");
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
