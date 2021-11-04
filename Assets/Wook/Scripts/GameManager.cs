using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
public class GameManager : MonoBehaviour
{
    private static GameManager instance;

    public int StartCount = 0;
    public int Score;


    public float TimeCheck = 0;
    public float EndTime = 60;
    bool isIngame = false;
    public static GameManager Instance { get { return instance; } }
    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);
    }

    private void Update()
    {
        if(SceneManager.GetActiveScene().name == "Rhythm")
        {
            isIngame = true;
            
        }
        if (!isIngame)
            return;
        TimeCheck += Time.deltaTime;
        if (TimeCheck >= 60)
        {
            TimeCheck = 0;
            isIngame = false;
            SceneManager.LoadScene("End");

        }


    }


}
