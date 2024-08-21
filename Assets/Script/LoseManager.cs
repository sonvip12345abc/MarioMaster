using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEditor;
using UnityEngine.SceneManagement;

public class LoseManager : MonoBehaviour
{
    private bool isClick=false;
    public int currentScore;
    public Text currentScoreText;
    // Start is called before the first frame update
    void Start()
    {
        currentScore = PlayerPrefs.GetInt("currentScore");
    }

    // Update is called once per frame
    void Update()
    {
        currentScoreText.text=currentScore.ToString(); 
        PlayerPrefs.SetInt("currentScore", currentScore);
        if (isClick==true)
        {
            PlayerPrefs.DeleteKey("currentScore");
        }
    }
    public void PlayAgain()
    {
        isClick = true;
        SceneManager.LoadScene(1);
    }
    public void QuitGame()
    {
        isClick = true;
        SceneManager.LoadScene(0);
    }
}
