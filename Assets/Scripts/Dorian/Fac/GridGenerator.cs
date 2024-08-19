using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Splines;

public class GridGenerator : MonoBehaviour
{
    public TileSettings[] tileSettings;
    public Texture2D Map;
    public Texture2D ColorPallet;
    
    public GameObject[] Tileset;
    [SerializeField] int X;
    [SerializeField] int Y;

    [SerializeField] bool GenerateWithMap;

    
    public static GameObject[,] CurrentTileset;

    public SplineContainer EnemyPath;
    private Vector2Int Spawnpos;
    private Vector2Int Goalpos;

    // Start is called before the first frame update
    void Createlevel()
    {
        CurrentTileset = new GameObject[X, Y];
        int index = 0;
        for (int x = 0; x < X; x++)
        {
            // check if x is even or odd

            if (x % 2 == 0)
            {
                index = 1;

            }
            else
            {
                index = 0;
            }

            for (int y = 0; y < Y; y++)
            {

                index++;
                if (index > Tileset.Length - 1) { index = 0; }
                CurrentTileset[x, y] = Instantiate(Tileset[index], new Vector3(x, 0, y), Quaternion.identity, transform);
            }
            index++;
        }
    }
    void GenerateLevel()
    {
        TileManager.Instance.TileMap = new TileManager.Tile[Map.width, Map.height];
        for (int x = 0; x < Map.width; x++)
        {
            for (int y = 0; y < Map.height; y++)
            {
                Color pixlelcolor = Map.GetPixel(x, y);

                foreach(TileSettings tilesettings in tileSettings)
                {
                    
                    if(!ColorPallet.GetPixel(tilesettings.PalletIndex,0).Equals(pixlelcolor)) { continue; }

                    GameObject Tile = Instantiate(tilesettings.Tile, new Vector3(x, 0, y),Quaternion.identity,transform);
                    GameObject PlaceAble = null;
                    if (tilesettings.PlaceAble != null)
                    {
                        PlaceAble = Instantiate(tilesettings.PlaceAble, new Vector3(x, 0.5f, y), Quaternion.identity, Tile.transform);
                    }
                    if(tilesettings.Type == TileSettings.TileType.Spawn){ Spawnpos = new Vector2Int(x, y); }
                    if (tilesettings.Type == TileSettings.TileType.Goal) { Goalpos = new Vector2Int(x, y); }
                    TileManager.Instance.TileMap[x, y] = new TileManager.Tile(Tile, PlaceAble);
                    TileManager.Instance.GetTilePos.Add(Tile, new Vector2Int(x, y));
                    break;
                }
            }
        }
    }
    void CreatePath()
    {
        TileManager tileManager = TileManager.Instance;
        bool isfinished = false;
        Vector2Int Lastpos = Spawnpos;
        
        adKnot(Lastpos);
        while(!isfinished)
        {
            bool foundpath = false;
            for (int x = Lastpos.x - 1; x < Lastpos.x +1; x++)
            {
                Debug.Log(x);
                if (x == Lastpos.x) { continue; }
                if (x < tileManager.TileMap.GetLength(0)) { continue; }
               
                Vector2Int currentpos = new Vector2Int(x, Lastpos.y);
                if (Goalpos == currentpos) 
                {
                    isfinished = true;
                    foundpath = true;
                    Debug.Log("found the goal");
                   // break;
                }
                Debug.Log("hello world");
                Debug.Log(tileManager.TileMap[currentpos.x, currentpos.y].TileObject.name);
                if (tileManager.TileMap[currentpos.x, currentpos.y].TileObject.tag == "Path")
                {
                    Lastpos += currentpos;
                    adKnot(Lastpos);
                    foundpath = true;
                    break;
                }
            }

            if (foundpath) { continue; }
            for (int y = Lastpos.y - 1; y < Lastpos.y + 1; y++)
            {
                if (y == Lastpos.y) { continue; }
                if (y < tileManager.TileMap.GetLength(1)) { continue; }

                Vector2Int currentpos = new Vector2Int(Lastpos.x, +y);
                if (Goalpos == currentpos)
                {
                    isfinished = true;
                    foundpath = true;
                    break;
                }

                if (tileManager.TileMap[currentpos.x, currentpos.y].TileObject.tag == "Path")
                {
                    Lastpos += currentpos;
                    adKnot(Lastpos);
                    foundpath = true;
                    break;
                }
            }
            if (!foundpath) 
            {
                Debug.Log("Cant Find Path");
                break; 
            }

            
            
        }
    }

    void adKnot(Vector2Int pos)
    {
        EnemyPath.Spline.Add(new BezierKnot(new Unity.Mathematics.float3(TileManager.Instance.TileMap[pos.x, pos.y].TileObject.transform.position + Vector3.up * 0.5f)));
    }
    void DestroyTileMap()
    {
        for (int x = 0; x < CurrentTileset.GetLength(0); x++)
        {
            for (int y = 0; y < CurrentTileset.GetLength(1); y++)
            {
                DestroyImmediate(CurrentTileset[x, y]);
            }
        }

        CurrentTileset = null;
    }

    private void Start()
    {
        if (GenerateWithMap) 
        { 
            GenerateLevel();
            CreatePath();
        }
        else { Createlevel(); }
    }

    [Serializable]
    public struct TileSettings
    {
        public string Name;
        public GameObject Tile;
        public GameObject PlaceAble;
        public enum TileType { Other, Spawn, Goal, Path }
        public TileType Type;
        public int PalletIndex;
    }
}
