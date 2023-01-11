using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SimpleAISpawner : MonoBehaviour
{
    public GameObject targetSpawner;
    public GameObject enemyPrefab;

    [SerializeField] private GameObject enemy;

    private void Update()
    {
        if (enemy == null)
            EnemySpawnerLogic();
    }

    private void EnemySpawnerLogic()
    {
        enemy = Instantiate(enemyPrefab,this.transform.position,Quaternion.identity);
        //enemy.GetComponent<Enemy>().SetTarget(targetSpawner);
    }
}
