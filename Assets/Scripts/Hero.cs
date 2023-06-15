using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hero : MonoBehaviour
{

    private Animator anim;

    // Start is called before the first frame update
    void Start()
    {
        anim = GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    //eventually this will be passed the enemy chosen.
    public void Attack()
    {
        anim.SetTrigger("Attack");  //Works so far!
    }
}
