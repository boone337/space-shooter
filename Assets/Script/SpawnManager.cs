using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
//using TMPro.EditorUtilities;
//using UnityEditor.Experimental.GraphView;
//using UnityEditorInternal;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PowerUp _powerUp;

    private Player _player;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField] private GameObject _enemyPrefab2;

    [SerializeField] private GameObject _enemyPrefab3;

    [SerializeField] private GameObject _bossPrefab;

    //  [SerializeField] private float _spawnTime = 6f;

    [SerializeField] private int _waveNumber = 0;

    private bool _startWave = true;


    [SerializeField] private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject  _asteroidPrefab;

    [SerializeField]
    private int randomAsteroidID; 


    private UIScript _uIScript;

    [SerializeField]
    private GameObject[] _powerup;

    //most frequent down to rarity
    public int total;   //can be total weight or whatever you choose to call it

    public int RandomNumber;

    public int[] table = //{ 60, 30, 10 }; or can do as shown below
    {
     
    50,  //ammo
    20,  //Shield
    15,  //Triple
    10, //Homing
    5, //bomb

    };

    // Start is called before the first frame update
    void Start()
    {
        _uIScript = GameObject.Find("Canvas").GetComponent<UIScript>();

        // _powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();

        _player = GameObject.Find("Player").GetComponent<Player>();

        _stopSpawning = false;
    }

    public void StartSpawning()

    {
     
            StartCoroutine(SpawnEnemyRoutine());

            StartCoroutine(SpawnEnemyRoutine2());

            StartCoroutine(SpawnPowerUpRoutine());

            StartCoroutine(SpawnAsteroidRoutine());

            StartCoroutine(EnemyWave());

            _uIScript.UpdateChangeWaveCount(_waveNumber);
        
    }
    
    IEnumerator SpawnPowerUpRoutine()
    {
        while (_player != null)
        {
            WeightPowerUp();

            yield return new WaitForSeconds(3.0F);
        }
    }
    public void WeightPowerUp()
    {


        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
        foreach (var item in table)
        {
            total += item;

        }

        RandomNumber = Random.Range(0, total);

        foreach (var weight in table)
        {
            for (int i = 0; i < table.Length; i++)
            {
                if (RandomNumber <= weight)
                {

                    Instantiate(_powerup[i], posToSpawn, Quaternion.identity);
                    return;
                }

                else
                {

                    RandomNumber -= weight;
                }

            }
        }

    }

    // Update is called once per frame
    void Update()
    {
        //WeightPowerUp();
        
    }

  
    public IEnumerator EnemyWave()
    {
        Debug.Log("enemy wave started");
        while (_startWave == true)  //or could have used _stopSpawning == false
        {

            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 12f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

            Debug.Log(" EnemyPrefabSpawned");
           // GameObject newEnemy1 = Instantiate(_enemyPrefab2, posToSpawn, Quaternion.identity);  

            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5F);   //(5f / 1.5f);
            // _spawnTime /= 1.5f;  I can add this in yield time under _spawntime 

            _waveNumber += 1;

            _uIScript.UpdateChangeWaveCount(_waveNumber);



            if (_waveNumber == 5)
            {
                _startWave = false;
                _stopSpawning = true;


            }

            if (_waveNumber == 5  && _player != null)  //previously had if stopSpawning is true and player != null instantiate boss 
            {
                _startWave = false;                // lets see if this will stop boss from spawning 2x..it did so it was spawning boss as soon as stop spawning = true
                                                    //which was the case when the player dies and it spawned boss  (problem fixed 7.2.23)
                Boss();

                _uIScript._bossLifeBar.enabled = true;

                _uIScript.UpdateBossText();
            }
        }



      

    }

    IEnumerator SpawnEnemyRoutine2()
    {
        while (_startWave == true )
        {
            yield return new WaitForSeconds( 15f
                );

            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 12f, 0);

            GameObject newEnemy1 = Instantiate(_enemyPrefab2, posToSpawn, Quaternion.identity);

            Debug.Log("EnemyPrefab2Spawned");

            newEnemy1.transform.parent = _enemyContainer.transform;

            
        }



    }
    IEnumerator SpawnAsteroidRoutine()
    {
        while (_stopSpawning == false)
        {
           

            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

             GameObject Asteroid = Instantiate(_asteroidPrefab, posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3.0f,8.0f));
        }
    }

    IEnumerator SpawnEnemyRoutine()
    {
       

          while (_startWave == true)    // while (_stopSpawning == false)
        {
            yield return new WaitForSeconds(Random.Range(5.0f, 10.0f));

            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 12f, 0);

           GameObject newEnemy = Instantiate(_enemyPrefab3, posToSpawn, Quaternion.identity);

            Debug.Log("SpawnEnemyPrefab3");

            

        }
    }

   

   
    public void OnPlayerDeath()
    {
        _stopSpawning = true;

        _startWave = false;

    }

    public void Boss()
    {
        Debug.Log("boss Spawned");

        Vector3 posToSpawn = new Vector3(0, 16f, 0);

        GameObject newEnemy = Instantiate(_bossPrefab, posToSpawn, Quaternion.identity);

    }

}

    
