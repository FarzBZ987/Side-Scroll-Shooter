using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class OpenGame : MonoBehaviour
{
    public void Load()
    {
        Time.timeScale = 1;
        SceneManager.LoadScene(1);
    }
}