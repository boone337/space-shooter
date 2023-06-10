using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography.X509Certificates;

using UnityEngine;

public class CameraShake : MonoBehaviour
{
    //Camera
    public Transform cameraTransform;

    private Vector3 originalCameraPos;

    //Shake
    [SerializeField]
    public float shakeDuration = 2.0f;
    [SerializeField]
    private float _shakeAmount = 0.7f;

    private float _nukeShake = 3.0f;

    private bool _canShake = false;

    private float _shakeTimer;

    private Player _player;



    // Start is called before the first frame update
    void Start()
    {
        _player = GameObject.Find("Player").GetComponent<Player>();

        originalCameraPos = cameraTransform.localPosition;

    }

    // Update is called once per frame
    void Update()
    {


        if (Input.GetKeyDown(KeyCode.B))
        {
            NukeShake();
        }

        if (_canShake)
        {
            StartCamerashakeEffect();
        }

    }
   
     public void NukeShake()
    {
        _canShake = true;    
        _shakeTimer = _nukeShake;
    }
    

    public void ShakeCamera()
    {
        _canShake = true;
        _shakeTimer = shakeDuration;

    }

    public void StartCamerashakeEffect()
    {   if (_shakeTimer > 0)
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

    }
}
