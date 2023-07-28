using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
//using TMPro.EditorUtilities;
//using UnityEditorInternal;
using UnityEngine;
using UnityEditor;
using UnityEngine.UI;



public class Player : MonoBehaviour
{

    //public or private reference
    //data types (int, float, bool, string)
    //every variable has a name
    //optional value assigned
    //[SerializeField]
    // private bool _thruster = false;



    [SerializeField]
    private float _horizontalInput;

    [SerializeField]
    private float _fireRate = 0.2f;

    [SerializeField]
    private float _canFire = -1f;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private bool _laser = true;

    [SerializeField]
    private int _lives = 3;

    [SerializeField]
    private int _oneUP = 1;

    private SpawnManager _spawnManager;

    [SerializeField]
    private bool _isTripleShotActive = false;

    [SerializeField]
    private GameObject _TripleShotPrefab;


    //[SerializeField]
    //private bool _speedBoostActive = false;

    [SerializeField]
    private float _speedMultiplier = 2.0f;

    [SerializeField]
    private bool _isShieldActive = false;

    [SerializeField]
    private int _shieldPower = 3;

    [SerializeField]
    private GameObject _shield;

    //variable reference to shield visualizer
    [SerializeField]
    private SpriteRenderer _shieldVisualizer;

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
    private AudioSource _audioSource; //if made serialized field:  you will need to reassign the variable everytime the game crashes
                                      //question is: is it only for audio source you do this with?

    [SerializeField]
    private int _ammoSupply = 15;                              

    [SerializeField]
    private int _ammo = 15;

    [SerializeField]
    private GameObject _nuke_prefab;

    [SerializeField]
    private int _bomb = 3;

    // [SerializeField]
    //  private Image _thrusterbar;

    [SerializeField]
    private float _thrusterSpeed = 5.0f;

    [SerializeField]
    private float _thrusterLevel = 100f;

    [SerializeField]
    private float _speed = 5.0f;

    private CameraShake _cameraShake;

    private Animator _anim;

    [SerializeField]
    private GameObject _homingMissile;

    [SerializeField]
    private bool _homingActive = false;

    private Enemy _enemy;

    // Start is called before the first frame update

    void Start()
    {
        

        _cameraShake = GameObject.Find("Camera_Shake").GetComponent<CameraShake>();

        if (_cameraShake == null)
        {
            

            Debug.LogError("_cameraShake is Null");
        }

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

            _audioSource.clip = _laserSoundClip;
        }

     
        //take the current position = new position (0,0,0)
        transform.position = new Vector3(0, -5.8f, 0);

