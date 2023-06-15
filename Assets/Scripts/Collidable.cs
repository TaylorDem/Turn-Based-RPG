using System.Collections;
using System.Collections.Generic;
using UnityEngine;
  
  //this line ensures a boxcollider component is on the object
  [RequireComponent(typeof(BoxCollider2D))]    
public class Collidable : MonoBehaviour
{
    //this ContactFilter2D is still unknown to me but works
    public ContactFilter2D filter;
    private BoxCollider2D boxCollider;  
    //gotta research this too. what exactly is Collider2D?
    private Collider2D[] hits = new Collider2D[10];

    //we make this one inheritable so that we can change the event/trigger for different items

    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void Update()
    { 
        //check for overlap of boxcollider objects
        boxCollider.OverlapCollider(filter, hits);
        for(int i =0; i<hits.Length ; i++)
        {
            if (hits[i] == null) 
                continue;

            //Debug.Log(hits[i].name);
            OnCollide(hits[i]);

            //clean up array once its been handled
            hits[i] = null;
            
        }
    }

    protected virtual void OnCollide(Collider2D coll)
    {
        //this is defined here only so I can call it to modify it later for now.
        //Debug.Log("OnCollide not defined for: " + this.name);
        //this.name should tell us what object has no definition for collision
    }
}
