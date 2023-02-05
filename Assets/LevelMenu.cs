using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class LevelMenu : MonoBehaviour
{
    public Button Level2;

    public void startLevel1(){
        SceneManager.LoadScene("Level1");
        PlayerPrefs.SetInt("currentLevel", 1);

    }

    public void startLevel2(){
        SceneManager.LoadScene("Level2");
        PlayerPrefs.SetInt("currentLevel", 2);

    }

        void Start()
    {
        if(PlayerPrefs.GetInt("finishedLevel") >=2){
            Level2.GetComponent<Button>().interactable = true;
        }
    }
} 
