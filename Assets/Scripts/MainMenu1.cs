using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class MainMenu1 : MonoBehaviour
{
    public void PlayGame()
    {
        SceneManager.LoadScene("Battlefield Jennifer");
    }
    public void QuitGame()
    {
        Application.Quit();
    }
}
