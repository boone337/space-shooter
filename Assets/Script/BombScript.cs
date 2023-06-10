using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombScript : MonoBehaviour

{

    //bomb needs to travel up and stop
    //speed of bomb travel
    //size of explosion 

    private bool _bombActive = false;

    [SerializeField]
    private float _bombSpeed = 5.0f;

    private Animator    _anim;

    private AudioSource _audioSource;

    // Start is called before the first frame update
    void Start()
    {
        _anim = GetComponent<Animator>();

        if (_anim == null)
        {
            Debug.LogError("Animator is Null");
        }

        _audioSource = GetComponent<AudioSource>();

        if (_audioSource == null)
        {
            Debug.LogError("AudioSource is null");
        }

    }

    // Update is called once per frame
    void Update()
    {
        BombTravel();

    }

    void BombTravel ()
    {
  
        transform.Translate(Vector3.up * _bombSpeed * Time.deltaTime);

        if (_bombActive == false )
        {
            StartCoroutine(Bomb());
            
           
        }

    }

    IEnumerator Bomb()

    {   _bombActive=true;
        _anim.SetTrigger("On Bomb Death ");
        _audioSource.Play();     
        yield return new WaitForSeconds(6f);
        Destroy(this.gameObject);
    }

}
