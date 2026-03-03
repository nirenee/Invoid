using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using UnityEngine;

[System.Serializable]
public class EnemyType
{
    public GameObject enemyprefab;
    public float cost;
}

public class EnemySpawner : MonoBehaviour
{
    public Transform[] SpawnPoints;
    public List<EnemyType> enemies = new List<EnemyType>();
    public float levelcost;

    private void Start()
    {
        StartCoroutine(InstantiateEnemies());
    }
    private IEnumerator InstantiateEnemies()
    {
        while (levelcost > 0)
        {
            List<EnemyType> affordableEnemy = enemies.FindAll(e => e.cost <= levelcost);
            if(affordableEnemy.Count == 0)
            {
                break;
            }

            EnemyType selected = affordableEnemy[Random.Range(0, affordableEnemy.Count)];
            Transform sp = SpawnPoints[Random.Range(0, SpawnPoints.Length)];
            Instantiate(selected.enemyprefab, sp.position, sp.rotation);
            levelcost -= selected.cost;
            yield return new WaitForSeconds(2f);

        }
    }
}
