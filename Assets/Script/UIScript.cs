using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
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
    private Text _restartText;

    [SerializeField]
    private GameManager _gameManager;

    // Start is called before the first frame update
    void Start()
    {
        _scoreText.text = "Score: " + 0;  //assign text component to handle

        _gameOverText.gameObject.SetActive(false);

        _gameManager = GameObject.Find("Game_Manager").GetComponent<GameManager>();

        if(_gameManager == null)
        {
            Debug.LogError("GameManager is NULL");

        }

    }

    public void UpdateScore(int playerScore)

    {
        _scoreText.text = "Score" + playerScore;
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