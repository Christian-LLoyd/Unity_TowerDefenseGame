using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl1CH2Intro : MonoBehaviour
{
    //Load Scene
    public void ContinueButton_lvl1()
    {
        SceneManager.LoadScene("Lvl_1_CH2");
    }

    public void ContinueButton_lvl2()
    {
        SceneManager.LoadScene("Lvl_2_CH2");
    }

    public void ContinueButton_lvl3()
    {
        SceneManager.LoadScene("Lvl_3_CH2");
    }
}