using System.Collections;
using System.Collections.Generic;
using UnityEngine;

internal class PlaceObjOnGrid : MonoBehaviour
{
    public static PlaceObjOnGrid instance = null;
    private UIController uiController;
    
    [Header("Attributes")]
    public int gridHeight;
    public int gridWidth;

    [Header("Object References")]
    public Transform gridCellPrefab;
    public Transform selectedPlant;

    [Header("Dynamic Prefabs")]
    public Transform onMousePrefab;
    public Vector3 smoothMousePos;
    public Node[,] nodes;
    private Plane plane;

    [HideInInspector] public Vector3 mousePos;

    private void Awake()
    {
        if (instance == null) instance = this; //Assign instance.
        else if (instance != this) Destroy(gameObject); //If we already have this existing, we don't need another.
    }

    void Start()
    {
        uiController = UIController.instance;
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
                obj.transform.parent = transform;
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
                        uiController.currentlyHighlightedUIMember.PlaceAndBeginRecharging();
                        TraceBeans.Info("Plant: <" + onMousePrefab.name +  "> has been placed on Tile: <" + node.cellPos + ">.");

                        if (onMousePrefab.GetComponent<Plant_Offensive>() != null)
                        {
                            Plant_Offensive plant = onMousePrefab.GetComponent<Plant_Offensive>();
                            plant.isFiring = true;
                            plant.StartCoroutine("fireBullet");
                        }

                        else if (onMousePrefab.GetComponent<Plant_SunProducing>() != null) 
                        { 
                            Plant_SunProducing plant = onMousePrefab.GetComponent<Plant_SunProducing>();
                            plant.canGenerate = true;
                            plant.StartCoroutine("generateSun");
                        }

                        onMousePrefab = null;
                    }
                }
            }
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