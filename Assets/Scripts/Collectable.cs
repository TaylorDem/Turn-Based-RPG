using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Collectable : Collidable     
{
    protected bool collected;   //defaults to false apparently


    protected override void OnCollide(Collider2D coll)  //not sure what Collider2D is still
    {
        if(coll.name == "Player")
            OnCollect();
    }

    protected virtual void OnCollect()
    {
        collected = true;
    }
}
