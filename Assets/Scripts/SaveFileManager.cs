using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using UnityEngine;
using System;

public static class SaveFileManager 
{
    private const string DataPathFolder = "Attempts";

    public static bool SaveAttempt(string name, int wave)
    {
        try
        {
            string folderPath = GetFolderPath();
            string filePath = Path.Combine(folderPath, name);

            SaveFile saveFile = new SaveFile(name, DateTime.Now, wave);
            string json = JsonUtility.ToJson(saveFile);
            File.WriteAllText(filePath, json);
            return true;
        }
        catch (Exception ex)
        {
            return false;
        }
    }

    public static SaveFile GetAttempt(string name)
    {
        try
        {
            string folderPath = GetFolderPath();
            string filePath = Path.Combine(folderPath, name);
            if (File.Exists(filePath))
            {

                string textObject = File.ReadAllText(filePath);
                SaveFile saveFile = JsonUtility.FromJson<SaveFile>(textObject);
                if (saveFile != null)
                {
                    return saveFile;
                }
            }
            return null;
        }
        catch (Exception ex)
        {
            return null;
        }
    }

    private static string GetFolderPath()
    {
        string folderPath = Path.Combine(Application.persistentDataPath, DataPathFolder);
        if (!Directory.Exists(folderPath))
        {
            Directory.CreateDirectory(folderPath);
        }
        return folderPath;
    }



}
