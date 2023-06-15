using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FightManager : MonoBehaviour
{
    public Transform cursor;
    private int selection = 0;
    /*  order of positions in list:
        fight | item | defend | flee
    */
    public List<Transform> availPositions;

    public List<Hero> teammates;
    public List<Baddies> enemies;

    public void Start()
    {   //we have to destroy the GameManager, then include a copy of it in each scene outside of the battle scene.
        Destroy(GameObject.Find("GameManager"));
    }

    public void Update()
    {
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        //push to go right
        if(x>0)
        {
            //if on fight
            if(cursor.position == availPositions[0].position) 
            {
                cursor.position = availPositions[1].position; 
                selection = 1;
            }
            //if on defend
            if(cursor.position == availPositions[2].position)
            {
                cursor.position = availPositions[3].position;
                selection = 3;
            }  
        }
        //push to go left
        if(x<0)
        {
            //if on item
            if(cursor.position == availPositions[1].position) 
            {
                cursor.position = availPositions[0].position; 
                selection = 0;
            }
            //if on flee
            if(cursor.position == availPositions[3].position) 
            {
                cursor.position = availPositions[2].position;
                selection = 2;
            }
        }
        //push up
        if(y>0)
        {
            //if on defend
            if(cursor.position == availPositions[2].position) 
            {
                cursor.position = availPositions[0].position; 
                selection = 0;
            }
            //if on flee
            if(cursor.position == availPositions[3].position) 
            {
                cursor.position = availPositions[1].position;  
                selection = 1;
            }
        }
        //push down
        if(y<0)
        {
            //if on item
            if(cursor.position == availPositions[1].position) 
            {
                cursor.position = availPositions[3].position; 
                selection = 3;
            }
            //if on fight
            if(cursor.position == availPositions[0].position) 
            {
                cursor.position = availPositions[2].position;
                selection = 2;
            }
        }

        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("selected input: " + availPositions[selection].name);
            if(selection == 3)//flee
            {
                //will have to make this more significant later on. 
                UnityEngine.SceneManagement.SceneManager.LoadScene("Dungeon1");
            }
            if(selection == 0)//fight
            {
                teammates[0].Attack();  //basic enough for now I hope.
            }
        }
    }

}
