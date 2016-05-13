using UnityEngine;
using System.Collections;

namespace NetherWars
{
    public enum LoggerLogType
    {
        Message,
        Event,
        Action,
        Warning,
        Error,
    }

    public class Logger 
    {
 
        public static void Log(string str)
        {
            Logger.Log(LoggerLogType.Message, str);
        }

        public static void LogMessage(string str)
        {
            Logger.Log(LoggerLogType.Message, str);
        }

        public static void LogEvent(string str)
        {
            Logger.Log(LoggerLogType.Event, str);
        }

        public static void LogAction(string str)
        {
            Logger.Log(LoggerLogType.Action, str);
        }

        public static void LogWarning(string str)
        {
            Logger.Log(LoggerLogType.Warning, str);
        }

        public static void LogError(string str)
        {
            Logger.Log(LoggerLogType.Error, str);
        }

        public static void Log(LoggerLogType logType, string str)
        {
            switch(logType)
            {
                case LoggerLogType.Message:
                    {
                        UnityEngine.Debug.Log(str);
                        break;
                    }
                case LoggerLogType.Event:
                    {
                        UnityEngine.Debug.Log("<color=blue><b>Event: </b>" + str + "</color>");
                        break;
                    }
                case LoggerLogType.Action:
                    {
                        UnityEngine.Debug.Log("<color=green><b>Action: </b>" + str + "</color>");
                        break;
                    }
                case LoggerLogType.Warning:
                    {
                        UnityEngine.Debug.LogWarning(str);
                        break;
                    }
                case LoggerLogType.Error:
                    {
                        UnityEngine.Debug.LogError(str);
                        break;
                    }

            }
        }
      
    }

}

