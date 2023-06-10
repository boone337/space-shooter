using JetBrains.Annotations;
using System.Collections;
using System.Collections.Generic;
//using System.Numerics;  fOR SOME REASON THIS LINE THREW AN ERROR CODE.  never had a problem up until now 4.16.23
using UnityEngine;
using UnityEngine.UIElements;

public class Enemy : MonoBehaviour

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
    private float _speed = 4f;

    private Player _player;

    private laser _laser;

    //private BombScript _bomb;        //////////// ?????????

    //create handle to animator component
    private Animator _anim;

    [SerializeField]
    private bool _shieldActive = true;

    [SerializeField]
    private GameObject _shield;

    [SerializeField]
    private int _shieldhit = 1;

    private bool canFire = true;

    private GameObject laserClone;

    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

       // _laser = GameObject.Find("laser").GetComponent<laser>();  this shit right here caused my enemy not to explode!!!!! 

        GameObject laserClone = GameObject.Find("laserClone");

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
       // EvadePlayer();   I am not using this at the moment 

        RayCast();

        CalculateMovement();

       /* if (Time.time > _canFire)
        {
            _fireRate = Random.Range(3f, 7F);
            _canFire = Time.time + _fireRate;
            GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, -2.9f, 0), transform.rotation);  //transform.rotation lets projectile fire towards position facing
                                                                                                                                   // GameObject enemyLaser = Instantiate(_laserPrefab, transform.position, Quaternion.identity);  //Quaternion.identity fires projectile foward only no matter what direction facing
            laser[] lasers = enemyLaser.GetComponentsInChildren<laser>();

            for (int i = 0; i < lasers.Length; i++)
            {
                lasers[i].AssignEnemyLaser();

            }
        }  */

        Bomb();

    }
    public void RayCast()
    {
        Debug.DrawRay(transform.position, -Vector3.up * 10, Color.white, 0);

        RaycastHit2D hit = Physics2D.Raycast(transform.position, -Vector2.up, 10, LayerMask.GetMask("Player"));

        //RaycastHit2D hit2 = Physics2D.Raycast(transform.position, -Vector2.up, 30, LayerMask.GetMask("LaserLayer")); //add get layermask for raycast to work

        if ( hit.collider != null && hit.collider.gameObject.tag == "Player" )

        {            
           Debug.DrawRay(transform.position, -Vector3.up * 10, Color.red, 0);


            if (Time.time > _canFire)
            {
                _fireRate = Random.Range(1f, 1.5f);
                _canFire = Time.time + _fireRate;

                Fire();
            } 

            Debug.Log("Hit Player");

            StartCoroutine(ResetFireRate());
                                 
        }        
      
    }
  
    private IEnumerator ResetFireRate()
    {
        yield return new WaitForSeconds(0f); 
       // canFire= true;  

    }

    private void EvadePlayer()
    {
        if (_player != null)
        {
            float delta = _player.transform.position.x - transform.position.x;

            float evasionSpeed = Mathf.Pow(5, -(delta * delta)) * 10;


            if (delta >= 0)
            {
                evasionSpeed = -Mathf.Pow(5, -(delta * delta)) * 10;
            }

            transform.Translate(new Vector3(evasionSpeed, -4, 0) * Time.deltaTime);

        }

    }

    public void Fire()
    {         
       GameObject enemyLaser = Instantiate(_laserPrefab, transform.position + new Vector3(0, -2.9f, 0), transform.rotation);
    }


    public void Bomb()      //trying to implement bomb damage through player instead of bombscript

    { 
            if (Input.GetKeyDown(KeyCode.B))
            {
                StartCoroutine(Damage());

            }

            IEnumerator Damage()
            {

                yield return new WaitForSeconds(.5f);

                Destroy(this.gameObject);

                _anim.SetTrigger("On Bomb Death");

                _audioSource.Play();

               

            }

        }

       public void CalculateMovement()
        {
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

            //move down at 4 meters per second

            //if bottom screen respawn with a new random position

            if (transform.position.y < -9f)
            {
                transform.position = new Vector3(Random.Range(-8f, 8f), 7, 0); 

            }

        }

        public void ShieldActive()
        {
            if (_shieldActive == true)
            {
                _shieldhit--;

                if (_shieldhit == 0)
                {
                  Shield();
                  _shieldActive = false;
                }
             
            }

        }
        void Shield()
        {
            _shield.SetActive(false);

        }

    void OnTriggerEnter2D(Collider2D other)

    {
        if (other.tag == "laser" && _shieldActive == true)
        {
            ShieldActive();
            // _shield.SetActive(false) ;
            Destroy(other.gameObject);
            // _shieldActive = false;
            return;

        }


        if (other.tag == "Player" && _shieldActive == true)
        {
            Player player = other.transform.GetComponent<Player>();
            ShieldActive();
            if (player != null)
            {
                player.Damage();

                ShieldActive();
            }

        }

        //  Debug.Log("Hit: " + other.transform.name);

        //if other is player  
        //damage the player 
        //destroy 

        if (other.tag == "Player" && _shieldActive == false)
        {  //damage player  
            Player player = other.transform.GetComponent<Player>();

            if (player != null)
            {
                player.Damage();

            }
            //trigger anim to set explosion
            _anim.SetTrigger("On Enemy Death");
            _speed = 0;
            _audioSource.Play();
            Destroy(this.gameObject, 1f);

        }

        if (other.tag == "laser" && _shieldActive == false)
        {


            Debug.Log("laser hit enemy");

            Destroy(this.gameObject, 1f);

            Destroy(other.gameObject);

           // _anim.SetTrigger("On Enemy Death");

            if (_anim != null)

            { _anim.SetTrigger("On Enemy Death"); }

            else
            {
                Debug.Log("explode anim is null");
            }

            if (_player != null)
            {
                _player.AddScore(10);
            }


            //  _anim.SetTrigger("OnAlienDeath");
            _speed = 0;
            _audioSource.Play();

            Destroy(GetComponent<Collider2D>());   //this will cause double explosions even after death




            // Player player = GameObject.Find("Player").GetComponent<Player>();
            //this was mad global at the top so it is not needed every where

            //add 10 to score
            //create method to add 10 to score

            //communicate with ui to add score
            //if other islaser  //destroy us // laser
        }

        else if (other.tag == "enemy laser")
        {
            Debug.Log("no damage no emotional damage");
        }

    }

   public void EvadeLaser()
    {
        int range = Random.Range(0, 2);

        if (range == 0)
        {
            StartCoroutine(Left());

        }

        else
        {
            StartCoroutine(Right());

        }
    }

    IEnumerator Left()
    {
        Vector2 _currentPos = transform.position;
        Vector2 _destination = new Vector2(transform.position.x - 2, transform.position.y);
        float _t = 0f;

        while (_t < 1)
        {
            _t += Time.deltaTime * 3f;
            transform.position = Vector2.Lerp(_currentPos, _destination, _t);
            yield return null;
        }

    }


    IEnumerator Right()
    {
        Vector2 _currentPos = transform.position;
        Vector2 _destination = new Vector2(transform.position.x + 2, transform.position.y);
        float _t = 0f;

        while (_t < 1)
        {
            _t += Time.deltaTime * 3f;
            transform.position = Vector2.Lerp(_currentPos, _destination, _t);
            yield return null;
        }

    }  
}











