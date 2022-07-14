using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class GridPositionScript : MonoBehaviour
{
    private Tilemap tilemap;
    private Grid grid;
    // Start is called before the first frame update
    void Start()
    {
        grid = GameObject.Find("Grid").GetComponent<Grid>() ;
        GetTilePosition();
    }

    public Vector2 GetTilePosition()
    {
        Vector2 pos = transform.position;
        Vector3Int lpos = grid.WorldToCell(transform.position);
        transform.position = grid.CellToWorld(lpos);
        Debug.Log(lpos);
        return pos;
    }


    void Update()
    {
        
    }
}
