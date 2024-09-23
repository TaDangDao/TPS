using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName ="WaveData",menuName ="SpawnData/WaveData")]
public class WaveData : SpawnData
{
    public int startingCount = 0;
    public uint totalSpawn =uint.MaxValue;
    public enum ExitCondition
    {
        waveDuration=1,
        reachedTotalSpawns=2
    }
    public ExitCondition exitCondition = (ExitCondition)1;
    public bool mustKillAll=false;
    public uint spawnCount;
    public override GameObject[] GetSpawns(int totalEnemies=0)
    {
        int count = Random.Range(spawnPerTick.x, spawnPerTick.y);
        if(totalEnemies + count < startingCount)
        {
            count=startingCount-totalEnemies;
        }
        GameObject[] results = new GameObject[count];
        for (int i = 0; i < count; i++)
        {
            results[i] = possibleSpawnPrefabs[Random.Range(0, possibleSpawnPrefabs.Length)];
        }
        return results;
    }
}
