using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MenuBrowser : MonoBehaviour
{
    public GameObject buttonPrefab;
    public GameObject buttonMenu;
    List<FeedbackElement> elements;
    int listLength = 3;



    void Start()
    {
        elements = AppManager.Instance.GetFeedbackList().elements;
        listLength = AppManager.Instance.GetFeedbackList().elements.Count;

        for (int i = 0; i < listLength ; i++)
        {
            GameObject newButton = Instantiate(buttonPrefab, buttonMenu.transform);

            int fbNum = i;
            newButton.GetComponent<FeedbackButton>().feedbackName.text = elements[i].name;
            newButton.GetComponent<Button>().onClick.AddListener( () => SelectFeedback(fbNum) );
        }
    }

    private void SelectFeedback(int feedbackId)
    {
        Debug.Log("Loaded " + elements[feedbackId].name);
        AppManager.Instance.LoadFeedbackRessources(feedbackId);
        //Debug.Log("Loaded " + feedbackId);
    }
}
