using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class SceneController : MonoBehaviour
{
    #region singleton
    private static SceneController instance = null;

    void Awake()
    {
        if (null == instance)
        {
            instance = this;
        }
        else
            Destroy(this.gameObject);
    }

    public static SceneController Instance
    {
        get
        {
            if (null == instance)
                return null;
            return instance;
        }
    }
    #endregion

    public void ChangeScene(string _Scene)
    {
        GameManager.Instance.TimeCheck = 0;
        if (_Scene == "DialogScene")
        {

        }
        else if (_Scene == "Rhythm")
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
