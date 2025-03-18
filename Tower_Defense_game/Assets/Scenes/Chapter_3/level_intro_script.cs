using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Chap3_intros : MonoBehaviour
{
    //Load Scene
    public void Chap3_intro_1()
    {
        SceneManager.LoadScene("Lvl_1_CH3");
    }

    public void Chap3_intro_2()
    {
        SceneManager.LoadScene("Lvl_2_CH3");
    }

    public void Chap3_intro_3()
    {
        SceneManager.LoadScene("Lvl_3_CH3");
    }



}