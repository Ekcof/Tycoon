using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GetGridScript : MonoBehaviour
{
    public static GetGridScript Instance;
    private Transform greenFrame;
    private Grid grid;
    
    #region Singleton
    private void Start()
    {
        Instance = this;
        grid = GameObject.Find("Grid").GetComponent<Grid>();

    }
    #endregion

    public void SetObjectToGridCenter(Transform transformToGrid)
    {
        Vector3Int cellPosition = grid.WorldToCell(transformToGrid.position);
        transformToGrid.position = grid.GetCellCenterWorld(cellPosition);
    }
}
