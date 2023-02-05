using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class LevelMenu : MonoBehaviour
{
    public void startLevel1(){
        SceneManager.LoadScene("Level1");
    }

    public void startLevel2(){
        SceneManager.LoadScene("Level2");
    }
} 
