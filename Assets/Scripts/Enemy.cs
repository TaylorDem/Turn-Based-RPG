using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : Mover
{
    // Experience
    public int xpValue = 1;

    // Logic
    public float triggerLength = 1;
    public float chaseLength = 5;
    private bool chasing;
    private bool collidingWithPlayer;
    private Transform playerTransform;
    private Vector3 startingPosition;

    // Hitbox
    public ContactFilter2D filter;
    private BoxCollider2D hitbox;
    private Collider2D[] hits = new Collider2D[10];

    protected override void Start()
    {
        base.Start();
        //this is example that will pull the players transform object as well
        //playerTransform = GameObject.Find("Player").transform;

        playerTransform = GameManager.instance.player.transform;
        startingPosition = transform.position;
        //this GetChild() function pulls based on order they are listed
        hitbox = transform.GetChild(0).GetComponent<BoxCollider2D>();
    }

    private void FixedUpdate()
    {
        //Is player in chasing range?
        if(Vector3.Distance(playerTransform.position, startingPosition) <= chaseLength)
        {
            //close enough to trigger it?
            if(Vector3.Distance(playerTransform.position, startingPosition) <= triggerLength)
                chasing = true;
                
            if(chasing)
            {
                if(!collidingWithPlayer)
                {   
                    //move towards player's position
                    UpdateMotor((playerTransform.position - transform.position).normalized);
                }
            }
            else
            {
                //move back to start
                UpdateMotor(startingPosition - transform.position);
            }
        }
        //not in range?
        else
        {
            //move back to start in this case as well as opposed to not moving
            UpdateMotor(startingPosition - transform.position);
            chasing = false;
        }
        
        //check for overlaps
        collidingWithPlayer = false;

        boxCollider.OverlapCollider(filter, hits);
        for(int i =0; i<hits.Length ; i++)
        {
            if (hits[i] == null) 
                continue;

            if (hits[i].tag == "Fighter" && hits[i].name == "Player")
            {
                collidingWithPlayer = true;
            }

            //clean up array once its been handled
            hits[i] = null;
        }
    }

    /* for right now we want to ignore this. I have to figure out how the gamemanager will work with the battle scene
    protected override void Death()
    {
        Destroy(gameObject);    //removes entire entity
        GameManager.instance.GrantXp(xpValue); //grant XP
        //floating text for xp granted.
        GameManager.instance.ShowText("+"+xpValue+" exp", 26, Color.magenta, transform.position, Vector3.up * 40, 1.0f);
    }
    */
}
