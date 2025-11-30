using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class WaveManager : MonoBehaviour
{
    [System.Serializable]
    public class WaveSetting
    {
        public string waveName = "Mixed Wave";
        
        public Character[] possibleSpawns; 
        
        public int count = 10;
        public float rate = 1.0f;
    }

    [Header("Wave Configuration")]
    [SerializeField] private List<WaveSetting> _waves;
    [SerializeField] private float _timeBetweenWaves = 3.0f;

    [Header("Spawn Settings")]
    [SerializeField] private Transform[] _spawnPoints;

    public int CurrentWaveIndex { get; private set; }
    public bool IsBossSpawned { get; private set; }

    private bool _isSpawning = false;

    private void Start()
    {
        CurrentWaveIndex = 0;
        StartWaves();
    }

    private void Update()
    {
        UpdateWaves();
    }

    public void StartWaves()
    {
        if (_waves.Count > 0)
        {
            StartCoroutine(SpawnWaveRoutine(CurrentWaveIndex));
        }
    }

    public void UpdateWaves()
    {
        if (GameManager.Instance.IsGameOver) return;
        if (_isSpawning) return;

        if (IsAllWaveCleared())
        {
            CurrentWaveIndex++;
            if (CurrentWaveIndex < _waves.Count)
            {
                StartCoroutine(SpawnWaveRoutine(CurrentWaveIndex));
            }
            else
            {
                if (GameManager.Instance != null)
                {
                    GameManager.Instance.WinGame();
                }
                
                enabled = false; // ปิด Script
            }
        }
    }

    public bool IsAllWaveCleared()
    {
        
        return GameObject.FindGameObjectsWithTag("Enemy").Length == 0;
    }

    private IEnumerator SpawnWaveRoutine(int index)
    {
        _isSpawning = true;
        yield return new WaitForSeconds(_timeBetweenWaves);

        WaveSetting currentWave = _waves[index];
        Debug.Log("Starting " + currentWave.waveName);

        foreach(var spawn in currentWave.possibleSpawns)
        {
            if (spawn is BossCommandShip)
            {
                IsBossSpawned = true;
                break;
            }
        }

        for (int i = 0; i < currentWave.count; i++)
        {
            if (currentWave.possibleSpawns.Length > 0)
            {
                int randomIndex = Random.Range(0, currentWave.possibleSpawns.Length);
                Character randomPrefab = currentWave.possibleSpawns[randomIndex];
                
                SpawnObject(randomPrefab);
            }
            
            yield return new WaitForSeconds(currentWave.rate);
        }

        _isSpawning = false;
    }

    private void SpawnObject(Character prefab)
    {
        Transform randomPoint = _spawnPoints[Random.Range(0, _spawnPoints.Length)];
        Instantiate(prefab, randomPoint.position, Quaternion.identity);
    }
}