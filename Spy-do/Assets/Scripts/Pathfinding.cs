using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Pathfinding : MonoBehaviour
{
    [SerializeField] private Transform _startPos, _endPos; //player object
    private LineRenderer _lr;

    internal Grid GridComponent;
    public Vector3 CursorPos;

    void Awake()
    {
        GridComponent = GetComponent<Grid>();
        _lr = GetComponent<LineRenderer>();
    }

    void Update()
    {
        CursorPos = Camera.main.ScreenToWorldPoint(Input.mousePosition);

        FindPath(_startPos.position, CursorPos);

        if (GridComponent.Path != null)
        {
            _lr.positionCount = GridComponent.Path.Count;
            for (int i = 0; i < _lr.positionCount; i++)
            {
                _lr.SetPosition(i, GridComponent.Path[i].WorldPos);
            }
        }
    }

    //Calculates the shortest distance between two nodes without taking obstacles into consideration 
    private int GetDistance(Node nodeA, Node nodeB)
    {
        int xDistance = Mathf.Abs(nodeB.GridPosX - nodeA.GridPosX);
        int yDistance = Mathf.Abs(nodeB.GridPosY - nodeA.GridPosY);
        int sum = 0;

        if(xDistance > yDistance)
        {
            sum = 14 * yDistance + 10 * (xDistance - yDistance);
        }
        else
        {
            sum = 14 * xDistance + 10 * (yDistance - xDistance);
        }

        return sum;
    }

    private void RetracePath(Node startNodeIn, Node targetNodeIn)
    {
        List<Node> path = new List<Node>();
        Node currentNode = targetNodeIn;
        
        while(currentNode != startNodeIn)
        {
            path.Add(currentNode);
            currentNode = currentNode.Parent;
        }

        path.Add(currentNode);

        path.Reverse();
        GridComponent.Path = path;
    }

    private void FindPath(Vector2 startPosIn, Vector2 targetPosIn)
    {
        Node startNode = GridComponent.WorldToNodePoint(startPosIn);
        Node targetNode = GridComponent.WorldToNodePoint(targetPosIn);

        List<Node> openSet = new List<Node>();
        HashSet<Node> closedSet = new HashSet<Node>();

        openSet.Add(startNode);

        while(openSet.Count > 0)
        {
            Node currentNode = openSet[0]; 

            for(int i = 1; i < openSet.Count; i++)
            {
                if(openSet[i].FCost < currentNode.FCost || openSet[i].FCost == currentNode.FCost && openSet[i].HCost < currentNode.HCost)
                {
                    currentNode = openSet[i];
                }
            }

            openSet.Remove(currentNode);
            closedSet.Add(currentNode);

            if(currentNode == targetNode)
            {
                RetracePath(startNode, targetNode);
                return;
            }

            foreach (Node neighbour in GridComponent.GetNeighbourNodes(currentNode))
            {
                if (!neighbour.Walkable || closedSet.Contains(neighbour))
                {
                    continue;
                }

                int newMovementCostToNeighbour = currentNode.GCost + GetDistance(currentNode, neighbour);

                if (neighbour.GCost > newMovementCostToNeighbour || !openSet.Contains(neighbour))
                {
                    neighbour.GCost = newMovementCostToNeighbour;
                    neighbour.HCost = GetDistance(neighbour, targetNode);
                    neighbour.Parent = currentNode;

                    if(!openSet.Contains(neighbour))
                    {
                        openSet.Add(neighbour);
                    }
                }
            }
        }

        
    }
}
