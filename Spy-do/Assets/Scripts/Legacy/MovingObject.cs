/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: ROB1N (Ivan Fomenko)
 * Email: iv_fomenko@bk.ru
 * Twitter: @ROB1N21806
 */
//Script source (tutorial): https://youtu.be/fURWEzpNPL8

using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class MovingObject : MonoBehaviour //Abstract class is called there because this script is not supposed to be directly included in the project
{
    public float moveTime_ = 3;
    public LayerMask blockingLayer_; //This variable is going to store the layers that should be checked on the collision

    private BoxCollider2D bc2D_;
    private Rigidbody2D rb2D_;
    //private float inverseMoveTime_;    Didn't really get what is the point of doing that, so I commented it for now 

    // Start is called before the first frame update
    protected virtual void Start() //It is "protected" because it has to ba accessed only from this and derived classes. It is "virtual" because it is required to be overriden within derived classes
    {
        bc2D_ = GetComponent<BoxCollider2D>();
        rb2D_ = GetComponent<Rigidbody2D>();
        //inverseMoveTime_ = 1f / moveTime_;
    }

    protected bool Move(int x, int y, out/* * */ RaycastHit2D hit_)
    /* 
       *The out is a keyword in C# which is used for the passing the arguments to methods as a reference type. It is generally used when a method returns multiple values.
       source: https://www.geeksforgeeks.org/out-parameter-with-examples-in-c-sharp/
    */
    {
        Vector2 start_ = transform.position; 
        Vector2 end_ = start_ + new Vector2(x, y);

        bc2D_.enabled = false; //Turning off the BoxCollider component before casting ray to make sure that the ray won't accidentaly hit the hitbox of the object itself
        hit_ = Physics2D.Linecast(start_, end_, blockingLayer_); //Casting a ray that is going to consider all objects being placed on the corresponding layer
        bc2D_.enabled = true; //Turning box collider component back on

        if (hit_.transform == null) //(If nothing was hit on the ray's way
        {
            StartCoroutine(Movement(end_)); //Guide about coroutines: https://docs.unity3d.com/Manual/Coroutines.html
            return true;
        }

        else return false; 
    }

    protected IEnumerator Movement (Vector3 end_) //This function gets a point of a destination as an argument 
    {
        float sqrRemainingDistance_ = (transform.position - end_).sqrMagnitude; //Calculates the distance between current position and destination and takes the sqr root out of it. It is claimed that taking root is computationally cheaper rather than not doing it

        while(sqrRemainingDistance_ > float.Epsilon) //This loop will continue until the distance is equal to zero
        {
            Vector3 newPosition_ = Vector3.MoveTowards(rb2D_.position, end_, moveTime_ * Time.deltaTime); //This vector calculates to what position to move per each call of a loop
            rb2D_.MovePosition(newPosition_); //Speaks for itself
            sqrRemainingDistance_ = (transform.position - end_).sqrMagnitude; //Recalculating sqr root of remaining distance
            yield return null; //Pauses function, waits for a frame update, then continues
        }
    }

    protected virtual void AttemptMove<T>(int x, int y)
        where T : Component
    {
        RaycastHit2D hit_;
        bool canMove_ = Move(x, y, out hit_);

        if (canMove_ == true) //If moving object was succesful, then we stop executing this function
            return;

        else //otherwise we get what exact component was encountered by the ray and pass this component as the argument to the OnCantMove()
        {
            T hitComponent_ = hit_.transform.GetComponent<T>();
            OnCantMove(hitComponent_);
        }
    }


    protected abstract void OnCantMove<T>(T component_) //This function should be specified and described in the inherited classes
        where T : Component;
}
