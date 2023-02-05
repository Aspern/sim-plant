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
    }

    public void startLevel2(){
        SceneManager.LoadScene("Level2");
    }

        void Start()
    {
        int finishedLevel = PlayerPrefs.GetInt("finishedLevel");
        if(finishedLevel >=2){
            Level2.GetComponent<Button>().interactable = true;
        }
    }
} 
