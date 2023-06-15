using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Chest : Collectable
{
    public Sprite emptyChest;   //Set this in the object to chest_1
    public int goldAmount = 5;
    protected override void OnCollect()
    {
        //base.OnCollect(coll);  //this is how you would call the original function
        
        if(!collected)
        {
            //can also modify the protected values like this
            collected = true;

            //modify sprite
            GetComponent<SpriteRenderer>().sprite = emptyChest; //set sprite
            GameManager.instance.money += goldAmount;           //actually grant the gold
            //create floating text for gold pickup (color is yellow)
            GameManager.instance.ShowText("+" + goldAmount + " Gold", 16, new Color(1f, 0.92f, 0.016f, 1f), transform.position, Vector3.up * 25, 1.5f);
            //Debug.Log("Grant " + goldAmount + " Gold");
        }

        //GameManager.instance.ShowText(goldAmount.ToString() + " Gold", 8, new Color(1f, 0.92f, 0.016f, 1f), new Vector3(1,1,1), new Vector3(1,1,1), 10)
    }
}
