using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Asteroid : MonoBehaviour

{

    // [SerializeField]
    //private GameObject _Asteroid;

    [SerializeField]
    private GameObject _explosion_prefab;

    [SerializeField]
    private GameObject _model;

    [SerializeField]
    private float _speed = 9.0f;

    [SerializeField]
    private float _rotateSpeed = 19.0f;

    private SpawnManager _spawnManager;

    private Player _player;

    private Animator _anim;

    [SerializeField]
    private bool _isStarterAsteroid = false;

    [SerializeField]
    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();
        _spawnManager = GameObject.Find("SpawnManager").GetComponent<SpawnManager>();

        _player = GameObject.Find("Player").GetComponent<Player>();

        _audioSource = GetComponent<AudioSource>(); 
    }

    // Update is called once per frame
    void Update()

    {
        if (_isStarterAsteroid == false)
        {
            transform.Translate(transform.up * -1 * _speed * Time.deltaTime);

        }
        // transform.Rotate(Vector3.down  * _rotateSpeed * Time.deltaTime);
        //transform.Rotate(Vector3.forward * _rotateSpeed  * Time.deltaTime);
        _model.transform.Rotate(Vector3.forward, _rotateSpeed * Time.deltaTime, Space.Self);

        if (transform.position.y < -9.0f)
        {

            Destroy(gameObject);
        }

        if (transform.parent != null)
        {

            Destroy(transform.parent.gameObject);
        }


        if (Input.GetKeyDown(KeyCode.B))
        {
            boom();
            Debug.Log("Destroyed by Nuke!");
        }

        //transform.position = new Vector3(Random.Range(-8f, 8f),7, 0);

            // Destroy (this.gameObject);


            //transform.Translate(Vector3.down * _speed * Time.deltaTime);
            //
            //transform.Rotate(Vector3.down * _speed * Time.deltaTime);

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "laser")
        {
            Debug.Log("Laser hit Asteroid");
            
            Destroy(other.gameObject);
            if (_isStarterAsteroid == true)

            {
                _spawnManager.StartSpawning();

            }
            Instantiate(_explosion_prefab, transform.position, Quaternion.identity);
            // Destroy(GetComponent<Collider2D>());
            _anim.SetTrigger("On Enemy Death");
            _speed = 0;
            Destroy(this.gameObject, 1f);
          
        }

        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            Debug.Log("player hit asteroid");
            
            //Destroy(_player);
            if (_player != null)
            {
                _player.Damage();

            }
            Instantiate(_explosion_prefab, transform.position, Quaternion.identity);
            _anim.SetTrigger("On Enemy Death");
            _speed = 0;
           
            Destroy(this.gameObject, 1f);
            _speed = 0;
 
        }

        //trying to implement bomb damage through player instead of bombscript

    }

    void boom()
    {
        //if (Input.GetKeyDown(KeyCode.B))


        Instantiate(_explosion_prefab, transform.position, Quaternion.identity);
        _anim.SetTrigger("On Enemy Death");
        _speed = 0;

        Destroy(this.gameObject, 1f);
        _speed = 0;

    }






    //check for laser collision (Trigger)
    //instantiate explosion at the position of the asteroid (us)
    //destroy the explosion   













}



