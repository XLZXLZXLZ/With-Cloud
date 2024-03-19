using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GlobalGameManager : MonoBehaviour
{
    private static int unlockLevel;
    public static int UnlockLevel
    {
        get { LoadData(); return unlockLevel; }
        set {  unlockLevel = Mathf.Max(value,unlockLevel); SaveData(); }
    }
    private static void SaveData()
    {
        PlayerPrefs.SetInt("level", unlockLevel);
    }

    private static void LoadData()
    {
        unlockLevel = PlayerPrefs.GetInt("level", 1);
    }

    public static void ClearData()
    {
        unlockLevel = 1;
        SaveData();
    }
}
