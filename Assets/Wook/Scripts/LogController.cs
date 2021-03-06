using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.IO;
public class LogController : MonoBehaviour
{
    private static LogController instance;

    public static LogController Instance { get { return instance; } }

    private void Awake()
    {
        if (null == instance)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else Destroy(gameObject);

    }
    public void SetLog(string _text)
    {
        string str = "Log" + GameManager.Instance.StartCount + ".txt";
        StreamWriter sw = File.AppendText(str);
        sw.WriteLine(_text);    //저장될 string
        sw.Close();
    }
}
