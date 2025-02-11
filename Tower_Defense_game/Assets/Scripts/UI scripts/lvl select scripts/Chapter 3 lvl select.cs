using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter3_lvlselect : MonoBehaviour
{
    //Load Scene
    public void Chapter_levelselect()
    {
        SceneManager.LoadScene("Chapter3_Level_select");
    }

    //Quit Game
    public void Exit_Button()
    {
        Application.Quit();
        Debug.Log("The Player has Quit the game");
    }
}