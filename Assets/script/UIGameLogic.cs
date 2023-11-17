using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class UIGameLogic : MonoBehaviour
{
    public Image HealthBar;
    public Text HealthText;
    public GameObject PanelGameResult;
    public Text TextGameResult;

    public void UpdateHealthBar(float CurrentHealth, float MaxHealth)
    {
        HealthBar.fillAmount = CurrentHealth / MaxHealth;
        if(CurrentHealth <= 0) 
        {
        CurrentHealth = 0f;
        HealthText.text = CurrentHealth.ToString();
        GameResult(false);
        }else{
        HealthText.text = CurrentHealth.ToString();
        }
    }
    // Start is called before the first frame update
    void Start()
    {

    }

    // Update is called once per frame
    void Update()
    {

    }

    public void GameResult(bool win)
    {
        Time.timeScale = 0f;    
        PanelGameResult.SetActive(true);
        Cursor.lockState = CursorLockMode.None;
        Cursor.visible = true;
        if (win)
        {
            TextGameResult.color = Color.green;
            TextGameResult.text = "Mission Complete";

        }
        else
        {
            TextGameResult.color = Color.red;
            TextGameResult.text = "Game Over";
        }
    }

    public void GameResultDecision(bool TryAgain)
    {
        if (TryAgain) SceneManager.LoadScene("Maze");
        else SceneManager.LoadScene("Menu");
    }
}
