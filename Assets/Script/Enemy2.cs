using System.Collections;
using System.Collections.Generic;
using UnityEditor;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;


public class Enemy2 : MonoBehaviour
{

    [SerializeField]
    private float _fireRate = 3.0f;

    [SerializeField]
    private float _canFire = -1;

    [SerializeField]
    private GameObject _laserPrefab;

    [SerializeField]
    private AudioSource _audioSource;

    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    //private BombScript _bomb;        //////////// ?????????

    //create handle to animator component
    private Animator _anim;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        _audioSource = GetComponent<AudioSource>();

        //null check player
        //assign component to anim
        if (_player == null)
        {
            Debug.LogError("The Player is Null.");
        }
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is Null");
        }      
    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7F);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0f, 0f, 0), transform.rotation);  //transform.rotation lets projectile fire towards position facing
                                                                                                                                 // GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);  //Quaternion.identity fires projectile foward only no matter what direction facing
            laser[] lasers = enemyLaser.GetComponentsInChildren<laser>();

            // for (int i = 0; i < lasers.Length; i++)
            // {
            //      lasers[i].AssignEnemyLaser();

            //  }
        }
    }


    void CalculateMovement()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);

        //move down at 4 meters per second

        //if bottom screen respawn with a new random position

        if (transform.position.y < -9f)
        {
            transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0);

        }
    }

    private void OnTriggerEnter2D(Collider2D other)

    {
        Debug.Log("Hit: " + other.transform.name);

        //if other is player  
        //damage the player 
        //destroy us

        if (other.tag == "Player")
        {  //damage player  
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();
                
            }
            //trigger anim to set explosion
            _anim.SetTrigger("On Asteroid Death");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 1f);
        }

        if (other.tag == "laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);

                _player.EnemyDestroyCount(1);
            }
            //trigger anim 
            if (_anim != null)
            {
                _anim.SetTrigger("On Asteroid Death");
            }

            Destroy(this.gameObject, 1f);
           
            Debug.Log(" laser hit enemy variant");
           
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());   //this will cause double explosions even after death          
        }

        else if (other.tag == "Bomb")
        {
            Destroy(this.gameObject, 1f);

            StartCoroutine(DestroySelfCoroutine());

            Debug.Log("TEST DAMAGE NUKE");    //So we are getting things to blow up now.  next is to do a coroutine to delay the effects.  6.26.23 fuck yeah!! 
        }
    }

    private void DestroySelf()
    {
        StartCoroutine(DestroySelfCoroutine());
    }

    IEnumerator DestroySelfCoroutine()
    {
        yield return new WaitForSeconds(1.5f);


        Destroy(this.gameObject, 1f);

        Destroy(GetComponent<Collider2D>());

        if (_anim != null)

        { _anim.SetTrigger("On Asteroid Death"); }

        else if (_anim == null)
        {
            Debug.Log("Anim is null");
        }

        if (_player != null)
        {
            _player.AddScore(10);
        }

        _audioSource.Play();

        Debug.Log("Enemy destroyed by bomb");
    }
}
