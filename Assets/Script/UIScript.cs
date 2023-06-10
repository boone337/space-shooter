using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using UnityEngine.Serialization;
using UnityEditor.Experimental.GraphView;

public class UIScript : MonoBehaviour
{
    [SerializeField]
    private Text _gameOverText;

    [SerializeField]
    private Text _scoreText;   //handle to Text

    [SerializeField]
    private Image _LivesImg;

    [SerializeField]
    private Sprite[] _liveSprites;

    [SerializeField]
    private Sprite[] _bombSprites;

    [SerializeField]
    private Image _bombImg; 

    [SerializeField]
    private Text _restartText;

    [SerializeField]
    private GameManager _gameManager;

    [SerializeField]
    private Text _ammoText;

    [SerializeField]
    private Player _player;

    [SerializeField]
    private Image _thrusterBar;

    [SerializeField]
    private float _thrusteramount = 100f;

    private int _maxAmmo = 15;

    [SerializeField]
    private TMP_Text _waveStartText; // using text mesh pro


    [SerializeField]
    private bool Ammobool  = true;

    [SerializeField]
    public Image _bossLifeBar;

    [SerializeField]
    public float _bosshealth = 100f;


    // Start is called before the first frame update
    void Start()
    {
        _bossLifeBar.enabled = false;  //This makes the image disappear

        _player = GameObject.Find("Player").GetComponent<Player>();   
        
        _ammoText.text = "AMMO COUNT:" + 15;  
        
        // 15 will show up after play is initiated

        _scoreText.text = "Score: " + 0;  //assign text component to handle

        _gameOverText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");

        }
        _waveStartText.text = "READY!!";
        StartCoroutine(_flashWave());



    }

    public void  UpdateChangeWaveCount(int waveNumber)
    {
        _waveStartText.text = "WAVE " + waveNumber;
        _waveStartText.enabled = true;  

        if (_flashWave() != null)
        {
            // StopCoroutine(_flashWave);
           // _flashWave = Flashwave();
            
        }
        StartCoroutine(_flashWave());


    }

   

    public void UpdateBossText()
    {
        _waveStartText.text = "BOSS";
        StartCoroutine (_flashWave());
    }

    public void FixedUpdate()
    {
        


    }

    IEnumerator _flashWave()

    {
        for (int i =0; i <3; i++)
        {
            yield return new WaitForSeconds(.5f);
            _waveStartText.enabled =false;
            yield return new WaitForSeconds(.5f);
            _waveStartText.enabled=true;

        }

        _waveStartText.enabled = false;

    }

    

    public void UpdateBossDamage(float damage )
    {
        

        _bosshealth -= 1;

        _bossLifeBar.fillAmount = _bosshealth / 100f;


    }

    public void UpdateThrusterLevel(float value)
    {
        float amount = value / _thrusteramount;
        _thrusterBar.fillAmount = amount; 
        
    }

    public void UpdateAmmoDisplay(int Ammo )
    {
            
        _ammoText.text = " CURRENT/MAX: " + $"{Ammo} / {_maxAmmo}";
        

     /* if (Ammo >= 15)

        {
            _ammoText.text = " MAX AMMO " + Ammo;
        }*/

      
       if (Ammo ==0 )
        {         

           StartCoroutine(ammoflickerRoutine());
           
        }
 
       else
        {
            Ammobool = false;

            _ammoText.text = " CURRENT/MAX: " + $"{Ammo} / {_maxAmmo}";
        }
    }

  IEnumerator ammoflickerRoutine()
    {
        //while (true) 

        Ammobool = true;

        while (Ammobool)

        {
            _ammoText.text = "AMMO:";

            yield return new WaitForSeconds(0.5f);

            _ammoText.text = "DEPLETED:" + 00;

            yield return new WaitForSeconds(0.5f);

        }
         
        
    }

    public void UpdateScore(int playerScore)

    {
        _scoreText.text = "Score" + playerScore;
        
    }

    public void UpdateBomb(int currentBomb)
    {
        _bombImg.sprite = _bombSprites[currentBomb];
    }

    public void UpdateLives(int currentLives)
    {
        //Need to access display img sprite
        //give it a new one based on the currentLives index
        _LivesImg.sprite = _liveSprites[currentLives];
        
        if (currentLives == 0)
        {
            //_gameOverText.gameObject.SetActive(true);
            GameOverSequence();
          //  StartCoroutine(GameFlickerRoutine());
        }
    }
 
    void GameOverSequence()
    {
        _gameManager.GameOver();
        _gameOverText.gameObject.SetActive(true);
        _restartText.gameObject.SetActive(true);    
        StartCoroutine(GameFlickerRoutine());
    }

    IEnumerator GameFlickerRoutine()
    {
        while (true)
        {
            _gameOverText.text = "GAME OVER ";                             
            yield return new WaitForSeconds(0.5f);
            _gameOverText.text = "";
            yield return new WaitForSeconds(0.5f);
        }
    }


}