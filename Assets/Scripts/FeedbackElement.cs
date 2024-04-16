using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[CreateAssetMenu(fileName = "Data", menuName = "Feedback", order = 1)]

[System.Serializable]
public class FeedbackElement
{
    public int id;
    public string name;
    public string path_OpenFace;
    public string path_Video;
    public string path_Audio;
}

[System.Serializable]
public class FeedbackList
{
    public List<FeedbackElement> elements;
    
}