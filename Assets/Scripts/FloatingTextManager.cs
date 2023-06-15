using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class FloatingTextManager : MonoBehaviour
{
    public GameObject textContainer;
    public GameObject textPrefab;

    private List<FloatingText> floatingTexts = new List<FloatingText>();

    private void Update()   //gotta put the update from FloatingText.cs to run
    {
        foreach(FloatingText txt in floatingTexts)  //update each text in the list
                    txt.UpdateFloatingText();
    }
    


    public void Show(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        //run below function to initialize the text we want to show
        FloatingText floatingText = GetFloatingText();

        //grab the msg we pass it
        floatingText.txt.text = msg;
        //how it looks
        floatingText.txt.fontSize = fontSize;
        floatingText.txt.color = color;
        //put it on the screen, the "world" position is different from the camera or grid position
        floatingText.go.transform.position = Camera.main.WorldToScreenPoint(position);
        //movement + duration
        floatingText.motion = motion;
        floatingText.duration = duration;

        floatingText.Show();    //call the show function
    }

    private FloatingText GetFloatingText()
    {
        FloatingText txt = floatingTexts.Find(t => !t.active); 
        //this line creates a txt using the list where they already exist
        //works similarly to a for() if() combo

        //if nothing was found, create a new one
        if(txt == null)
        {
            txt = new FloatingText();
            txt.go = Instantiate(textPrefab);   //create the gameobject
            txt.go.transform.SetParent(textContainer.transform);    //not sure what this does tbh
            txt.txt = txt.go.GetComponent<Text>();      //pull text component

            floatingTexts.Add(txt);
        }
        return txt;
    }

}