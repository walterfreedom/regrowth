using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;

public class Greenify : MonoBehaviour
{
    public GameObject GroundTilemap;
    public int range;
    public List<Tile> tilestoset;

    void Start()
    {
       

    }

    private void Update()
    {
        for (int x = (int)(transform.position.x - range); x <= transform.position.x + range; x++)
        {
            for (int y = (int)(transform.position.y - range); y <= transform.position.y + range; y++)
            {

                if (Vector2.Distance(new Vector2Int((int)transform.position.x,(int)transform.position.y), new Vector2(x, y)) <= range)
                {
                    GroundTilemap.GetComponent<Tilemap>().SetTile(new Vector3Int(x, y, 0), tilestoset[0]);
                    
                }
            }
        }
    }

}
