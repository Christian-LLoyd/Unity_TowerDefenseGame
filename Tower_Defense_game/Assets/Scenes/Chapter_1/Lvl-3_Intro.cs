using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class lvl3Intro : MonoBehaviour
{
    //Load Scene
    public void ContinueButton()
    {
        SceneManager.LoadScene("Lvl_3");
    }
}