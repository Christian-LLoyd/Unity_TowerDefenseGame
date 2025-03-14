using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chap2_lvl1 : MonoBehaviour
{
    //Load Scene
    public void Chap2_level1()
    {
        SceneManager.LoadScene("Lvl_1-CH2-Intro");
    }

    //Quit Game
    public void Exit_Button()
    {
        Application.Quit();
        Debug.Log("The Player has Quit the game");
    }
}