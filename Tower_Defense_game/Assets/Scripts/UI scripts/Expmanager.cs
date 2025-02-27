using UnityEngine;
using UnityEngine.UI;

public class ExpManager : MonoBehaviour
{

    [SerializeField] GameObject Powerupmenu;
    [SerializeField] GameObject Pause_Menu;
    [SerializeField] GameObject Healthbar_prefab;
    
    public Image expBar;
    public float expAmount = 100f;

    void Update()
    {
        if (expAmount >= 100)
        {
            Powerupmenu.SetActive(true);
            Pause_Menu.SetActive(false);
            Healthbar_prefab.SetActive(false);
            expAmount = 0;
            expBar.fillAmount = 0;
            Time.timeScale = 0;
        }

        if (Input.GetKeyDown(KeyCode.Keypad1))
        {
            TakeExp(10);
        }

        if (Input.GetKeyDown(KeyCode.Keypad5))
        {
            GiveExp(20);
        }
    }

    public void TakeExp(float exp)
    {
        expAmount -= exp;
        expBar.fillAmount = expAmount / 100f;
    }

    public void GiveExp(float expGive) 
    {
        expAmount += expGive; 
        expAmount = Mathf.Clamp(expAmount, 0, 100);
        expBar.fillAmount = expAmount / 100f;
    }
}