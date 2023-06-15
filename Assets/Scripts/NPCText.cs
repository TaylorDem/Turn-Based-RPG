using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NPCText : Collidable
{
    public string message;
    public Color color = Color.white;

    private float lastShown;
    private float cooldown = 7.0f;

    protected override void OnCollide(Collider2D coll)
    {
        //if default time (never hit) and past CD.
        if(Time.time - lastShown > cooldown || lastShown == 0.0f)
        {   
            if(coll.name == "Player")
            {
                lastShown = Time.time;
                GameManager.instance.ShowText(message, 25, color, transform.position + new Vector3(0,0.08f,0), Vector3.zero, cooldown);
            }
        }
    }
}
