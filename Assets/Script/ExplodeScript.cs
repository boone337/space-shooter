using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ExplodeScript : MonoBehaviour
{

  //  [SerializeField]
    //private Asteroid _asteroid;

    // Start is called before the first frame update
    void Start()
    {
        //_asteroid = GameObject.Find("Asteroid").GetComponent < Asteroid>();
        Destroy(this.gameObject, 8f);
    }

    // Update is called once per frame
    void Update()
    {
       // if (_asteroid == null)
        //{ Destroy(this.gameObject,3f);
        
       //}   

       //Destroy(this.gameObject, 3f);

        //can I do a null check if asteroid is null destroy this gameobject.

    }
}
