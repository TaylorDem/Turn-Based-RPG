using System.Collections;
using System.Collections.Generic;
using UnityEngine;
//using UnityEngine.SceneManagement;

public class Portal : Collidable
{
    //Door Sprites
    public Sprite openedDoor;
    private SpriteRenderer spriteRenderer;
    public string[] sceneNames; //we set this manually on the object for each scene this portal can take us to.

    public void Awake()
    {   
        //base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            //Open the door
            spriteRenderer.sprite = openedDoor;

            //save on portal jump
            GameManager.instance.SaveState();

            //change scene, for now its randomly selected from the list.
            string sceneName = sceneNames[Random.Range(0, sceneNames.Length)];  
            UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);
            //GameManager.instance.player.transform.position = GameObject.Find("SpawnPoint").transform.position;

        }
    }
}
