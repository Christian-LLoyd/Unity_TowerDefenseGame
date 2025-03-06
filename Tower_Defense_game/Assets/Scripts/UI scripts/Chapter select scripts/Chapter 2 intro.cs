using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chapter2_intro : MonoBehaviour
{
    //Load Scene
    public void Chapter_2_intro()
    {
        SceneManager.LoadScene("Chapter2_Level_select");
    }
}