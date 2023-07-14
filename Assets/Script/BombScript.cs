using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditorInternal;
using UnityEditor;
using UnityEngine.UI;
using TMPro.EditorUtilities;
using UnityEngine.UIElements;
using System.Runtime.CompilerServices;
using System.Threading;


public class BombScript : MonoBehaviour

{

    //bomb needs to travel up and stop
    //speed of bomb travel
    //size of explosion 
    private CameraShake _cameraShake;

    // private bool _bombActive = false;

    [SerializeField]
    private float _bombSpeed = 3.0f;

    private Animator _anim;

    private AudioSource _audioSource;

    [SerializeField]

    public float explosionRadius = 5f;


    // Start is called before the first frame update
    void Start()
    {
        _cameraShake = GameObject.Find("Camera_Shake").GetComponent<CameraShake>();

        if (_cameraShake == null)
        {
            Debug.LogError("_cameraShake is Null");
        }

        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is Null");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError(" Bomb AudioSource is null");
        }

        
    }

    // Update is called once per frame
    void  Update()
    {


        MoveUp();

    }

    void MoveUp()
    {
        transform.Translate(Vector3.up * _bombSpeed * Time.deltaTime);

        if (transform.position.y > 6.5f)
        {
            if (transform.parent != null)
            {

                Destroy(transform.parent.gameObject);
            }


            Destroy(this.gameObject);

        }

    }
     public void startCoroutine()
     {


         StartCoroutine(Bomb());     

     } 

    IEnumerator Bomb()

    {
        yield return new WaitForSeconds(1.0f);

        _cameraShake.ShakeCamera();

        _anim.SetTrigger("On Bomb Death");     

        _bombSpeed = 0f;

        Destroy(this.gameObject, 1f);

        _audioSource.Play();

        //  Destroy(GetComponent<BoxCollider2D>());

        Debug.Log("bomb audio played");

        Debug.Log("Bomb blew up");

      //  Destroy(transform.parent.gameObject);
        

    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.CompareTag("enemy"))
        {
            Destroy(other.gameObject);
        }
    }

}



