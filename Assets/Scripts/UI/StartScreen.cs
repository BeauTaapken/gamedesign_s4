using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class StartScreen : MonoBehaviour
{
    public GameObject mainMenuButton;
    public GameObject settingsButton;

    public Toggle ControllerToggle;

    public TextMeshProUGUI TmpSensitivity;
    public TextMeshProUGUI TmpRotateControls;
    public TextMeshProUGUI TmpAttackControls;
    
    public Settings Settings;

    void Start()
    {
        mainMenuButton.SetActive(true);
        settingsButton.SetActive(false);

        Settings.SetSensitivity(100);

        if (Input.GetJoystickNames().Length > 0)
        {
            Settings.SetController(true);
            ControllerToggle.isOn = true;
        }
        else
        {
            Settings.SetController(false);
            ControllerToggle.isOn = false;
        }

        SetSensitivityText();
        setControlsText();
    }

    public void StartGame()
    {
        SceneManager.LoadScene("TestScene_Beau");
    }

    public void OpenSettings()
    {
        mainMenuButton.SetActive(false);
        settingsButton.SetActive(true);
    }

    public void SetSensitivity(Slider sensitivitySlider)
    {
        Settings.SetSensitivity((int)sensitivitySlider.value);
        SetSensitivityText();
    }

    public void SetController(Toggle toggle)
    {
        Settings.SetController(toggle.isOn);
        setControlsText();
    }

    public void CloseSettings()
    {
        mainMenuButton.SetActive(true);
        settingsButton.SetActive(false);
    }

    public void QuitGame()
    {
        Application.Quit();
    }

    private void SetSensitivityText()
    {
        TmpSensitivity.text = "Sensitivity: " + Settings.GetSensitivity();
    }

    private void setControlsText()
    {
        if (Settings.GetController())
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
