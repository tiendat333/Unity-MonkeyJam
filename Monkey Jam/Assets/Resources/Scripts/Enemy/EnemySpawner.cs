using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawner : MonoBehaviour
{
    public enum SpawnState { SPAWNING, WAITING, COUNTING};

    //VARIABLES
    [SerializeField] private Wave[] waves;

    [SerializeField] private float timeBetweenWaves = 3f;
    [SerializeField] private float waveCountdown = 0;

    private SpawnState state = SpawnState.COUNTING;

    private int currentWave;

    //REFERENCES
    [SerializeField] private Transform[] spawners;
    [SerializeField] private List<CharacterStats> enemyList;

    private void Start()
    {
        waveCountdown = timeBetweenWaves;
        currentWave = 0;
    }

    private void Update()
    {
        if (state == SpawnState.WAITING)
        {
            //Check if all enemies are dead
            if (!EnemiesAreDead())
                return;
            else
                CompleteWave();
            
        }

        if (waveCountdown <= 0)
        {            
            if (state != SpawnState.SPAWNING)
            {
                //Spawn enemies
                StartCoroutine(SpawnWave(waves[currentWave]));
            }
        }
        else
        {
            waveCountdown -= Time.deltaTime;
        }
    }


    private IEnumerator SpawnWave (Wave wave)
    {
        state = SpawnState.SPAWNING;

        for (int i = 0; i < wave.enemiesAmount; i++)
        {
            SpawnBanana(wave.enemy);
            yield return new WaitForSeconds(wave.delay);
        }
        

        state = SpawnState.WAITING;

        yield break;
    }

    private void SpawnBanana(GameObject enemy)
    {
        int randomInt = Random.RandomRange(1, spawners.Length);
        Transform randomSpawner = spawners[randomInt];


        GameObject newEnemy = Instantiate(enemy, randomSpawner.position, randomSpawner.rotation);
        CharacterStats newEnemyStats = newEnemy.GetComponent<CharacterStats>();

        enemyList.Add(newEnemyStats);

    }

    private bool EnemiesAreDead()
    {
        int i = 0;
        foreach (CharacterStats enemy in enemyList)
        {
            if (enemy.IsDead())
                i++;
            else
                return false;
        }
        return true;
    }

    private void CompleteWave()
    {
        Debug.Log("WAVE COMPLETED");

        state = SpawnState.COUNTING;
        waveCountdown = timeBetweenWaves;
        if (currentWave + 1 > waves.Length - 1)
        {
            currentWave = 0;
            Debug.Log("COMPLETED ALL THE WAVES");
        }
        else        
            currentWave++;                
    }
}
