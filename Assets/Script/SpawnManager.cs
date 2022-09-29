using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{




    [SerializeField]
    private GameObject _enemyPrefab;
    [SerializeField]
    private GameObject _enemyContainer;
    private bool _stopSpawning = false;
    [SerializeField]
    private GameObject _powerUpPrefab;
    [SerializeField]
    private GameObject _speed_Powerup;

    [SerializeField]
    private GameObject[] _powerups;

    [SerializeField]
    private GameObject _asteroidPrefab;

    // Start is called before the first frame update
    void Start()
    {
    }

    public void StartSpawning()

    {
        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerUpRoutine());

        StartCoroutine(SpawnAsteroidRoutine());
    }

    // Update is called once per frame
    void Update()
    {



    }
    //Spawn game objects every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield Events
    // while loops
    IEnumerator SpawnAsteroidRoutine()
    {
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            GameObject Asteroid = Instantiate(_asteroidPrefab, posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(5.0f);
        }


    }

    IEnumerator SpawnEnemyRoutine()
    {
        Debug.Log("spawn");
        yield return new WaitForSeconds(3.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);
            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(5.0f);

            //never get here because in loop
            //then his line is called     
            //while loop (infinite loop)
            //Instantiate enemy prefab
            //yield wait for 5 sec. 

        }

    }



    IEnumerator SpawnPowerUpRoutine()
    {

        yield return new WaitForSeconds(3.0f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
            int randomPowerUp = Random.Range(0, 3);
            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);
            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));

        }

    }

    public void OnPlayerDeath()
    {
        _stopSpawning = true;

    }

}

    
