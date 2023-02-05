using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public void PlayGame(){
        
        PlayerPrefs.GetInt("currentLevel");

        SceneManager.LoadScene(PlayerPrefs.GetInt("currentLevel"));
        Debug.Log("current level: " + PlayerPrefs.GetInt("currentLevel"));
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    void Start()
    {
        mixer.SetFloat("MusicVol", Mathf.Log10(PlayerPrefs.GetFloat("MusicVol", 0.75f)) * 20);
        
        if(PlayerPrefs.GetInt("currentLevel") == 0){
            PlayerPrefs.SetInt("currentLevel", 1);
        }
    }
} 
