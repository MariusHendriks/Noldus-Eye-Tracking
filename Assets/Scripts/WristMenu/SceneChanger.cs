using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneChanger : MonoBehaviour
{
    public void ChangeToMainMenu()
    {
        SceneManager.LoadScene("MainMenu");
    }

    public void ChangeToTrafficScene()
    {
        SceneManager.LoadScene("TrafficScene");
    }

    public void ChangeToBrownian()
    {
        SceneManager.LoadScene("Brownian");
    }
}
