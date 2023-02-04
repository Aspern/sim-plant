using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.Audio;

public class MainMenu : MonoBehaviour
{
    public AudioMixer mixer;
    public void PlayGame(){
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
    }

    public void QuitGame(){
        Debug.Log("QUIT!");
        Application.Quit();
    }

    public void Awake (){
        Debug.Log("Hallo Welt");
         mixer.SetFloat("MusicVol", Mathf.Log10(0.5f) * 20);
    }
}
