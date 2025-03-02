using UnityEngine;
using System.Collections.Generic;
using System.Collections;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;



public class powerupmenu : MonoBehaviour
{

    [SerializeField] GameObject Powerupmenu;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Healthbar_prefab;


   public void debugpause()
   {
        Powerupmenu.SetActive(true);
        Pause_Menu.SetActive(false);
        Healthbar_prefab.SetActive(false);
        Time.timeScale = 0; 
   }

   public void PowerupResume()
   {
        Powerupmenu.SetActive(false);
        Pause_Menu.SetActive(true);
        Healthbar_prefab.SetActive(true);
        Time.timeScale = 1; 
        


   }
}
