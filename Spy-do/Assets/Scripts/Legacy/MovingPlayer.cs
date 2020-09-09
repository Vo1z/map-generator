/*
 * Sirex production code:
 * Project: Spy-Do
 * Author: ROB1N (Ivan Fomenko)
 * Email: iv_fomenko@bk.ru
 * Twitter: @ROB1N21806
 */

using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class MovingPlayer : MovingObject
{
    private Animator animator_;

    // Start is called before the first frame update
    protected override void Start()
    {
        animator_ = GetComponent<Animator>();
        base.Start();
    }

    protected override void AttemptMove<T>(int x, int y)
    {
        base.AttemptMove<T>(x, y);

        //RaycastHit2D hit_;
    }

    protected override void OnCantMove<T>(T component_)
    {
        throw new System.NotImplementedException();
    }

    // Update is called once per frame
    void Update()
    {
        int horizontal_ = 0;
        int vertical_ = 0;

        if (Input.GetKeyUp("w"))
        {
            horizontal_ = 0;
            vertical_ = 1;
        }
        else if (Input.GetKeyUp("a"))
        {
            horizontal_ = -1;
            vertical_ = 0;
        }
        else if (Input.GetKeyUp("s"))
        {
            horizontal_ = 0;
            vertical_ = -1;
        }
        else if (Input.GetKeyUp("d"))
        {
            horizontal_ = 1;
            vertical_ = 0;
        }

        if (horizontal_ != 0 || vertical_ != 0)
        {
            AttemptMove<TilemapCollider2D>(horizontal_, vertical_);
        }
    }


}
