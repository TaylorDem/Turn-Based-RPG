using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Mover : MonoBehaviour  //abstract means it can't stand alone. polymorphism
{
    protected BoxCollider2D boxCollider;
    protected Vector3 moveDelta;
    protected RaycastHit2D hit;   //gotta figure out what this type is
    public float ySpeed = 0.75f;
    public float xSpeed = 1.0f;

    private bool facingRight = true;

    // Start is called before the first frame update
    protected virtual void Start()
    {
        boxCollider = GetComponent<BoxCollider2D>();
    }

    protected virtual void UpdateMotor(Vector3 input)
    {
        //assign to move vector variable
        moveDelta = new Vector3(input.x * xSpeed, input.y * ySpeed, 0);
        //example debug
        //Debug.Log(x+y);
        
        //swap sprite direction to match movement
        if (moveDelta.x > 0 && !facingRight)
            Flip(); //right
        else if (moveDelta.x < 0 && facingRight)
            Flip(); //left

        //add push vector if any
        //moveDelta += pushDirection;

        //reduce push force based off recovery speed. "Lerping to get to zero"
        //pushDirection = Vector3.Lerp(pushDirection, Vector3.zero, pushRecoverySpeed);



        //check for hits with anything on the layers specified before moving on Y axis
        hit = Physics2D.BoxCast(transform.position,
                                    boxCollider.size,
                                    0, 
                                    new Vector2(0,moveDelta.y), 
                                    Mathf.Abs(moveDelta.y * Time.deltaTime), 
                                    LayerMask.GetMask("Character", "Blocking")
                                    );                           
        //if nothing got hit
        if (hit.collider == null)
        {
            //actually moving
            transform.Translate(0, moveDelta.y * Time.deltaTime, 0);
        }

        //Now check X axis
        hit = Physics2D.BoxCast(transform.position,
                                    boxCollider.size,
                                    0,
                                    new Vector2(moveDelta.x, 0), 
                                    Mathf.Abs(moveDelta.x * Time.deltaTime), 
                                    LayerMask.GetMask("Character", "Blocking")
                                    );
        if (hit.collider == null)
        {
            //actually moving
            transform.Translate(moveDelta.x * Time.deltaTime, 0, 0);
        }
    }

    private void Flip()
    {
        Vector3 currentScale = transform.localScale;
        currentScale.x *= -1;
        transform.localScale = currentScale;
        facingRight = !facingRight;
    }

}
