using UnityEngine;
using System.Collections;
using System.Net;
using System.Net.Sockets;

/// <summary>
/// A collection of static functions to assist with formatting data.
/// </summary>
public static class Format{

    //transform it into 2k, 3m, etc.
    /// <summary>
    /// Converts a number into a string, using single-character notation for very large numbers.
    /// </summary>
    /// <param name="number">The large integer to convert into a more human-readable string.</param>
    /// <returns>A string representing the number in human-readable form.</returns>
    public static string makeReadable(int number) //int, so no decimals (YAY!)
    {
        int level = 0;
        while (Mathf.Abs(number) >= Mathf.Pow(1000, level+1)) level++;
        string result = number.ToString();
        if (level > 0)
            result = result.Substring(0, result.Length - level * 3) + levelToSuffix(level);
        return result;
    }

    private static string levelToSuffix(int level)
    {
        switch(level)
        {
            case 1: return "K";
            case 2: return "M";
            case 3: return "B";

            //you must be making an incremental game if you get here
                //no performance cost, so might as well add them
            case 4: return "T";
            case 5: return "Qa"; //Quadrillion
            case 6: return "Qi"; //Quintillion
            case 7: return "Sx"; //Sextillion
            case 8: return "Sp"; //Septillion
            case 9: return "Oc"; //Octillion
            case 10: return "No"; //Nonillion
            case 11: return "De"; //Decillion
            case 12: return "Un"; //Undecillion
            case 13: return "Du"; //Duodecillion
            case 14: return "Te"; //Tredecillion
            default: return "";
        }
    }

    /// <summary>
    /// Calculates the position of the mouse cursor in world space relative to the main camera.
    /// </summary>
    /// <returns>The position of the mouse in world space relative to the main camera.</returns>
    public static Vector3 mousePosInWorld()
    {
        return mousePosInWorld(Camera.main); //I don't want to use default parameters, because that would involve some extra computation through null-coalescing (the ?? thing)
    }

    /// <summary>
    /// Calculates the position of the mouse cursor in world space relative to the specified camera.
    /// </summary>
    /// <param name="camera">The camera to use when calculating the position of the mouse cursor.</param>
    /// <returns>The position of the mouse in world space relative to the specified camera.</returns>
    public static Vector3 mousePosInWorld(Camera camera)
    {
        Vector3 screenPoint = Input.mousePosition;
        screenPoint.z = -camera.transform.position.z;
        return screenPoint.toWorldPoint(camera);
    }

    /// <summary>
    /// Returns the float formatted as a time in minutes, seconds, and milliseconds (XX:XX:XXX).
    /// </summary>
    /// <param name="numSeconds">Time in seconds.</param>
    /// <returns>Time formatted as a string (XX:XX:XXX).</returns>
    public static string formatMilliseconds(float numSeconds)
    {
        System.Text.StringBuilder result = new System.Text.StringBuilder(Mathf.FloorToInt(numSeconds / 60f).ToString("00"));
        result.Append(':');

        numSeconds = Mathf.Abs(numSeconds % 60f);
        result.Append(Mathf.FloorToInt(numSeconds).ToString("00"));
        result.Append(':');
        numSeconds = (numSeconds % 1f) / 0.001f;
        result.Append(Mathf.FloorToInt(numSeconds).ToString("000"));
        return result.ToString();
    }

    /// <summary>
    /// Returns the float formatted as a time in minutes and seconds (XX:XX).
    /// </summary>
    /// <param name="numSeconds">Time in seconds.</param>
    /// <returns>Time formatted as a string (XX:XX).</returns>
    public static string formatSeconds(float numSeconds)
    {
        System.Text.StringBuilder result = new System.Text.StringBuilder(Mathf.FloorToInt(numSeconds / 60f).ToString("00"));
        result.Append(':');

        numSeconds = Mathf.Abs(numSeconds % 60f);
        result.Append(Mathf.FloorToInt(numSeconds).ToString("00"));
        return result.ToString();
    }

    /// <summary>
    /// Returns the most local IP Address as a string.
    /// </summary>
    /// <returns>The most local IP Address.</returns>
    public static string localIPAddress()
    {
        IPHostEntry host;
        string localIP = "";
        host = Dns.GetHostEntry(Dns.GetHostName());
        foreach (IPAddress ip in host.AddressList)
        {
            if (ip.AddressFamily == AddressFamily.InterNetwork)
            {
                localIP = ip.ToString();
                break;
            }
        }
        return localIP;
    }
}