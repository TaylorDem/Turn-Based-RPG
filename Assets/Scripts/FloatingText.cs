using UnityEngine;
using UnityEngine.UI;   //need this for text

public class FloatingText   //no Monobehavior needed here so less using statements too
{
    public bool active;
    public GameObject go;
    public Text txt;
    public Vector3 motion;
    public float duration;
    public float lastShown;

    public void Show()
    {
        active = true;
        lastShown = Time.time;  //pull start time from when the text appeared.
        go.SetActive(active);   //gotta check what this function does from GameObject Library
    }

    public void Hide()
    {
        active = false;
        go.SetActive(active);
    }

    //our update function
    public void UpdateFloatingText()
    {
        if(!active)
            return;
        //time right now minus time when text appeared > duration means time to remove the text
        if(Time.time - lastShown > duration)
            Hide();

        //move text
        go.transform.position += motion * Time.deltaTime;
    }
}
