using System;
using System.Collections.Generic;
using UnityEngine;

public class GroupSpawner : MonoBehaviour
{
    public enum GState
    {
        DEFAULT = 0,    //Enemies are being spawned
        COMPLETE = 1,   //Enemies have finished spawning
    }

    public GState State;

    [SerializeField] private List<GameObject> enemies;
     private float spawnCounter;
    [SerializeField] private float duration;    //The gap between spawning each enemy

    public Action OnGroupSpawningComplete;

    private void Awake()
    {
        SetState(GState.DEFAULT);
        spawnCounter = duration;
    }

    private void SetState(GState state)
    {
        State = state;
    }

    private void Update()
    {
        if(State == GState.DEFAULT)
        {
            spawnCounter -= Time.deltaTime;

            if(spawnCounter < 0)
            {
                SpawnNext();
            }

            SpawningComplete();
        }
    }

    private void SpawnNext()
    {
        if (enemies.Count == 0) return;

        enemies[0].SetActive(true);
        spawnCounter = duration;

        RemoveFromGroup();
    }

    private void RemoveFromGroup()
    {
        //Remove top enemy from the group
        if (enemies.Count == 0) return;

        enemies.RemoveAt(0);
    }

    private void SpawningComplete()
    {
        if(enemies.Count == 0)
        {
            OnGroupSpawningComplete?.Invoke();
            SetState(GState.COMPLETE);
        }
    }
}
