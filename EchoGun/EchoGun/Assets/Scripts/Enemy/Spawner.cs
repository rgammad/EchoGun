using UnityEngine;
using UnityEngine.Assertions;
using System.Collections;
using System.Collections.Generic;

public class Spawner : MonoBehaviour {

    [SerializeField]
    protected GameObject[] enemyPrefabs;

    [SerializeField]
    protected float enemyRespawnTime = 10;

    /// <summary>
    /// Multiplied by Time.timeSinceLevelLoad to determine how many enemies should be active.
    /// </summary>
    [SerializeField]
    float enemiesActiveMultiplier = 0.1f;

    int numEnemiesActive = 0;
    LayerMask stageBoundary;

    void Start () {
        stageBoundary = LayerMask.GetMask("StageBoundary");
    }
	
	void Update () {
        //while we need to spawn enemies
        while(numEnemiesActive < Mathf.Ceil(Time.timeSinceLevelLoad * enemiesActiveMultiplier)) {
            numEnemiesActive++;
            Callback.FireAndForget(SpawnEnemy, enemyRespawnTime, this);
        }
	}

    void SpawnEnemy() {
        Vector2 location = new Vector2(Random.value * Navigation.navigationWidth, -Random.value * Navigation.navigationHeight);
        //while location is not in the walkable map
        while(Physics2D.OverlapPoint(location, stageBoundary) == null) {
            location = new Vector2(Random.value * Navigation.navigationWidth, -Random.value * Navigation.navigationHeight);
        }

        int enemyIndex = Random.Range(0, enemyPrefabs.Length);

        GameObject enemy = Instantiate(enemyPrefabs[enemyIndex]);
        enemy.transform.position = location;
    }
}
