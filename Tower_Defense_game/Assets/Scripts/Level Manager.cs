using System.Collections.Generic;
using UnityEngine;

public class LevelManager : MonoBehaviour
{
    public static LevelManager instance;

    public bool levelActive;
    private bool levelVictory;
    public Target_Castle theCastle;

    public List<Enemy_Controller> activeEnemies = new List<Enemy_Controller>();

    void Awake()
    {
        instance = this;
    }

    void Start()
    {
        levelActive = true;
        theCastle = FindAnyObjectByType<Target_Castle>();
    }

    void Update()
    {
        if (!levelActive) return;

        // Win Condition (All Enemies Defeated)
        if (activeEnemies.Count == 0)
        {
            levelActive = false;
            levelVictory = true;
            UIController.instance.levelCompleteScreen.SetActive(true);
            Debug.Log("🔥🔥🔥🔥🔥🔥n🔥🔥🔥🔥🔥🔥 🔥🔥🔥🔥🔥🔥n🔥🔥🔥🔥🔥🔥 potanNANALO KA GALING MO TALAGA ");
        }
    }


    public void CastleDestroyed()
    {
        if (levelActive)
        {
            levelActive = false;
            levelVictory = false;
            UIController.instance.levelFailedScreen.SetActive(true);
            Debug.Log("🔥🔥🔥🔥🔥🔥natalo ka bobo");
        }
    }
}
