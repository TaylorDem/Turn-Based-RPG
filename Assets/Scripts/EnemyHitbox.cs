using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyHitbox : Collidable
{
    //Damage
    public int damage;
    public int pushForce;

    protected override void OnCollide(Collider2D coll)
    {
        if (coll.name == "Player")
        {
            /*
            Damage dmg = new Damage{
                damageAmount = damage,
                origin = transform.position,
                pushForce = pushForce,
            };

            coll.SendMessage("ReceiveDamage", dmg);
            */
            //Here same thing. go to battle scene. eventually I want to make it so that if we get hit the enemy gets the first move.
            UnityEngine.SceneManagement.SceneManager.LoadScene("Battle");
        }
    }
}
