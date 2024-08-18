using System;
using System.Collections;
using System.Collections.Generic;
using UnityEditorInternal;
using UnityEngine;

public class GridGenerator : MonoBehaviour
{
    public TileSettings[] tileSettings;
    public Texture2D Map;

    public GameObject[] Tileset;
    [SerializeField] int X;
    [SerializeField] int Y;

    [SerializeField] bool GenerateWithMap;

    
    public static GameObject[,] CurrentTileset;


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

    private void Awake()
    {
        if (GenerateWithMap) { GenerateLevel(); }
        else { Createlevel(); }
    }

    [Serializable]
    public struct TileSettings
    {
        public string Name;
        public GameObject Tile;
        public GameObject PlaceAble;

        public Color Color;
    }
}
