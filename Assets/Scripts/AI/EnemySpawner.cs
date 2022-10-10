using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public List<GroupSpawner> spawns;

    [SerializeField] private GroupSpawner currentSpawner;

    [SerializeField] private int index;
    private void Start()
    {
        index = 0;

        SpawnNextGroup();
    }

    private void OnGroupSpawningCompleteHandler()
    {
        //Switch spawner to the next. Unsubscribe from the current one
        currentSpawner.OnGroupSpawningComplete -= OnGroupSpawningCompleteHandler;
        index++;

        SpawnNextGroup();
    }

    private void SpawnNextGroup()
    {
        if (index == spawns.Count) return;
        
        currentSpawner = spawns[index];
        currentSpawner.gameObject.SetActive(true);
        currentSpawner.OnGroupSpawningComplete += OnGroupSpawningCompleteHandler;
    }
}
