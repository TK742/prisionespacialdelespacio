using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Random = UnityEngine.Random;

public class IManager : MonoBehaviour
{
    [SerializeField] private ISpawner[] spawners;
    [SerializeField] private GameObject prefab;
    
    private int points;
    public static Action<int> OnPointsChange;
    public void Points(int points)
    {
        this.points += points;
        OnPointsChange?.Invoke(this.points);
    }

    public static IManager Instance => instance;
    private static IManager instance;

    private void Start()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);
    }

    public void RandomMultiSpawn(int quantity, float timeStep) => StartCoroutine(IRandomMultiSpawn(quantity, timeStep));
    private IEnumerator IRandomMultiSpawn(int quantity, float timeStep)
    {
        while (quantity > 0)
        {
            quantity--;
            foreach (var spawner in spawners)
                spawner.Spawn(1);
            yield return new WaitForSeconds(timeStep);
        }
    }

    public void RandomSingleSpawn(int quantity, float timeStep) => StartCoroutine(IRandomSingleSpawn(quantity, timeStep));
    private IEnumerator IRandomSingleSpawn(int quantity, float timeStep)
    {
        while (quantity > 0)
        {
            quantity--;
            spawners[Random.Range(0, spawners.Length)].Spawn(1);
            yield return new WaitForSeconds(timeStep);
        }
    }


    [System.Serializable]
    public class ISpawner
    {
        [SerializeField] private Transform spawnPoint;
        Queue<IEnemy> enemies = new Queue<IEnemy>();
        GameObject enemyPrefab;

        public void Initialize(GameObject enemyPrefab)
        {
            if (enemies == null) enemies = new Queue<IEnemy>();
            this.enemyPrefab = enemyPrefab;
            for (int i = 0; i < 10; i++)
            {
                enemies.Enqueue(Instantiate(enemyPrefab, spawnPoint).GetComponent<IEnemy>());
            }
        }

        public void Spawn(int quantity)
        {
            for (int i = 0; i < quantity; i++)
            {
                IEnemy enemy;
                if (enemies.Peek().Idle)
                {
                    enemy = enemies.Dequeue();
                    enemy.Initialize();
                }
                else
                {
                    enemy = Instantiate(enemyPrefab, spawnPoint).GetComponent<IEnemy>();
                    enemies.Enqueue(enemy);
                }
                enemy.transform.position = spawnPoint.position;
            }
        }
    }
}