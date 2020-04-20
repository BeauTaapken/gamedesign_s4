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
    public TextMeshProUGUI TmpRotateControls;
    public TextMeshProUGUI TmpAttackControls;

    public Sensitivity Sensitivity;

    void Start()
    {
        mainMenu.SetActive(true);
        settings.SetActive(false);
        SetSensitivityText();
        setControlsText();
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

    private void SetSensitivityText()
    {
        TmpSensitivity.text = "Sensitivity: " + Sensitivity.GetSensitivity();
    }

    private void setControlsText()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            TmpRotateControls.text = "Rotate Weapon: Left trigger + right stick";
            TmpAttackControls.text = "Attack: Right trigger";
        }
        else
        {
            TmpRotateControls.text = "Rotate Weapon: Right mouse button + circle mouse";
            TmpAttackControls.text = "Attack: Left mouse button";
        }
    }
}
