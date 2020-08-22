using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float MoveTime = 3;

    private Grid _gridComponent = null;
    private BoxCollider2D _bc2D = null;
    private Rigidbody2D _rb2D = null;
    public bool _isMoving = false;

    //test
    private Transform _player = null;

    // Start is called before the first frame update
    private void Start()
    {
        _isMoving = false;
        _gridComponent = FindObjectOfType<Grid>();
        _bc2D = GetComponent<BoxCollider2D>();
        _rb2D = GetComponent<Rigidbody2D>();
    }

    protected IEnumerator Movement(Vector3 end) //This function gets a point of a destination as an argument 
    {
        _isMoving = true;
        float sqrRemainingDistance_ = (transform.position - end).sqrMagnitude; //Calculates the distance between current position and destination and takes the sqr root out of it. It is claimed that taking root is computationally cheaper rather than not doing it

        while (sqrRemainingDistance_ > float.Epsilon) //This loop will continue until the distance is equal to zero
        {
            Vector3 newPosition_ = Vector3.MoveTowards(_rb2D.position, end, MoveTime * Time.deltaTime); //This vector calculates to what position to move per each call of a loop
            _rb2D.MovePosition(newPosition_); //Speaks for itself
            sqrRemainingDistance_ = (transform.position - end).sqrMagnitude; //Recalculating sqr root of remaining distance
            yield return null; //Pauses function, waits for a frame update, then continues
        }

        _isMoving = false;
    }

    protected bool Move(int x, int y, out/* * */ RaycastHit2D hitIn)
    /* 
       *The out is a keyword in C# which is used for the passing the arguments to methods as a reference type. It is generally used when a method returns multiple values.
       source: https://www.geeksforgeeks.org/out-parameter-with-examples-in-c-sharp/
    */
    {
        Vector2 start = transform.position;
        Vector2 end = start + new Vector2(x, y);

        _bc2D.enabled = false; //Turning off the BoxCollider component before casting ray to make sure that the ray won't accidentaly hit the hitbox of the object itself
        hitIn = Physics2D.Linecast(start, end); //Casting a ray that is going to consider all objects on the way
        _bc2D.enabled = true; //Turning box collider component back on

        if (hitIn.transform == null) //(If nothing was hit on the ray's way
        {
            StartCoroutine(Movement(end)); //Guide about coroutines: https://docs.unity3d.com/Manual/Coroutines.html
            return true;
        }

        else return false;
    }

    protected virtual void AttemptMove(int x, int y)
    {
        RaycastHit2D hit;
        bool canMove = Move(x, y, out hit);

        if (canMove == true) //If moving object was succesful, then we stop executing this function
        {
            return;
        }
    }

    private IEnumerator ExecuteMovement()
    {
        List<Node> currentPath = _gridComponent.Path;
        _isMoving = true;

        if(currentPath != null)
        {
            for(int i = 0; i < currentPath.Count - 1; i++)
            {
                //bool inLoop = true;
                //bool firstTime = true; //checks whether the condition below is executed for the first time or not

                int currentPosX = currentPath[i].GridPosX;
                int currentPosY = currentPath[i].GridPosY;

                int nextPosX = currentPath[i + 1].GridPosX;
                int nextPosY = currentPath[i + 1].GridPosY;

                Vector3 a = currentPath[i].WorldPos;
                Vector3 b = currentPath[i + 1].WorldPos;

                float horizontal = nextPosX - currentPosX;
                float vertical = nextPosY - currentPosY;

                //transform.position = Vector3.Lerp(a, b, Time.deltaTime * 5);
                transform.Translate(new Vector3(horizontal, vertical, 0f));

                yield return new WaitForSeconds(0.03f);
                //while (inLoop)
                //{
                //    if (!_isMoving)
                //    {
                //        if (firstTime)
                //        {
                //            StartCoroutine(Movement(new Vector3(horizontal, vertical, 0)));

                //            firstTime = false;
                //        }
                //        else
                //        {
                //            inLoop = false;
                //        }
                //    }
                //}
            }
        }

        currentPath = null;
        _isMoving = false;
    }

    // Update is called once per frame
    private void Update()
    {
        if(Input.GetKeyDown(KeyCode.DownArrow))
        {
            List<Node> currentPath = _gridComponent.Path;

            for (int i = 0; i < currentPath.Count; i++)
            {
                Debug.Log(i + ": " + currentPath[i].WorldPos);
            }
        }

        if (Input.GetMouseButtonDown(0) && !_isMoving)
        {
            StartCoroutine(ExecuteMovement());
        }
        else
        {
            int horizontal = 0;
            int vertical = 0;

            if (!_isMoving)
            {
                if (Input.GetKeyUp("w"))
                {
                    horizontal = 0;
                    vertical = 1;
                }
                else if (Input.GetKeyUp("a"))
                {
                    horizontal = -1;
                    vertical = 0;
                }
                else if (Input.GetKeyUp("s"))
                {
                    horizontal = 0;
                    vertical = -1;
                }
                else if (Input.GetKeyUp("d"))
                {
                    horizontal = 1;
                    vertical = 0;
                }
            }

            Vector2 start1 = transform.position;
            Vector2 end1 = start1 + new Vector2(horizontal, vertical);
            //Debug.DrawLine(start1, end1);
            //Debug.Log(start1);
            //Debug.Log(end1);

            if (horizontal != 0 || vertical != 0)
            {
                AttemptMove(horizontal, vertical);
            }
        }
    }
}
