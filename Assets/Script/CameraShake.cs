using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;
using UnityEditor;
//using UnityEditorInternal;
using UnityEngine;
using UnityEngine.UI;
//using TMPro.EditorUtilities;
//using UnityEngine.UIElements;
//using System.Runtime.CompilerServices;
using System.Threading;



public class CameraShake : MonoBehaviour
{
    //Camera
    public Transform cameraTransform;

    private Vector3 originalCameraPos;

    //Shake
    [SerializeField]
    public float shakeDuration = 2.0f;
    [SerializeField]
    private float _shakeAmount = 2f;

    //private float _nukeShake = 3.0f;

    private bool _canShake = false;

    private float _shakeTimer;

   // private Player _player;



    // Start is called before the first frame update
    void Start()
    {
       // _player = GameObject.Find("Player").GetComponent<Player>();

        originalCameraPos = cameraTransform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {
       

        
    }
   
   

    public void ShakeCamera()
    {
        if (_canShake == true)
        {
            StartCoroutine(ShakeCoroutine());
        }

        _shakeTimer = shakeDuration;
        //StartCamerashakeEffect();
    }

    private IEnumerator ShakeCoroutine()
    {
        _canShake = true;
        float timer = shakeDuration;
        while (timer > 0)
        {
            cameraTransform.localPosition = originalCameraPos + Random.insideUnitSphere * _shakeAmount;
            timer -= Time.deltaTime;
            yield return null;
        }
        cameraTransform.localPosition = originalCameraPos;
        _canShake = false;
    }

    /* public void StartCamerashakeEffect()
    {   if (_shakeTimer  > 0)
        {
           cameraTransform.localPosition = originalCameraPos + Random.insideUnitSphere * _shakeAmount;
           _shakeTimer -= Time.deltaTime;

        }

        else
        {
            _shakeTimer = 0f;
            cameraTransform.position = originalCameraPos;
            _canShake= false;

        }

    }  */  //it was better to turn it into a coroutine
}
