using UnityEngine;
using Utilities;
using Random = UnityEngine.Random;

public class Spawner : MonoBehaviour
{
    [SerializeField] private float _delayToNextSpawn = 2f;
    [SerializeField] private Transform[] _spawnPoints;
    [SerializeField] private Enemy[] _enemies;
    

    private float _timeToNextSpawn;

    private void Update()
    {
        if (ShouldSpawn())
        {
            Spawn();
        }
    }

    private bool ShouldSpawn()
    {
        if (!GameManager.Instance.IsPlaying()) return false;
        _timeToNextSpawn -= Time.deltaTime;
        if (_timeToNextSpawn > 0) return false;
        _timeToNextSpawn = _delayToNextSpawn;
        return true;
    }

    private void Spawn()
    {
        var spawnPoint = ChooseSpawnPoint();
        Enemy enemy = ChooseEnemy(); 
        Instantiate(enemy, spawnPoint.position, spawnPoint.rotation);
    }

    #region RANDOMIZERS

    

    private Enemy ChooseEnemy()
    {
        var randomIndex = Random.Range(0, _enemies.Length);
        var enemy = _enemies[randomIndex];
        return enemy;
    }

    private Transform ChooseSpawnPoint()
    {
        var randomIndex = Random.Range(0, _spawnPoints.Length);
        var spawnPoint = _spawnPoints[randomIndex];
        return spawnPoint;
    }
    
    
    
    #endregion
}