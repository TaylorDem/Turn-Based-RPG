using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static GameManager instance; //static means can be called directly from anywhere.
    private void Awake()        //run once when initialized
    {
        //if we end up in a scene where the GameManager already exists (meaning its the second Awake() call)
        if (GameManager.instance != null)   
        {
            //Destroy ourself and don't need to initialize anything (Keeps the old one in place)
            Destroy(gameObject);    
            return;
        }

        //delete old data, not needed now.
        //if(delete) PlayerPrefs.DeleteKey("SaveState");

        //assign the gamemanager object to itself so it can be called.
        instance = this;

        //this is an event. when a scene is loaded, add the load function to the list of stuff to do
        SceneManager.sceneLoaded += LoadState;  
        SceneManager.sceneLoaded += OnSceneLoad;

        //this creates the GameManager object on the scene thats been loaded, which triggers another Awake()
        DontDestroyOnLoad(gameObject);   
        
    }

    //resources
    public List<Sprite> playerSprites;  //list of sprites
    public List<Sprite> weaponSprites;  //list of weapons
    public List<int> weaponPrices;      //gold to upgrade weapon
    public List<int> xpTable;           //xp drops

    //references
    public Player player;
    public FloatingTextManager floatingTextManager;
    public Weapon weapon;
    public RectTransform hitPointBar;
    public Animator deathMenuAnim;

    //logic
    public int money;
    public int xp;
    //public bool delete = false;    //toggles whether or not to delete save data, no longer needed.

    //put another instance of the show function for the floating text here since gameManager is staying with us always
    public void ShowText(string msg, int fontSize, Color color, Vector3 position, Vector3 motion, float duration)
    {
        //simply calls the other show function we have, but we throw all the parameters so that it can be called from anywhere
        floatingTextManager.Show(msg, fontSize, color, position, motion, duration);
    }
    public bool TryUpgradeWeapon()
    {
        //are we max level?
        if(weaponPrices.Count <= weapon.weaponLevel)
            return false;

        //check money
        if(money >= weaponPrices[weapon.weaponLevel])
        {   
            //reduce money
            money -= weaponPrices[weapon.weaponLevel];
            weapon.UpgradeWeapon();
            return true;
        }
        
        return false;
    }
    /*
    //HealthBar
    public void OnHitPointChange()
    {
        float ratio = (float)player.hitPoint / (float)player.maxHitPoint;
        hitPointBar.localScale = new Vector3( 1, ratio, 1);
    }
    */
    public int GetCurrentLevel()
    {
        int r = 0;
        int add = 0;

        while(xp >= add)
        {
            add += xpTable[r];
            r++;

            //max level
            if(r == xpTable.Count)  
                return r;
        }

        return r;
    }
    // auto levels to a particular level
    public int GetXpToLevel(int level)
    {
        int r = 0;
        int xp = 0;

        while (r<level)
        {   
            xp += xpTable[r];
            r++;
        }
        return xp;
    }
    public void GrantXp(int experience)
    {
        int currLevel = GetCurrentLevel();
        xp += experience;
        if (currLevel < GetCurrentLevel())
            LevelUp();
    }
    public void LevelUp()
    {
        //Debug.Log("Level Up");
        //player.LevelUp();
    }

    //Death Menu
    public void Respawn()
    {
        deathMenuAnim.SetTrigger("Hide");   //hide the deathscreen
        SceneManager.LoadScene("Main");     //reload main      
        player.Respawn();                   //this function heals player and re-enables movement.
    }
    public void ResetProgress()
    {
        money = 0;
        xp = 0;
        weapon.ResetWeapon();
        //player.ResetLevel();
    }

    //save
    /*  order data is saved in:
     * INT preferedSkin
     * INT money
     * INT xp
     * INT weaponLevel    
     */
    public void SaveState()
    {
        string s = "";

        s += "0" + "|";     //put them in order per above
        s += money.ToString() + "|";
        s += xp.ToString() + "|";
        s += weapon.weaponLevel.ToString();

        PlayerPrefs.SetString("SaveState", s);
        //PlayerPrefs.Save();       //not necessary but works with it in there?
        Debug.Log("SAVE");
    }
    public void LoadState(Scene s, LoadSceneMode mode)
    {
        SceneManager.sceneLoaded -= LoadState;

        //check if a save exists
        if(!PlayerPrefs.HasKey("SaveState"))
            return;
        // take string "0|25|300|0" and split into string array
        string[] data = PlayerPrefs.GetString("SaveState").Split('|');

        // Change Skin

        // Change Gold
        money = int.Parse(data[1]);

        // Change Level
        xp = int.Parse(data[2]);
        //if(GetCurrentLevel() != 1)
        //    player.SetLevel(GetCurrentLevel());

        // Change Weapon
        weapon.weaponLevel = int.Parse(data[3]);
        weapon.SetWeaponLevel(weapon.weaponLevel);

        //Debug.Log("LOAD");
    }

    //Load at the spawnpoint for each scene.
    public void OnSceneLoad(Scene s, LoadSceneMode mode)
    {  
        //if we don't load up battle scene
        if(SceneManager.GetActiveScene().name != "Battle" && player != null)
            player.transform.position = GameObject.Find("SpawnPoint").transform.position;
    }

    public void QuitButton()
    {
        Application.Quit();
    }
}
