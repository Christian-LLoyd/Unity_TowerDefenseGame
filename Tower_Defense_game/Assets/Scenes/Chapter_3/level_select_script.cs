using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chap3_levelselect : MonoBehaviour
{
    //Load Scene
    public void Chap3_lvl1()
    {
        SceneManager.LoadScene("Lvl_1-CH3-Intro");
    }

    public void Chap3_lvl2()
    {
        SceneManager.LoadScene("Lvl_2-CH3-Intro");
    }

    public void Chap3_lvl3()
    {
        SceneManager.LoadScene("Lvl_3-CH3-Intro");
    }



}