using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Fighter : MonoBehaviour
{
    public int hitPoint = 10;
    public int maxHitPoint = 10;
    public float pushRecoverySpeed = 0.16f;

    // Immunity
    protected float immuneTime = 0.5f;
    protected float lastImmune;

    // Push
    protected Vector3 pushDirection;

    //Receive damage and die are needed.
    protected virtual void ReceiveDamage(Damage dmg)
    {
        if (Time.time - lastImmune > immuneTime)
        {
            lastImmune = Time.time;
            hitPoint -= dmg.damageAmount;
            pushDirection = (transform.position - dmg.origin).normalized * dmg.pushForce;

            //floating text
            if(transform.parent.name != "Crates")   //don't want damage numbers for crates.
                GameManager.instance.ShowText(dmg.damageAmount.ToString(), 24, Color.red, transform.position, Vector3.zero, 0.5f);

            if (hitPoint <= 0)
            {
                hitPoint = 0;
                Death();
            }
        }
    }

    protected virtual void Death()
    {

    }
}
