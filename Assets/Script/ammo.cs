using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ammo : MonoBehaviour
{
    [SerializeField]
    private float x;

    [SerializeField] 
    private float y;

    [SerializeField]
    private float z;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        float x = transform.position.x;
        float y = transform.position.y; 
        float z = transform.position.z;

        transform.position= new Vector3(x, y, z);
    }
}
