using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;
using UnityEditor;
//using UnityEditorInternal;


public class Asteroid : MonoBehaviour

{

    // [SerializeField]
    //private GameObject _Asteroid;


    [SerializeField]
    private float _speed = 9.0f;

    [SerializeField]
    private float _rotateSpeed = 190.0f;

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
        if (_isStarterAsteroid == true)

        {
            transform.Rotate(Vector3.forward * _rotateSpeed * Time.deltaTime);

        }

        if (_isStarterAsteroid == false)
        {
            //  transform.Translate(transform.up * -1 * _speed * Time.deltaTime);
            //  transform.Rotate(Vector3.forward * _speed * Time.deltaTime);   
            //  transform.Rotate(Vector3.up * _rotateSpeed * Time.deltaTime);

            transform.Translate(Vector3.down * _speed * Time.deltaTime);
        }


        if (transform.position.y < -9.0f)
        {

            Destroy(gameObject);
        }

        if (transform.parent != null)
        {

            Destroy(transform.parent.gameObject);
        }

    }

    private void OnTriggerEnter2D(Collider2D other)
    {

        if (other.tag == "Bomb")
        {
            Debug.Log("Bomb hit Asteroid");

            Destroy(this.gameObject, 1f);
            if (_isStarterAsteroid == true)

            {
                _spawnManager.StartSpawning();

            }
            

            _speed = 0;
            Destroy(this.gameObject);
            _audioSource.Play();
        }


        if (other.tag == "laser")
        {

            if (_isStarterAsteroid == true)

            {
                _spawnManager.StartSpawning();

                Debug.Log("Laser hit Asteroid");

                _speed = 0;

                

                if (_anim != null)

               _anim.SetTrigger("On Asteroid Death");

                Destroy(this.gameObject, 1f);

                Destroy(other.gameObject);

                _audioSource.Play();
            }

            Debug.Log("Laser hit Asteroid");
                      
            _speed = 0;

            Destroy(this.gameObject, 1f);

            if (_anim != null)

           _anim.SetTrigger("On Asteroid Death");

           _audioSource.Play();

        }


        if (other.tag == "Player")
        {
            Player player = other.transform.GetComponent<Player>();
            Debug.Log("player hit asteroid");

            if (_isStarterAsteroid == true)

            {
                _spawnManager.StartSpawning();

            }

            //Destroy(_player);
            if (_player != null)
            {
                _player.Damage();

            }
           // Instantiate(_explosion_prefab, transform.position, Quaternion.identity);
            _anim.SetTrigger("On Asteroid Death");
            _speed = 0;
           
            Destroy(this.gameObject, 1f);
            _speed = 0;
            _audioSource.Play();
        }

        //trying to implement bomb damage through player instead of bombscript

    }

  












}



