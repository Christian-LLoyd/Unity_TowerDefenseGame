using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl1Intro : MonoBehaviour
{
    //Load Scene
    public void ContinueButton()
    {
        SceneManager.LoadScene("Lvl_1");
    }
}