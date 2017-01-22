using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    [SerializeField]
    protected GameObject[] enemyPrefabs;

    [SerializeField]
    protected int[] enemyCounts;

    [SerializeField]
    protected GameObject[] destructablePrefabs;

    [SerializeField]
    protected float enemyRespawnTime = 10;

    [SerializeField]
    protected int numDestructables = 40;

    /// <summary>
    /// Multiplied by Time.timeSinceLevelLoad to determine how many enemies should be active.
    /// </summary>
    [SerializeField]
    float enemiesActiveMultiplier = 0.1f;

    float numEnemiesActive = 0;
    int currentDestructables = 0;
    LayerMask stageBoundary;

    void Start () {
        stageBoundary = LayerMask.GetMask("StageBoundary");
        Assert.IsTrue(enemyPrefabs.Length == enemyCounts.Length);
    }
	
	void Update () {
        //while we need to spawn enemies
        while(numEnemiesActive < Mathf.Ceil(Time.timeSinceLevelLoad * enemiesActiveMultiplier)) {
            numEnemiesActive++;
            Callback.FireAndForget(SpawnEnemy, enemyRespawnTime, this);
        }

        while (currentDestructables < numDestructables) {
            currentDestructables++;
            Vector2 location = new Vector2(Random.value * Navigation.navigationWidth, -Random.value * Navigation.navigationHeight);
            //while location is not in the walkable map
            while (Physics2D.OverlapPoint(location, stageBoundary) == null) {
                location = new Vector2(Random.value * Navigation.navigationWidth, -Random.value * Navigation.navigationHeight);
            }

            //choose which enemy type to spawn
            int destructibleIndex = Random.Range(0, destructablePrefabs.Length);
            GameObject destructable = Instantiate(destructablePrefabs[destructibleIndex]);


            DestructibleTracker destructibleTracker = destructable.AddComponent<DestructibleTracker>();
            //set up death tracking
            destructibleTracker.source = this;
            //move to location
            destructable.transform.position = location;

        }
	}

    void SpawnEnemy() {
        Vector2 location = new Vector2(Random.value * Navigation.navigationWidth, -Random.value * Navigation.navigationHeight);
        //while location is not in the walkable map
        while(Physics2D.OverlapPoint(location, stageBoundary) == null) {
            location = new Vector2(Random.value * Navigation.navigationWidth, -Random.value * Navigation.navigationHeight);
        }

        //choose which enemy type to spawn
        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        //spawn a group of enemies
        for (int i = 0; i < enemyCounts[enemyIndex]; i++) {
            GameObject enemy = Instantiate(enemyPrefabs[enemyIndex]);


            SpawnTracker spawnTracker = enemy.AddComponent<SpawnTracker>();
            //set up death tracking
            spawnTracker.source = this;
            spawnTracker.weight = 1f / enemyCounts[enemyIndex];
            //move to location
            enemy.transform.position = location;
        }
    }

    public void EnemyDeath(float weight) {
        Debug.Log(numEnemiesActive);
        numEnemiesActive-= weight;
    }

    public void DestructibleDeath() {
        numDestructables--;
    }
}
