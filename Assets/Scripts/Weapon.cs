using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Weapon : Collidable
{
    // Damage struct
    public int[] damagePoint = {1, 3, 6, 10, 15};
    public float[] pushForce = {2.0f, 2.2f, 2.5f, 2.8f, 3.2f};


    //upgrades!
    public int weaponLevel = 0;
    private SpriteRenderer spriteRenderer;

    //Swing
    private float cooldown = 0.3f;
    private float lastSwing;
    private Animator anim;

    private void Awake()
    {
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    protected override void Start()
    {
        base.Start();   //grabs box collider
        anim = GetComponent<Animator>();
    }

    protected override void Update()
    {
        base.Update();  //assigns hit

        if(Input.GetKeyDown(KeyCode.Space))
        {
            if(Time.time - lastSwing > cooldown)
            {
                lastSwing = Time.time;
                Swing();
            }
        }
    }

    private void Swing()
    {
        //just trigger the animation here.
        anim.SetTrigger("Swing");
    }

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.tag == "Fighter")
        {   
            //don't hit myself
            if (coll.name == "Player")
                return;

            /*create damage object and send it
            Damage dmg = new Damage{
                damageAmount = damagePoint[weaponLevel],
                origin = transform.position,
                pushForce = pushForce[weaponLevel],
            };
            

            coll.SendMessage("ReceiveDamage", dmg);
            */
            //instead now we want to load the battle scene if we attack a monster.
            if(UnityEngine.SceneManagement.SceneManager.GetActiveScene().name != "Battle")
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");

            //Debug.Log("collided with " + coll.name);
            
        }
    }

    public void UpgradeWeapon()
    {
        weaponLevel++;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    //sets weapon when loaded
    public void SetWeaponLevel(int level)
    {
        weaponLevel = level;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }

    public void ResetWeapon()
    {
        weaponLevel = 0;
        spriteRenderer.sprite = GameManager.instance.weaponSprites[weaponLevel];
    }
}
