using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class MainMenuLogic : MonoBehaviour
{
    public GameObject PanelMainMenu;
    public GameObject PanelOption;
    public GameObject PanelInfo;

    public void OpenOption()
    {
        PanelMainMenu.SetActive(false);
        PanelInfo.SetActive(false);
        PanelOption.SetActive(true);
    }
    public void Back()
    {
        PanelMainMenu.SetActive(true);
        PanelOption.SetActive(false);
        PanelInfo.SetActive(false);
    }
    public void OpenInfo()
    {
        PanelMainMenu.SetActive(false);
        PanelInfo.SetActive(true);
        PanelOption.SetActive(false);
    }
    public void OpenGamePlay()
    {
        SceneManager.LoadScene("Maze");
    }
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Hello world start");
    }

    // Update is called once per frame
    void Update()
    {
    }
}