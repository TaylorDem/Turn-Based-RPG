using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : Enemy
{
    public float fireballSpeed = 2.5f;
    public float distance = 0.3f;
    public List<Transform> fireballs;

    private void Update()
    {
        for(int i=0; i < fireballs.Count; i++)
        {
            //rotate each one an angle of i*60 (0,60,120) degrees offset
            //we update the position of the fireballs by saying center point (boss) + new location on circle
            fireballs[i].position = transform.position + new Vector3(Mathf.Cos((Time.time + (i*120)) * fireballSpeed) * distance, Mathf.Sin((Time.time + (i*120)) * fireballSpeed) * distance, 0);
        }
    }
}
