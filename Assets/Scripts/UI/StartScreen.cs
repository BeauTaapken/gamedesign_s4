using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameObject mainMenu;
    public GameObject settings;

    public TextMeshProUGUI TmpSensitivity;
    public Sensitivity Sensitivity;

    void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        SetSensitivityText();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TestScene_Beau");
    }

    public void OpenSettings()
    {
        mainMenu.SetActive(false);
        settings.SetActive(true);
    }

    public void SetSensitivity(Slider sensitivitySlider)
    {
        Sensitivity.SetSensitivity((int)sensitivitySlider.value);
        SetSensitivityText();
    }

    public void CloseSettings()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    public void SetSensitivityText()
    {
        TmpSensitivity.text = "Sensitivity: " + Sensitivity.GetSensitivity();
    }
}
