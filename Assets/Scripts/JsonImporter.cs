using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Text.Json;
using System.Text.Json.Serialization;
using Palmmedia.ReportGenerator.Core.Common;
using System.IO;
using System.Threading.Tasks;
using Unity.VisualScripting;
using System;
using UnityEditor;

public static class JsonImporter
{

    public static FeedbackList ParseJSON()
    {
        if (File.Exists(@"Assets\Resources\FeedbackData.json"))
        {
            
            string jsonString = File.ReadAllText(@"Assets\Resources\FeedbackData.json");
            FeedbackList import = JsonUtility.FromJson<FeedbackList>(jsonString);

            return import;
        }
        else
        {
            Debug.LogError("JSON file not found!");
            return null;
        }

        

        
    }

}
