using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class StartScreen : MonoBehaviour
{
    public GameObject OptionPanel, MusicOn, MusicOff, SoundOn, SoundOff,BackgroundSound;
    private int musicnum, soundnum;
    private void Update()
    {
        soundnum = PlayerPrefs.GetInt("sound");
        musicnum = PlayerPrefs.GetInt("music");
        if (musicnum == 0)
        {
            BackgroundSound.SetActive(false);
            MusicOn.SetActive(false);
            MusicOff.SetActive(true);

        }
        if (musicnum == 1)
        {
            MusicOn.SetActive(true);
            MusicOff.SetActive(false);
            BackgroundSound.SetActive(true);
        }
        musicnum = PlayerPrefs.GetInt("music");
        if (soundnum == 0)
        {
            SoundOn.SetActive(false);
            SoundOff.SetActive(true);
          

        }
        if (soundnum == 1)
        {
            SoundOn.SetActive(true);
            SoundOff.SetActive(false);
          
        }
    }
    public void OptionOn()
    {
        OptionPanel.SetActive(true);
        Debug.Log("abc");
    }
    public void OptionOff()
    {
        OptionPanel.SetActive(false);
    }
    public void MusicOn1()
    {
        PlayerPrefs.SetInt("music", 1);
    }
    public void MusicOff1()
    {
        PlayerPrefs.SetInt("music", 0);
    }
    public void SoundOn1()
    {
        PlayerPrefs.SetInt("sound", 1);
    }
    public void SoundOff1()
    {
        PlayerPrefs.SetInt("sound", 0);
    }
    public void StartGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }
    public void Quit()
    {
        Application.Quit();
    }
}
