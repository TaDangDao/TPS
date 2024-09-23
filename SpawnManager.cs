
using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
namespace Minigame.ZombieSurvival {
    public class SpawnManager : MonoBehaviour
    {
        int currentWaveIndex;
        int currentWaveSpawnCount;
        public WaveData[] waveData;
        public int maxEnemyCount = 300;
        float spawnTimer;
        float currentWaveDuration = 0f;
        public static SpawnManager instance;
        private void Start()
        {
            if (instance == null)
            {
                instance = this;
            }
        }
        private void Update()
        {
            spawnTimer -= Time.deltaTime;
            currentWaveDuration += Time.deltaTime;
            if (spawnTimer <= 0)
            {
                if (HasWaveEnd())
                {
                    currentWaveIndex++;
                    currentWaveDuration = currentWaveSpawnCount = 0;
                    if (currentWaveIndex >= waveData.Length)
                    {
                        enabled = false;
                    }
                    return;
                }
                if (!CanSpawn())
                {
                    spawnTimer += waveData[currentWaveIndex].GetSpawnInterval();
                    return;
                }
                GameObject[] spawns = waveData[currentWaveIndex].GetSpawns();
                foreach (GameObject prefab in spawns) {
                    if (!CanSpawn())
                    {
                        continue;
                    }
                    Instantiate(prefab, GeneratePostion(), Quaternion.identity);
                    currentWaveSpawnCount++;
                }
                spawnTimer += waveData[currentWaveIndex].GetSpawnInterval();
            }

        }

        private Vector3 GeneratePostion()
        {
            throw new NotImplementedException();
        }

        private bool CanSpawn()
        {
            if (HasExceededMaxEnemies())
            {
                return false;
            }
            if (instance.currentWaveSpawnCount > instance.waveData[instance.currentWaveIndex].totalSpawn) return false;
            if (instance.currentWaveDuration > instance.waveData[instance.currentWaveIndex].duration) return false;
            return true;
        }

        private bool HasExceededMaxEnemies()
        {
            if (!instance) return false;
            return false;
    }

        private bool HasWaveEnd()
        {
            WaveData currentWave = waveData[currentWaveIndex];
            if((currentWave.exitCondition & WaveData.ExitCondition.waveDuration) > 0)
            {
                if(currentWaveDuration <currentWave.duration) return false;

            }
            if ((currentWave.exitCondition & WaveData.ExitCondition.reachedTotalSpawns) > 0)
            {
                if (currentWaveSpawnCount < currentWave.totalSpawn) return false;

            }
            if(currentWave.mustKillAll&& EnemyStats.count >0) return false;
            return true;

        }
    }
}