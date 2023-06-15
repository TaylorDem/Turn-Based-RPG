using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Mover
{
    private SpriteRenderer spriteRenderer;
    private bool isAlive = true;

    protected override void Start()
    {
        base.Start();
        spriteRenderer = GetComponent<SpriteRenderer>();
    }

    /*
    protected override void ReceiveDamage(Damage dmg)
    {
        if(isAlive)
        {
            base.ReceiveDamage(dmg);
            GameManager.instance.OnHitPointChange();
        }
    }
    */

    private void Death()
    {
        GameManager.instance.deathMenuAnim.SetTrigger("Show");
        isAlive = false;
    }

    private void FixedUpdate()
    {
        //pull inputs
        float x = Input.GetAxisRaw("Horizontal");
        float y = Input.GetAxisRaw("Vertical");
        if(isAlive)
            UpdateMotor(new Vector3(x,y,0));
    }

    public void SwapSprite(int skinId)
    {
        spriteRenderer.sprite = GameManager.instance.playerSprites[skinId];
    }

    /*
    public void LevelUp()
    {
        maxHitPoint++;
        hitPoint = maxHitPoint;
        GameManager.instance.OnHitPointChange();
        GameManager.instance.ShowText("Level Up!", 25, Color.green, transform.position, Vector3.up * 15, 2.0f);
    }

    public void SetLevel(int level)
    {
        for(int i=1;i<level;i++)
            LevelUp();
    }

    public void ResetLevel()
    {
        maxHitPoint = 5;
        GameManager.instance.OnHitPointChange();
    }

    public void Heal(int healingAmount)
    {
        //don't do anything if health is full
        if (hitPoint == maxHitPoint)
            return;

        hitPoint += healingAmount;
        //if over the cap correct it
        if (hitPoint > maxHitPoint)
            hitPoint = maxHitPoint;
        GameManager.instance.OnHitPointChange();
        GameManager.instance.ShowText("+" + healingAmount.ToString() + " hp", 25, Color.green, transform.position, Vector3.up * 30, 1.0f);
    }
    */
    public void Respawn()
    {
        //Heal(maxHitPoint);
        isAlive = true;
        //lastImmune = Time.time; //makes it so you don't get hit immediately after respawn.
        //pushDirection = Vector3.zero; // and don't get pushed.
        
    }

}
