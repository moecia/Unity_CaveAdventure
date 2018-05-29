using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;

public class MapGenerator : MonoBehaviour {

    public int width;
    public int height;

    public string seed;
    public bool useRandomSeed;

    [Range(0, 100)]
    public int randomFillPercent;

    public int smoothness;

    int[,] map;

    private void Start()
    {
        GenerateMap();
    }

    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            GenerateMap();
        }
    }

    void GenerateMap()
    {
        map = new int[width, height];
        RandomFillMap();
        SmoothMap(smoothness);

        MeshGenerator m_meshGenerator = GetComponent<MeshGenerator>();
        m_meshGenerator.GenerateMesh(map, 1);
    }

    void RandomFillMap()
    {
        if (useRandomSeed)
        {
            seed = Time.time.ToString();
        }

        System.Random prng = new System.Random(seed.GetHashCode());

        for (int x = 0; x < width; x++)
        {
            for (int y = 0; y < height; y++)
            {
                // Make sure boundaries is solid
                if (x == 0 || x == width - 1 || y == 0 || y == height - 1)
                    map[x, y] = 1;
                else
                    map[x, y] = (prng.Next(0, 100) < randomFillPercent)? 1:0;
            }
        }
    }

    void SmoothMap(int smoothness)
    {
        if (smoothness < 1)
            smoothness = 1;

        for (int i = 0; i < smoothness; i++)
        {
            for (int x = 0; x < width; x++)
            {
                for (int y = 0; y < height; y++)
                {
                    int neighbourWallTiles = GetSurroundingWallCount(x, y);

                    // Conway's rule
                    if (neighbourWallTiles > 4)
                        map[x, y] = 1;
                    else if (neighbourWallTiles < 4)
                        map[x, y] = 0;
                }
            }
        }
    }

    int GetSurroundingWallCount(int gridX, int gridY)
    { 
        int wallCount = 0;
		for (int neighbourX = gridX - 1; neighbourX <= gridX + 1; neighbourX ++)
        {
			for (int neighbourY = gridY - 1; neighbourY <= gridY + 1; neighbourY ++)
            {
                // Element inside boundaries
				if (neighbourX >= 0 && neighbourX < width && neighbourY >= 0 && neighbourY < height)
                {
					if (neighbourX != gridX || neighbourY != gridY)
                    {
						wallCount += map[neighbourX, neighbourY];
					}
				}
                // Keep boundaries solid
				else
                {
					wallCount ++;
				}
			}
		}

		return wallCount;
	}

    void OnDrawGizmos()
    {
    //    if (map != null)
    //    {
    //        for (int x = 0; x < width; x++)
    //        {
    //            for (int y = 0; y < height; y++)
    //            {
    //                Gizmos.color = (map[x, y] == 1) ? Color.black : Color.white;
    //                Vector3 pos = new Vector3(-width / 2 + x + .5f, 0, -height / 2 + y + .5f);
    //                Gizmos.DrawCube(pos, Vector3.one);
    //            }
    //        }
    //    }
    }
}
