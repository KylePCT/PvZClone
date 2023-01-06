using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlaceObjOnGrid : MonoBehaviour
{
    public Transform gridCellPrefab;
    public Transform cube;
    public Transform onMousePrefab;
    public Vector3 smoothMousePos;
    [SerializeField] private int gridHeight;
    [SerializeField] private int gridWidth;
    private Node[,] nodes;
    private Plane plane;

    private Vector3 mousePos;

    void Start()
    {
        CreateGrid();
    }

    private void Update()
    {
        GetMousePosOnGrid();
    }

    private void CreateGrid()
    {
        nodes = new Node[gridWidth, gridHeight];
        var name = 0;

        for (int i = 0; i < gridWidth; i++)
        {
            for (int j = 0; j < gridHeight; j++)
            {
                Vector3 worldPos = new Vector3(i, 0, j);
                Transform obj = Instantiate(gridCellPrefab, worldPos, Quaternion.identity);
                obj.name = "Cell " + name;
                nodes[i,j] = new Node(true, worldPos, obj);
                name++;
            }
        }
    }

    void GetMousePosOnGrid()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit hit;

        if (Physics.Raycast(ray, out hit))
        {
            mousePos = hit.point;
            mousePos = Vector3Int.RoundToInt(mousePos);
            mousePos.y = 0;
            smoothMousePos = mousePos;

            Debug.DrawRay(ray.origin, ray.direction * 20, Color.yellow);

            foreach (var node in nodes)
            {
                if (node.cellPos == mousePos && node.isPlaceable)
                {
                    if (Input.GetMouseButtonUp(0) && onMousePrefab != null)
                    {
                        node.isPlaceable = false;
                        onMousePrefab.GetComponent<ObjFollowsMouse>().isOnGrid = true;
                        onMousePrefab.position = node.cellPos + new Vector3(0, 0.5f, 0);
                        onMousePrefab = null;
                    }
                }
            }
        }
    }

    public void OnMouseClickOnUI()
    {
        if (onMousePrefab == null)
        {
            onMousePrefab = Instantiate(cube, mousePos, Quaternion.identity);
        }
    }
}

public class Node
{
    public bool isPlaceable;
    public Vector3 cellPos;
    public Transform obj;

    public Node(bool isPlaceable, Vector3 cellPos, Transform obj)
    {
        this.isPlaceable = isPlaceable;
        this.cellPos = cellPos;
        this.obj = obj;
    }
}