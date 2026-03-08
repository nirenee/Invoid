using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PauseMenu : MonoBehaviour
{
    public GameObject menupausa;
    public GameObject menuoptions;
    private bool isGamepaused;

    public void Pause()
    {
        Time.timeScale = 0f;
        menupausa.SetActive(true);
    }
    public void Options()
    {
        Time.timeScale = 0f;
        menupausa.SetActive(false);
        menuoptions.SetActive(true);
    }
    public void OptionsBAck()
    {
        Time.timeScale = 0f;
        menupausa.SetActive(true);
        menuoptions.SetActive(false);
    }
    public void Resume()
    {
        Time.timeScale = 1f;
        menupausa.SetActive(false);
    }
   
}
