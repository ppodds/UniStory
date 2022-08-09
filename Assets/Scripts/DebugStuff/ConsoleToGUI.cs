using UnityEngine;

namespace DebugStuff
{
    public class ConsoleToGUI : MonoBehaviour
    {
#if !UNITY_EDITOR
        static string myLog = "";
        private string output;
        private string stack;
 
        void OnEnable()
        {
            Application.logMessageReceived += Log;
        }
 
        void OnDisable()
        {
            Application.logMessageReceived -= Log;
        }
 
        public void Log(string logString, string stackTrace, LogType type)
        {
            output = logString;
            stack = stackTrace;
            myLog = output + "\n" + myLog;
            if (myLog.Length > 5000)
            {
                myLog = myLog.Substring(0, 4000);
            }
        }
 
        void OnGUI()
        {
            myLog = GUI.TextArea(new Rect(10, 10, Screen.width - 10, Screen.height - 10), myLog);
        }
#endif
    }
}