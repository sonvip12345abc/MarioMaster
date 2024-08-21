using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class UIManager : MonoBehaviour
{
    private MarioScript Mario;
    public TextMeshProUGUI scoreText;
    public TextMeshProUGUI highScoreText;
    public Text coinText;
    public int coin;
    public int score,score2;
    public int highscore;
   
    private bool isClick=false;
    private void Start()
    {
        
        if (PlayerPrefs.HasKey("Highscore"))
        {
            highscore = PlayerPrefs.GetInt("Highscore");
        }
        else
        {
            highscore = 0;
        }
       
        Mario = FindObjectOfType<MarioScript>();
        score = PlayerPrefs.GetInt("Score");
        coin = PlayerPrefs.GetInt("coin");
    }
    private void Update()
    {
        
        scoreText.text = score.ToString();
        highScoreText.text = highscore.ToString();
        
            highscore = PlayerPrefs.GetInt("Highscore");
            SaveHighScore();
        
        coinText.text = "X"+coin.ToString();
        PlayerPrefs.SetInt("coin", coin);
        
            PlayerPrefs.SetInt("Score", score);
      

        if (Mario == null)
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("coin");
        }
        if (isClick == true)
        {
            PlayerPrefs.DeleteKey("Score");
            PlayerPrefs.DeleteKey("coin");
            PlayerPrefs.DeleteKey("currentScore");
        }

    }

    public void SaveHighScore()
    {
        if (score > PlayerPrefs.GetInt("Highscore"))
        {
            PlayerPrefs.SetInt("Highscore", score);
        }
    }
    public void TryAgain()
    {
        isClick = true;
        SceneManager.LoadScene(1);
    }
    public void Home()
    {
        isClick = true;
        SceneManager.LoadScene(0);
    }

}
