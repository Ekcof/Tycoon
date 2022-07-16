using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class GetGridScript : MonoBehaviour
{
    public static GetGridScript Instance;
    private Transform player;
    private Transform greenFrame;
    private Grid grid;

    
    #region Singleton
    private void Start()
    {
        Instance = this;
        grid = GameObject.Find("Grid").GetComponent<Grid>();
        player = GameObject.Find("Cashier01").transform;
        if(player == null) { player = GameObject.Find("Player").transform;}
        Debug.Log(player.name);
    }
    #endregion

    public void SetObjectToGridCenter(Transform transformToGrid)
    {
        Vector3Int cellPosition = grid.WorldToCell(transformToGrid.position);
        transformToGrid.position = grid.GetCellCenterWorld(cellPosition);

    }

    public float GetZ()
    {
        return player.position.z;
    }
}
