using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BossScript : MonoBehaviour
{
    [SerializeField]
    private float _fireRate = 3.0f;

    [SerializeField]
    private float _canFire = -1;

    [SerializeField]
    private GameObject _laserPrefab;


    [SerializeField]
    private float _speed = 4.0f;

    private Player _player;

    private Animator _anim;

    [SerializeField]
    private float _moveFrequency = 1.5f;

    [SerializeField]
    private float _moveMagnitude = 6f;

    public float radius = .5f;      // Radius of the circle

    public float speed = 2f;       // Speed of rotation

    public Vector3 rotationAxis;   // Axis around which the object rotates

    private Vector3 center;        // Center of the circle

 //   private float angle = 0f;      // Current angle of rotation

    [SerializeField]
    private bool _move = true;

    [SerializeField]
    private GameObject _eyeLaser1;

    [SerializeField]
    private GameObject _eyeLaser2;

    [SerializeField]
    private int _bossLife = 100;

    private SpriteRenderer spriteRenderer;

    private Color originalColor;

    private UIScript _uIScript;

    [SerializeField]
    private AudioClip _audioClip;

    [SerializeField]
    private AudioSource _audioSource;

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

        // Get the reference to the SpriteRenderer component
        spriteRenderer = GetComponent<SpriteRenderer>();

        originalColor = spriteRenderer.color;

        _uIScript = GameObject.Find("Canvas").GetComponent<UIScript>();

    }

    // Update is called once per frame
    void Update()
    {
        CalculateMovement();

        /* if (_move == false)
         {  // Update the angle based on the speed
             angle += speed * Time.deltaTime;

             // Calculate the new position on the circle
             Vector3 offset = new Vector3(Mathf.Cos(angle),0f, Mathf.Sin(angle)) * radius;

             Vector3 newPosition = center + offset;

             // Rotate the object around the rotation axis
             transform.RotateAround(center, rotationAxis, speed * Time.deltaTime);

             // Move the object to the new position
             transform.position = newPosition;

             //center = transform.position;
         } */   

        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(2f, 4F);

            _canFire = Time.time + _fireRate;

            ShootLaser1();
            CreateBullet(-25f);
            CreateBullet(-50f);
            CreateBullet(-75f);
            CreateBullet(-90f);
            CreateBullet(-115f);
            CreateBullet(-140f);
        }


        if (Time.time > _canFire)
        {
            _fireRate = Random.Range(1.9f, 1.9F);

            _canFire = Time.time + _fireRate;

            ShootLaser1();
            
        }

     
    }

 
   private void ShootLaser1()
    {
        
        Instantiate(_eyeLaser1, transform.position + new Vector3(-2.07f, 3.8f, 0), transform.rotation);

        Instantiate(_eyeLaser2, transform.position + new Vector3(0.63f, 3.8f, 0), transform.rotation);

    }

    private void CreateBullet(float angleOffset = 0f)
    {
        GameObject bullet = Instantiate<GameObject>(_laserPrefab,transform.position+ new Vector3(0,-9,0),transform.rotation);

        _audioSource.Play();

        bullet.transform.position = transform.position;

        Rigidbody2D rigidbody = bullet.GetComponent<Rigidbody2D>();

        rigidbody.AddForce(Quaternion.AngleAxis(angleOffset, Vector3.forward) * transform.right * 1000.0f);

    }


    void CalculateMovement()
    {
        if (transform.position.y >= 4.5f && _move == true )

        {
            
            transform.Translate(Vector3.down * _speed * Time.deltaTime);

        }

        //transform.position = new Vector3(transform.position.x, 4f, 0);
   
        if (transform.position.y <= 4.5  )
        {
            _move = false;

             float x = Mathf.Cos(Time.time * _moveFrequency) * _moveMagnitude;

             float y = transform.position.y; //* (_speed * Time.deltaTime);

             transform.position = new Vector3(x, y);

        }

    }

    public void Damage()
    {
        _bossLife--;

        _uIScript.UpdateBossDamage(_bossLife);

        if ( _bossLife == 0)
        {
            Destroy(this.gameObject, 1f);

            Destroy(GetComponent<Collider2D>());  //      //this will cause double explosions even after death
                                                  // Destroy(this.gameObject, 1f);

            _anim.SetTrigger("death");
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
            _anim.SetTrigger("OnAlienDeath");
          
            _audioSource.Play();
           
            Damage();
        }

        if (other.tag == "laser")
        {
            Destroy(other.gameObject);
            if (_player != null)
            {
                _player.AddScore(10);
            }
            Damage();
       
            _audioSource.Play();
           
            StartCoroutine(ChangeColorForOneSecond());
       
        }
 
        else if (other.tag == "enemy laser")
        {
            Debug.Log("no damage no damage");
        }

        IEnumerator ChangeColorForOneSecond()
        {

            Color initialColor = spriteRenderer.color;

            // Modify the color
            Color newColor = new Color(1f, 0f, 0f, 1f); // Red color with full opacity

            spriteRenderer.color = newColor;

            // Wait for one second
            yield return new WaitForSeconds(.3f);  //Well well life is full of surprises! You dont get 1 sec!!  

            // Revert the color back to the original color
            spriteRenderer.color = originalColor;

        }
    }
}