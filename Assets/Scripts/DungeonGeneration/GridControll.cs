using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GridControll : MonoBehaviour
{
    public Room room;
    [System.Serializable]
    public struct Grid
    {
        public int columns, rows;
        public float verticalOffset, horizontalOffset; 
    }
    public Grid grid;
    public GameObject gridTile;
    public List<Vector2> avalibletPoints = new List<Vector2>();
    private void Awake()
    {
        room = GetComponentInParent<Room>();
        grid.columns = room.wigth - 4;
        grid.rows= room.heigth - 4;
        GenerateGrid();
    }
    public void GenerateGrid()
    {
        grid.verticalOffset += room.transform.localPosition.y;
        grid.horizontalOffset += room.transform.localPosition.x;
        for (int y=0; y<grid.rows; y++)
        {
            for (int x=0; x<grid.columns; x++)
            {
                GameObject go = Instantiate(gridTile, transform);
                go.GetComponent<Transform>().position = new Vector2(x - (grid.columns - grid.horizontalOffset), y - (grid.rows-grid.verticalOffset));
                go.name = "X: " + x + ", Y: " + y;
                avalibletPoints.Add(go.transform.position);
                go.SetActive(false);
            }
        }
        GetComponentInParent<ObjectRoomSpawner>().InitialiseObjectSpawner();
    }

}
