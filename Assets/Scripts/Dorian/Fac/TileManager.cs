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




    public struct Tile
    {
        public GameObject TileObject;
        public GameObject PlaceAbleObject;
        public bool CannotInteract;
        public bool isOccupied;
        public Tile(GameObject Tile,GameObject Placeable)
        {
            TileObject = Tile;
            PlaceAbleObject = Placeable;
            isOccupied = Placeable != null;
            CannotInteract = isOccupied;
        }

        public void Place(GameObject Placeableobject)
        {
            if(isOccupied) { return; }
            if(CannotInteract) { return; }
            PlaceAbleObject = Instantiate(Placeableobject, TileObject.transform.position+Vector3.up,Quaternion.identity,TileObject.transform);
            isOccupied = true;
            Debug.Log("is Placed");
        }

        public void Remove()
        {
            if (!isOccupied) { return; }
            if (CannotInteract) { return; }
            Destroy(PlaceAbleObject);
            isOccupied = false;
        }

        public void Rotate(float offset)
        {
            if(!isOccupied) { return; }
            if (CannotInteract) { return; }
            PlaceAbleObject.transform.eulerAngles += Vector3.up * offset;
        }
    }

}
