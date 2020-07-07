using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Grid : MonoBehaviour
{
    public LayerMask UnwalkableMask;
    public Vector2 GridWorldSize;
    public float NodeRadius;
    public Node[,] GridStorage;
    [SerializeField] public List<Node> Path;

    [SerializeField] private Transform _player;
    private float _nodeDiameter;
    private int _gridSizeX, _gridSizeY; //amount of nodes fit within GridWorldSize

    void Awake()
    {
        _nodeDiameter = NodeRadius * 2;

        _gridSizeX = Mathf.RoundToInt(GridWorldSize.x / _nodeDiameter);
        _gridSizeY = Mathf.RoundToInt(GridWorldSize.y / _nodeDiameter);
    }

    void Start()
    {
        CreateGrid();
    }

    public List<Node> GetNeighbourNodes(Node targetNode_in)
    {
        List<Node> neighbourNodes_ = new List<Node>();

        for(int x = -1; x <= 1; x++)
        {
            for (int y = -1; y <= 1; y++)
            {
                if(x == 0 && y == 0)
                {
                    continue; 
                }

                int neighbourX = targetNode_in.GridPosX + x;
                int neighbourY = targetNode_in.GridPosY + y;

                if(neighbourX >= 0 && neighbourX < _gridSizeX && neighbourY >= 0 && neighbourY < _gridSizeY)
                {
                    neighbourNodes_.Add(GridStorage[neighbourX, neighbourY]);
                }
            }
        }

        return neighbourNodes_;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawWireCube(transform.position, new Vector3(GridWorldSize.x, GridWorldSize.y, 1));

        if(GridStorage != null)
        {
            Node playerNode = WorldToNodePoint(_player.position);
            foreach(Node n in GridStorage)
            {
                Gizmos.color = (n.Walkable) ? Color.white : Color.red;

                if(Path != null)
                {
                    if (Path.Contains(n))
                    {
                        Gizmos.color = Color.blue;
                    }
                }
               
                if (n == playerNode)
                {
                    Gizmos.color = Color.green;
                }

                Gizmos.DrawCube(n.WorldPos, Vector3.one * (_nodeDiameter - 0.1f));
            }
        }
    }

    private void CreateGrid()
    {
        GridStorage = new Node[_gridSizeX, _gridSizeY];
        Vector3 worldBottomLeft = transform.position - (Vector3.right * (GridWorldSize.x / 2)) - (Vector3.up * (GridWorldSize.y / 2));

        for(int x = 0; x < _gridSizeX; x++)
        {
            for(int y = 0; y < _gridSizeY; y++)
            {
                Vector3 worldPoint = worldBottomLeft + (Vector3.right * (x * _nodeDiameter + NodeRadius)) + (Vector3.up * (y * _nodeDiameter + NodeRadius));
                bool walkable = !(Physics2D.OverlapCircle(worldPoint, NodeRadius, UnwalkableMask));
                GridStorage[x, y] = new Node(walkable, worldPoint, x, y);
            }
        }
    }

    public Node WorldToNodePoint(Vector2 worldPosition_in)
    {
        float percentX = (worldPosition_in.x + GridWorldSize.x / 2) / GridWorldSize.x;
        float percentY = (worldPosition_in.y + GridWorldSize.y / 2) / GridWorldSize.y;

        Mathf.Clamp01(percentX);
        Mathf.Clamp01(percentY);

        int x = Mathf.RoundToInt((_gridSizeX - 1) * percentX);
        int y = Mathf.RoundToInt((_gridSizeY - 1) * percentY);

        return GridStorage[x, y];
    }
}
