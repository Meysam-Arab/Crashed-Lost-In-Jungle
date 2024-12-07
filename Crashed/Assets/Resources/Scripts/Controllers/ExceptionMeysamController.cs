using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;

public class ExceptionMeysamController : MonoBehaviour
{

 

    void Awake()
    {
        Application.logMessageReceived += HandleException;
        DontDestroyOnLoad(gameObject);
    
    }

    void HandleException(string logString, string stackTrace, LogType type)
    {
        if (type == LogType.Exception)
        {
            //handle here

            string path = Application.persistentDataPath + Path.AltDirectorySeparatorChar + "errors";

            if (!Directory.Exists(path))
                Directory.CreateDirectory(path);

            path += Path.AltDirectorySeparatorChar + "meysamerror.txt";

            File.WriteAllText(path, logString + ", stack: " + stackTrace);
        }
    }
}
