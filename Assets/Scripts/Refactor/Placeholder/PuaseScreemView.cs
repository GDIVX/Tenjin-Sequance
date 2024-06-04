using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PuaseScreemView : MonoBehaviour
{
    [SerializeField] private TextMeshProUGUI text;
    private bool isPuased;

    public void OnGameOver()
    {
        text.text = "You Are Dead";

        Time.timeScale = 0;
        Puase();
    }

    public void TogglePuase()
    {
        if (isPuased)
        {
            gameObject.SetActive(true);
            OnUnpause();
        }
        else
        {
            OnPause();
        }
    }

    public void OnPause()
    {
        text.text = "Paused";
        Puase();
    }

    public void OnUnpause()
    {
        Puase(false);
        gameObject.SetActive(false);
    }

    public void Restart()
    {
        //TODO Temp hardcoded
        SceneManager.LoadScene("Game");
        OnUnpause();
    }

    void Puase(bool value = true)
    {
        isPuased = value;
        if (value)
        {
            Time.timeScale = 0;
        }
        else
        {
            Time.timeScale = 1;
        }
    }
}