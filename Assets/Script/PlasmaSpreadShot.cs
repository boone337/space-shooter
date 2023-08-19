using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class PlasmaSpreadShot : MonoBehaviour
{
    [SerializeField]
    public GameObject shotPrefab; // The prefab for the shot object

    [SerializeField]
    public  float _speed = 3f;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        MoveDown();
       

    }
    void MoveDown()
    {
        transform.Translate(Vector3.down * _speed * Time.deltaTime);
        // float angleBetweenShots = spreadAngle / (numShots - 1);
        //uaternion shotRotation = Quaternion.Euler(0f, -spreadAngle / 2f * angleBetweenShots, 0f);

        if (transform.position.y < -14.0f)
        {
            if (transform.parent != null)
            {
                Destroy(transform.parent.gameObject);

            }
                Destroy(gameObject);
        }
    }
}
