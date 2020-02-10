//script source (tutorial): https://www.youtube.com/watch?v=whzomFgjT50 hero direction
//script source (tutorial): https://www.youtube.com/watch?v=whzomFgjT50 tilemap direction
//Responsible for the script: Ivan
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    float movementSpeed_;
    public Rigidbody2D rb;
    Vector2 movement_;
    Vector2 direction_;

    // Start is called before the first frame update
    public void Start()
    {
        movementSpeed_ = Storage.variables[0];
        Debug.Log("movementSpeed_: " + movementSpeed_);        
    }   

    // Update is called once per frame
    void Update()
    {
        movement_.x = Input.GetAxisRaw("Horizontal");
        movement_.y = Input.GetAxisRaw("Vertical");

        if (movement_.x == 0 && movement_.y == 0)
        {
            direction_ = new Vector2(0f, 0f);
        }
        else if (movement_.x != 0 || movement_.y != 0)
        {
            GetMovementDirection();
        }
    }

    private void GetMovementDirection()
    {
        if (movement_.x < 0)
        {
            direction_ = new Vector2(-0.5f, 0f);
        }

        if (movement_.x > 0)
        {
            direction_ = new Vector2(0.5f, 0f);
        }

        if (movement_.y > 0)
        {
            direction_ = new Vector2(0f, 0.5f);
        }

        if (movement_.y < 0)
        {
            direction_ = new Vector2(0f, -0.5f);
        }
    }

    private void FixedUpdate()
    {
        rb.MovePosition(rb.position + direction_);
    }
}
