using System.Collections;
using System.Collections.Generic;
using TMPro.EditorUtilities;
using UnityEditor.Experimental.GraphView;
using UnityEditorInternal;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    private PowerUp _powerUp;



    private Player _player;

    [SerializeField]
    private GameObject _enemyPrefab;

    [SerializeField] private GameObject _enemyPrefab2;

    [SerializeField] private GameObject _enemyPrefab3;

    [SerializeField] private GameObject _enemyPrefab4;

    [SerializeField] private GameObject _bossPrefab;

    //  [SerializeField] private float _spawnTime = 6f;

    [SerializeField] private int _waveNumber = 0;

    private bool _startWave = true;


    [SerializeField] private GameObject _enemyContainer;

    private bool _stopSpawning = false;

    [SerializeField]
    private GameObject[] _powerups;

    [SerializeField]
    private GameObject _asteroidPrefab;

    private UIScript _uIScript;

    [SerializeField]
    private GameObject[] _powerup;



    //most frequent down to rarity
    public int total ;   //can be total weight or whatever you choose to call it

    public int RandomNumber;  

    public int[] table = //{ 60, 30, 10 }; or can do as shown below
    {
    60,
    30,
    10,
    };

    // Start is called before the first frame update
    void Start()
    {
        _uIScript = GameObject.Find("Canvas").GetComponent<UIScript>();

        // _powerUp = GameObject.Find("PowerUp").GetComponent<PowerUp>();

        _player = GameObject.Find("Player").GetComponent<Player>();

        Vector3 posToSpawn = new Vector3(0, 16f, 0);

       

        // GameObject newEnemy2 = Instantiate(_bossPrefab, posToSpawn, Quaternion.identity);
    }

    public void StartSpawning()

    {
        StartCoroutine(SpawnEnemyRoutine());

        StartCoroutine(SpawnPowerUpRoutine());

        StartCoroutine(SpawnAsteroidRoutine());

        StartCoroutine(EnemyWave());

        _uIScript.UpdateChangeWaveCount(_waveNumber);
       
    }

    // Update is called once per frame
    void Update()
    {
        WeightPowerUp();

    }
    //Spawn game objects every 5 seconds
    //Create a coroutine of type IEnumerator -- Yield Events
    // while loops
    public IEnumerator EnemyWave()
    {
        while (_startWave == true)  //or could have used _stopSpawning == false
        {

            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 12f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab, posToSpawn, Quaternion.identity);  //for demonstration purposes and project tasks I have an enemy spawnroutine and also instantiating enemies in wave routine.  

            GameObject newEnemy1 = Instantiate(_enemyPrefab2, posToSpawn, Quaternion.identity);  //ultimately I can put all the spawning here in the wave routine. 

            newEnemy1.transform.parent = _enemyContainer.transform;

            yield return new WaitForSeconds(5F);   //(5f / 1.5f);
            // _spawnTime /= 1.5f;  I can add this in yield time under _spawntime 

            _waveNumber += 1;

            _uIScript.UpdateChangeWaveCount(_waveNumber);

            

            if (_waveNumber == 5 ) 
            {    
                _startWave = false;
                _stopSpawning = true;
            }

            if (_stopSpawning == true && _player != null)
            {
                _startWave = false;

                Boss();

                _uIScript._bossLifeBar.enabled = true;

                _uIScript.UpdateBossText();
            }
        }

    }
  
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
        yield return new WaitForSeconds(10.0f);

        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-10f, 10f), 12f, 0);

            GameObject newEnemy = Instantiate(_enemyPrefab3, posToSpawn, Quaternion.identity);

           GameObject newEnemy1 = Instantiate(_enemyPrefab4, posToSpawn, Quaternion.identity);

            newEnemy.transform.parent = _enemyContainer.transform;
            yield return new WaitForSeconds(10.0f);

            //never get here because in loop
            //then his line is called     
            //while loop (infinite loop)
            //Instantiate enemy prefab
            //yield wait for 5 sec. 

        }
    }

    IEnumerator SpawnPowerUpRoutine()
    {
        
        yield return new WaitForSeconds(10f);
        while (_stopSpawning == false)
        {
            Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);

            int randomPowerUp = Random.Range(0, 3);

            Instantiate(_powerups[randomPowerUp], posToSpawn, Quaternion.identity);

            yield return new WaitForSeconds(Random.Range(3.0f, 8.0f));

        }
    }

    void WeightPowerUp()
    {  

        Vector3 posToSpawn = new Vector3(Random.Range(-8f, 8f), 7, 0);
       //foreach (var item in table)
        {
            //total += item / 1 ; 
            
        }
       
       RandomNumber = Random.Range(0, 150000); //since I am not using the coroutine I used a higher number so spawning a powerup is not too aggressive 

            foreach (var weight  in table)
            
                for (int i = 0; i < table.Length; i++)
       {    
            if (RandomNumber <=  weight )
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
      public void OnPlayerDeath()
     {
      _stopSpawning = true;

     }

    public void Boss()
    {
     
           Vector3 posToSpawn = new Vector3(0, 16f, 0);

            GameObject newEnemy = Instantiate(_bossPrefab, posToSpawn, Quaternion.identity);
        
     }
       
}

    
