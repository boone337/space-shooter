using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{

    //public or private reference
    //data types (int, float, bool, string)
    //every variable has a name
    //optional value assigned
    [SerializeField]
    private bool _thruster = false;

    [SerializeField]
    private float _thrusterSpeed = 5.0f;

    [SerializeField]
    private float _speed = 5.0f;
   
    [SerializeField]
    private float _horizontalInput;
   
    [SerializeField]
    private float _fireRate = 0.2f;
   
   [SerializeField]
    private float _canFire = -1f;
   
    [SerializeField]
    private GameObject _laserPrefab;
   
    [SerializeField]
    private int _lives = 3;
    private SpawnManager _spawnManager;
   
    [SerializeField]
    private bool _isTripleShotActive = false;
    
    [SerializeField]
    private GameObject _TripleShotPrefab;
   
    
    [SerializeField]
    private bool _speedBoostActive = false;

    [SerializeField]
    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private bool _isShieldActive = false;

    //variable reference to shield visualizer
    [SerializeField]
    private GameObject _shieldVisualizer;
    
    [SerializeField]
    private int _score;

    private UIScript _uIScript;

    [SerializeField]
    private GameObject _left_Engine;
   
    [SerializeField]
    private GameObject _right_Engine;

    //need variable to store audio clip

    [SerializeField]
    private AudioClip _laserSoundClip;

    [SerializeField]
    private AudioClip _explosionSoundClip;

    [SerializeField]
    private AudioSource _audioSource;  //if made serialized field:  you will need to reassign the variable everytime the game crashes
                                       //question is: is it only for audio source you do this with?  

    // Start is called before the first frame update

    void Start()
    {
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();
        //find the object. Get the component.    
        _uIScript = GameObject.Find("Canvas").GetComponent<UIScript>();
       // Find the Canvas object first then get component <UIScript is what I am looking for. 
        //Now UIScript can be accessed for update score.
 
            if (_spawnManager == null)
        {
            Debug.LogError("The SpawnManager is Null");

        }

            if (_uIScript == null)
        {
            Debug.LogError("The UI Script is null");

        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("Audio source on player is null");
        }

        else 
        { 
                _audioSource.clip = _laserSoundClip;
        }
        //take the current position = new position (0,0,0)
        transform.position = new Vector3(0, -5.8f, 0);
    }
 

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire )
            Shoot();

        CalculateMovement();
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float verticalInput = Input.GetAxis("Vertical");

        //Time.deltaTime is equivalent to real time
        // transform.translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime);
        //more optimal way to code.  now you can have one line of code instead of two

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0); // this code cleans up the above code even more

        transform.Translate(direction * _speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift))
        {
            Debug.Log("Thrusters Active");
            _thruster = true;
            transform.Translate(direction * _speed * _thrusterSpeed * Time.deltaTime);
        }
        else _thruster = false;
            

            // if player position on y is greater than 0  // y position = 0
            // else if position on the y is less than -3.8f 
            //y pos = -3.8f

            if (transform.position.y >= 0)
            {
                transform.position = new Vector3(transform.position.x, 0, 0);
            }

            else if (transform.position.y <= -8.0f)
            {
                transform.position = new Vector3(transform.position.x, -8.0f, 0);

            }

            //if player on the x >11  then x pos = -11
            //else if player on x = -11 then pos = 11

            if (transform.position.x >= 11)
            {
                transform.position = new Vector3(-11, transform.position.y, 0);

            }

            else if (transform.position.x <= -11)
            {
                transform.position = new Vector3(11, transform.position.y, 0);
            }
        }

    void Shoot()
        
      {
        // if space key is pressed
        //if triple shot is true
        //fire 3 lasers(triple shot prefab)
        //else fire 1 laser
        //instantiate 3 laser (triple shot prefab)
        _canFire = Time.time + _fireRate;
       

        if (_isTripleShotActive == true)
        { //instantiate triple shot 
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);

        }
        else
        {
           
            Instantiate(_laserPrefab, transform.position + new Vector3(0, 1.83f, 0), Quaternion.identity);
        }

        _audioSource.Play();    //This will automatically play the sound clip
        //play laser audio clip when shots fired

    }

      public void Damage()
    {
 
        // if shields are active do nothing
        //deactivate shield
        //return;
        
        if(_isShieldActive == true)
        {             
            _isShieldActive = false;
            //disable shield visualizer
            _shieldVisualizer.SetActive(false);
            return;
        }
        
        _lives -- ; //or lives--;     //_lives = _lives -1;     //check if dead//destroy us

        _uIScript.UpdateLives(_lives);
        

        if (_lives < 1)

        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject);
        }

        if (_lives == 2)
        {                
            _right_Engine.SetActive(true);
            Debug.Log("right engine damage");
        }

        else if (_lives == 1)
        {
            _left_Engine.SetActive(true);
            Debug.Log("left engine damage");
        }

    }

    public void TripleShotActive()
    {
        //tripleShotActive becomes true (first)
        //start the power down coroutine for triple shot  
        _isTripleShotActive = true;       //(first)      
        StartCoroutine(TripleShotPowerDownRoutine());   //(third)     
    }

    IEnumerator TripleShotPowerDownRoutine()   //(second)
    {
        yield return new WaitForSeconds(5.0f);    //after 5 sec becomes false
        _isTripleShotActive = false; 

    }
    //IEnumerator Triple Shot Powr down Routine
    //wait 5 seconds
    //set the tirple shot to false



    public void SpeedBoostActive()
    {
     _speedBoostActive = true;
        _speed *= _speedMultiplier;
        StartCoroutine(SpeedBoostPowerDownRoutine());
    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        _speedBoostActive = false; 
        _speed/=_speedMultiplier;
    }



    public void ShieldActive()
    {
        _isShieldActive = true;
        //enable the shield visualizer
        _shieldVisualizer.SetActive(true);

    }


    public void AddScore(int points)
    {
        _score += points;  
        _uIScript.UpdateScore(_score);

    }
    //add 10 to score
    //communicate with ui and update score

}




