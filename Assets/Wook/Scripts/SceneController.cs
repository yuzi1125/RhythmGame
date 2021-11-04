using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    public void ChangeScene(string _Scene)
    {
        GameManager.Instance.TimeCheck = 0;
        if (_Scene == "Rhythm")
        {
            GameManager.Instance.StartCount++;


        }
        SceneManager.LoadScene(_Scene);

    }
    public void End()
    {
        Application.Quit();
    }
}
