using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public  abstract class SpawnData : ScriptableObject
{
    // list possible object can spawm
   public GameObject[] possibleSpawnPrefabs = new GameObject[0];
    // thoi gian giua cac lan spawn
   public Vector2 spawninterval= new Vector2 (0,0);
    // so luong enemy moi lan spawn
    public Vector2Int spawnPerTick= new Vector2Int (1,1);
    // so luong quai sau moi gian trong 1 lan spawn 
    public float duration = 60f;
    public virtual GameObject[] GetSpawns(int totalEnemies)
    {
        int count =Random.Range (spawnPerTick.x,spawnPerTick.y);
        GameObject[] results= new GameObject[count];
        for(int i = 0; i < count; i++) {
            results[i] = possibleSpawnPrefabs[Random.Range(0,possibleSpawnPrefabs.Length)];
        }
        return results;
    }
    public virtual float GetSpawnInterval()
    {
        return Random.Range(spawninterval.x,spawninterval.y);
    }
}
