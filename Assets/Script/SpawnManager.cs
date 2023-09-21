using System.Collections;
//using System.Collections.Generic;
//using System.Diagnostics;
//using System.Runtime.InteropServices.WindowsRuntime;
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

    [SerializeField]
    private GameObject _enemyPrefab2;

    [SerializeField] 
    private GameObject _enemyPrefab3;

    [SerializeField] 
    private GameObject _bossPrefab;

    //  [SerializeField] private float _spawnTime = 6f;

    [SerializeField] 
    private int _waveNumber ;

    [SerializeField]
    private bool _startWave = true;

    [SerializeField] 
    private GameObject _enemyContainer;

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

    private void Update()
    {
        
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
    private void WeightPowerUp()
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
   public void WaveNumber( )
    {
        if (_startWave == true)
        {
            _waveNumber += 1;
            _uIScript.UpdateChangeWaveCount(_waveNumber);
            Debug.Log("WaveNumberStarted");
        }
    }
     IEnumerator EnemyWave()
    {
        Debug.Log("enemy wave started");

        while (_waveNumber < 5 && _startWave == true  )  //or could have used _stopSpawning == false
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 12f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(3F);   //(5f / 1.5f);
                                                   // _spawnTime /= 1.5f;  I can add this in yield time under _spawntime 

           

            if (_waveNumber == 4)
            {
                _startWave = false;

                _stopSpawning = true;

                Debug.Log("WAVE STOPPED");
            }

            if (_waveNumber == 4  && _player != null)  //previously had if stopSpawning is true and player != null instantiate boss 
            {
                _startWave = false; 
                                          // lets see if this will stop boss from spawning 2x..it did so it was spawning boss as soon as stop spawning = true                                                 
                _uIScript._bossLifeBar.enabled = true;

                _uIScript.UpdateBossText();

                Boss();
            }
        }

    }
   IEnumerator SpawnEnemyRoutine2()
    {
        while (_waveNumber != 2 && _startWave == true)  // If loop in the coroutine doesnt meet its condition it will end.  It will go to what is next
        {                                               // or end.. Here it waits in a loop while _waveNumber is not 2. Then when it becomes 2, the first
                                                        // while loop fails and begins to check the second loop which it can do because _waveNumber === 2
            yield return null;
        }

        while (_waveNumber  >1 && _startWave == true )
        {
          
            Vector3 posToSpawn = new Vector3(Random.Range(-15f, 15f), 12f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab2, posToSpawn, Quaternion.identity);

           newEnemy.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));
         
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
          while (_waveNumber != 3 && _startWave == true)
        {
            yield return null;
        }

          while (_waveNumber > 2 && _startWave == true )    
        {
            Debug.Log("SpawnSPACEALIEN!!");

            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 12f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab3, posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(5f, 10f));
        }
    }
    public void OnPlayerDeath()
    {
        _stopSpawning = true;

        _startWave = false;
    }
     void Boss()
    {
        Debug.Log("boss Spawned");

        Vector3 posToSpawn = new Vector3(0, 16f, 0);

        GameObject newEnemy = Instantiate(_bossPrefab, posToSpawn, Quaternion.identity);

    }

}

    
