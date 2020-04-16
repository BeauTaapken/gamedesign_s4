using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class onGameOverLoaded : MonoBehaviour
{
    public TextMeshProUGUI TmpUguiRestart;

    // Start is called before the first frame update
    void Start()
    {
        if (Input.GetJoystickNames().Length > 0)
        {
            TmpUguiRestart.text = "Press the A button to restart";
        }
        else
        {
            TmpUguiRestart.text = "Press R to restart";
        }

        GameObject[] monsters = GameObject.FindGameObjectsWithTag("Monster");
        GameObject[] bosses = GameObject.FindGameObjectsWithTag("Boss");
        foreach (GameObject monster in monsters)
        {
            if (monster.GetComponent<Animator>() != null)
            {
                monster.GetComponent<Animator>().SetBool("attacking", true);
            }
        }

        foreach (GameObject boss in bosses)
        {
            if (boss.GetComponent<Animator>() != null)
            {
                boss.GetComponent<Animator>().SetBool("attacking", true);
            }
        }
    }

    void Update()
    {
        if (Mathf.Approximately(Input.GetAxis("Restart"), 1))
        {
            SceneManager.LoadScene("TestScene_Beau");
        }
    }
}