        //question is: is it only for audio source you do this with?

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is Null");

        }

    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Space) && Time.time > _canFire && _laser == true)
        {
            Shoot();
        }

       

        if (Input.GetKeyDown(KeyCode.B) && Time.time > _canFire)
        {
           
              Bomb();
           
        }
      
            
            CalculateMovement();

        // _uIScript.UpdateAmmoDisplay(_ammo); causes ui message to blink and flicker rapidly so update it in the ammo void 

        //  if (_ammo > 0)
        //  {
        // _laser = true;   // Why does this work here and not in the shoot method below??

        //  }

       
    }

    void CalculateMovement()
    {
        float horizontalInput = Input.GetAxis("Horizontal");

        float verticalInput = Input.GetAxis("Vertical");

        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {

            _anim.SetTrigger("TurnLeft");

        }

        if (Input.GetKeyDown(KeyCode.RightArrow))
        {

            _anim.SetTrigger("TurnRight");

        }


        //Time.deltaTime is equivalent to real time
        // transform.translate(new Vector3(horizontal, vertical, 0) * speed * Time.deltaTime);
        //more optimal way to code.  now you can have one line of code instead of two

        Vector3 direction = new Vector3(horizontalInput, verticalInput, 0); // this code cleans up the above code even more

        transform.Translate(direction * _speed * Time.deltaTime);

        if (Input.GetKey(KeyCode.LeftShift) && _thrusterLevel > 0)
        {

            Debug.Log("Thrusters Active");

            _thrusterLevel -= 1.0f;   //Did this first then went update UI
            transform.Translate(direction * _speed * _thrusterSpeed * Time.deltaTime);
            _uIScript.UpdateThrusterLevel(_thrusterLevel);

        }
        else if (_thrusterLevel < 0)
        {
            _thrusterLevel = 0;  //2nd 
        }
        else if (_thrusterLevel < 100)
        {
            _thrusterLevel += (2 * Time.deltaTime);   //3rd 
            transform.Translate(direction * _speed * Time.deltaTime);
            _uIScript.UpdateThrusterLevel(_thrusterLevel);
        }
        if (_thrusterLevel == 100)
        {

            _thrusterLevel = 100;   //4th
        }
        else
        {
            new Vector3(horizontalInput, verticalInput, 0);
        }

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
        _canFire = Time.time + _fireRate;

        if (_isTripleShotActive == true)
        { //instantiate triple shot 
            Instantiate(_TripleShotPrefab, transform.position, Quaternion.identity);
            _audioSource.Play();
        }
        else
        {
            Instantiate(_laserPrefab, transform.position + new Vector3(0.2f, 2.49f, 0), Quaternion.identity);
           
            Ammo();

            if (_audioSource != null)
            {
                _audioSource.Play();

                Debug.Log("audio source is not null");
            }
        }
      //  _audioSource.Play();    //This will automatically play the sound clip
        //play laser audio clip when shots fired

        if (_ammo == 0)
        {
            _laser = false;
            _isTripleShotActive = false;
        }
    
       if (_homingActive == true)
        {
            Instantiate(_homingMissile, transform.position + new Vector3(0f, 2.0f, 0), Quaternion.identity);

        }

    }

    public void AmmoSupply()
    {
        _ammo = _ammo + _ammoSupply;

        if (_ammo > 0)

        {
            _ammo = 15;
            _uIScript.UpdateAmmoDisplay(_ammo);
            Debug.Log("Max Ammo ! ");
            _laser = true;
        }
  
    }

    public void HomingWeapon()
    {
        _homingActive = true;
        _homingMissile.SetActive(true);
    }

    public void Ammo()
    {
        _ammo--;
        _uIScript.UpdateAmmoDisplay(_ammo);

    }

    public void ShieldActive()
    {

        _isShieldActive = true;
        _shieldPower = 3;
        _shield.SetActive(true);

        //enable the shield visualizer
        _shieldVisualizer.color = new Color(0.9905f, 0.9885f, 0.9625f, 1f);

    }  //0f, 0.9323f, 0.9622f, 1f  //0.9528f,0.9490f,0.8988f,0f

    public void Bomb()
    {
        if (_bomb > 0)
        {
            _bomb--;

           Instantiate(_nuke_prefab, transform.position + new Vector3(0, 1.83f, 0), Quaternion.identity);

           

        }

        else if (_bomb == 0)
        {
            _bomb = 0;

        }

        if (_bomb > 3)

        {
            _bomb = 3;
        }

        _uIScript.UpdateBomb(_bomb);

        
    }

   
    public void Damage()
    {

        // if shields are active do nothing
        //deactivate shield
        //return;
        //enable the shield visualizer
        // _shieldVisualizer.color = new Color(0f, 0.9323f, 0.9622f, 1f);
        if (_isShieldActive == true)
        {
            _shieldPower--;

            if (_shieldPower == 2)
            {
                _shieldVisualizer.color = new Color(0.8670f, 0.9607f, 0f, 1f);
            }
            if (_shieldPower == 1)
            {

                _shieldVisualizer.color = new Color(0.9607f, 0.0f, 0.0544f, 1f);
            }
            if (_shieldPower == 0)
            {
                _isShieldActive = false;              //disable shield visualizer
                _shield.SetActive(false);
                return;
            }
            else if (_isShieldActive == false)
            {
                _lives--;
                //or lives--;     //_lives = _lives -1;     //check if dead//destroy us
                _uIScript.UpdateLives(_lives);
            }
        }
        if (_isShieldActive == false)

        {
            Lives();
          
         

        }

        if (_lives < 1)

        {
            _spawnManager.OnPlayerDeath();
            Destroy(this.gameObject,.5f);
            if (_anim != null)

            {
                _anim.SetTrigger("OnPlayerDeath"); 
            
            }
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

    public void Lives()
    {
        _lives--;
        _uIScript.UpdateLives(_lives);
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
        // _speedBoostActive = true;  Not doing anything here not declared anywhere but here
        _speed *= _speedMultiplier;

        StartCoroutine(SpeedBoostPowerDownRoutine());

    }

    IEnumerator SpeedBoostPowerDownRoutine()
    {
        yield return new WaitForSeconds(5.0f);
        //_speedBoostActive = false;
        _speed /= _speedMultiplier;
    }
    public void AddScore(int points)
    {
        _score += points;
        _uIScript.UpdateScore(_score);

    }
    //add 10 to score
    //communicate with ui and update score

    // ammo count 100
    //communicate with ui and update ammo
    public void OneUp()
    {
        _lives = _lives + _oneUP;

        if (_lives > 3)
        {
            _lives = 3;

        }
        _uIScript.UpdateLives(_lives);

    }

    public void RedBomb()
    {

        Debug.Log("RedBomb");
        _lives--;
        _uIScript.UpdateLives(_lives);

        // need it to blow up when contact is made
        //need it to update UI but its not doing it.   You had it on update ammo instead of update lives
       
    }
 
    private void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Player is hit by enemy laser");

        if (other.tag == "enemy laser")
        {
            if (_cameraShake == null)
            {
                Debug.LogError("Camera Shake is null");
            }
            else if (_cameraShake != null)
            {
                _cameraShake.ShakeCamera();
            }        

            Damage();

            

            _homingMissile.SetActive(false);
            


        }

        if(other.tag == "EnemyShield")
        {
            Damage();

            Debug.Log("Player hit Enemy Shield");
        }

        if(other.tag == "enemy")
        {
            _homingMissile.SetActive(false);
        }


    }
}




