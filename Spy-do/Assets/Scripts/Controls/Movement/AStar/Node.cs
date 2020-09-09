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

public class Node
{
    public int GCost;
    public int HCost;
    public int GridPosX;
    public int GridPosY;
    public bool Walkable;
    public Vector3 WorldPos;
    public Node Parent;

    public Node(bool walkableIn, Vector3 worldPosIn, int gridPosXIn, int gridPosYIn)
    {
        Walkable = walkableIn;
        WorldPos = worldPosIn;
        GridPosX = gridPosXIn;
        GridPosY = gridPosYIn;
    }

    public int FCost
    {
        get
        {
            return GCost + HCost;
        }
    }
}
