using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TileManager : MonoBehaviour
{
    public static TileManager Instance;
    public Tile[,] TileMap;
    public Dictionary<GameObject, Vector2Int> GetTilePos = new Dictionary<GameObject, Vector2Int>();
    private void Awake()
    {
        if (Instance == null) Instance = this;
    }

    // Start is called before the first frame update
    void Start()
    {
        if (GridGenerator.CurrentTileset == null) Debug.Log("tileset is null");
        TileMap = new Tile[GridGenerator.CurrentTileset.GetLength(0), GridGenerator.CurrentTileset.GetLength(1)];
        for (int x = 0; x < TileMap.GetLength(0); x++)
        {
            for (int y = 0;y < TileMap.GetLength(1);y++)
            {
                TileMap[x,y] = new Tile(GridGenerator.CurrentTileset[x,y]);
                GetTilePos.Add(GridGenerator.CurrentTileset[x, y], new Vector2Int(x, y));
            }
        }

    }


    // Update is called once per frame
    void Update()
    {
        
    }

   
    public struct Tile
    {
        public GameObject TileObject;
        public GameObject PlaceAbleObject;

        bool isOccupied;
        public Tile(GameObject Tile)
        {
            TileObject = Tile;
            PlaceAbleObject = null;
            isOccupied = false;
        }

        public void Place(GameObject Placeableobject)
        {
            if(isOccupied) { return; }
            PlaceAbleObject = Instantiate(Placeableobject, TileObject.transform.position+Vector3.up,Quaternion.identity,TileObject.transform);
            isOccupied = true;
            Debug.Log("is Placed");
        }

        public void Remove()
        {
            if (!isOccupied) { return; }
            Destroy(PlaceAbleObject);
            isOccupied = false;
        }
    }

}
