using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Ink.Runtime;
using UnityEngine.UI;
using TMPro;

public class DialogSystem : MonoBehaviour
{
    public TextAsset inkJson;
    private Story story;
    public GameObject tempbutton;
    public GameObject text;
    public GameObject canvas;
    public Button closedialog;
    public string text1;
    List<GameObject> stufftodestroy;
    private void Start()
    {
        stufftodestroy = new List<GameObject>();
        closedialog = gameObject.transform.Find("dialogue").Find("closedialog").GetComponent<Button>();
        closedialog.onClick.AddListener(closeDialogfunc);
        canvas = gameObject.transform.Find("dialogue").gameObject;
        tempbutton = gameObject.transform.Find("dialogue").Find("button").gameObject;
        text = gameObject.transform.Find("dialogue").Find("text").gameObject;
    }
    public void startStory()
    {
        story = new Story(inkJson.text);
        continueStory();
    }
    public void continueStory()
    {
        if (stufftodestroy.Count != 0)
        {
            destroyall(stufftodestroy);
        }
       
        if (story.canContinue)
        { 
            text.GetComponent<TMP_Text>().text= story.ContinueMaximally();
            int index = 0;
            float y = -250;

            foreach (Choice a in story.currentChoices)
            {
                GameObject choice = Instantiate(tempbutton,canvas.transform);
                choice.gameObject.transform.Find("btext").GetComponent<TMP_Text>().text = a.text;
                choice.name = index.ToString();
                choice.active = true;
                choice.transform.localPosition = new Vector2(choice.transform.localPosition.x, y); ;
                choice.GetComponent<Button>().onClick.AddListener(delegate { choosechoice(int.Parse(choice.name)); });
                stufftodestroy.Add(choice);
                index++;
                y -= 60.0f;
            }
        }
    }

    // Update is called once per frame
    void closeDialogfunc()
    {
         canvas.active = false;
    }
    void choosechoice(int index)
    {
        story.ChooseChoiceIndex(index);
        continueStory();
    }
    void destroyall(List<GameObject> stufftodestroy)
    {
        foreach(GameObject item in stufftodestroy)
        {
            Destroy(item);
        }
    }
}
