using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MapGenerator : MonoBehaviour
{
    public List<Map> maps;
    public int mapIndex = 0;

    public Transform tilePrefab;
    public Transform obstaclePrefab;
    public Transform navmeshFloor;
    public Transform navmeshMaskPrefab;

    public Vector2 maximumMapSize;

    [Range(0,1)]
    public float outlinePercentage;

    public float tileSize;

    List<Coordinate> allTileCoords;
    Queue<Coordinate> shuffledTileCoords;
    Queue<Coordinate> openTileCoords;

    Map currentMap;

    public Transform[,] tileMap;

    private void Start()
    {
        GenerateMap();
    }

    public void GenerateMap()
    {
        currentMap = maps[mapIndex];
        tileMap = new Transform[currentMap.mapSize.x, currentMap.mapSize.y];
        GetComponent<BoxCollider>().size = new Vector3 (currentMap.mapSize.x * tileSize, .05f, currentMap.mapSize.y * tileSize);
        //Generate Coords
        allTileCoords = new List<Coordinate>();
        for (int x = 0; x < currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < currentMap.mapSize.y; y++)
            {
                allTileCoords.Add(new Coordinate(x, y));
            }
        }
        shuffledTileCoords = new Queue<Coordinate>(Utility.ShuffleArray (allTileCoords.ToArray(), currentMap.seed));

        //Map holder obj
        string holderName = "Generated Map";

        if (transform.Find(holderName) != null)
        {
            Destroy(transform.Find(holderName).gameObject);
        }

        Transform mapHolder = new GameObject (holderName).transform;
        mapHolder.parent = transform;

        //Create Tiles
        for (int x = 0; x < currentMap.mapSize.x; x++)
        {
            for (int y = 0; y < currentMap.mapSize.y; y++)
            {
                Vector3 tilePos = CoordToPosition(x, y);
                Transform newTile = Instantiate(tilePrefab, tilePos, Quaternion.Euler(Vector3.right * 90));
                newTile.localScale = Vector3.one * (1 - outlinePercentage) * tileSize;

                newTile.parent = mapHolder;
                tileMap[x, y] = newTile;
            }
        }

        //Create Obstacles
        bool[,] obstacleMap = new bool[(int)currentMap.mapSize.x, (int)currentMap.mapSize.y];
        List<Coordinate> allOpenCoords = new List<Coordinate>(allTileCoords);
        int currentObstacleCount = 0;
        int obstacleCount = (int)(currentMap.mapSize.x * currentMap.mapSize.y * currentMap.obstaclePercent); 
        for (int i = 0; i < obstacleCount; i++)
        {
            Coordinate randomCoord = GetRandomCoord();
            obstacleMap[randomCoord.x, randomCoord.y] = true;
            currentObstacleCount++;

            if (randomCoord != currentMap.mapCenter && MapIsFullyAccessible(obstacleMap, currentObstacleCount))
            {
                Vector3 obstaclePosition = CoordToPosition(randomCoord.x, randomCoord.y);

                Transform newObstacle = Instantiate(obstaclePrefab, obstaclePosition, Quaternion.identity) as Transform;
                newObstacle.localScale = Vector3.one * (1 - outlinePercentage) * tileSize;
                newObstacle.parent = mapHolder;

                allOpenCoords.Remove(randomCoord);
            }
            else
            {
                obstacleMap[randomCoord.x, randomCoord.y] = false;
                currentObstacleCount--;
            }
        }

        openTileCoords = new Queue<Coordinate>(Utility.ShuffleArray(allOpenCoords.ToArray(), currentMap.seed));

        //Create Navmesh Masks
        Transform maskLeft = Instantiate(navmeshMaskPrefab, Vector3.left * (currentMap.mapSize.x + 1), Quaternion.identity) as Transform;
        maskLeft.parent = mapHolder;
        maskLeft.localScale = new Vector3(1, 1, currentMap.mapSize.y) * tileSize;

        Transform maskRight = Instantiate(navmeshMaskPrefab, Vector3.right * (currentMap.mapSize.x + 1), Quaternion.identity) as Transform;
        maskRight.parent = mapHolder;
        maskRight.localScale = new Vector3(1, 1, currentMap.mapSize.y) * tileSize;

        Transform maskTop = Instantiate(navmeshMaskPrefab, Vector3.forward * (currentMap.mapSize.y + 1), Quaternion.identity) as Transform;
        maskTop.parent = mapHolder;
        maskTop.localScale = new Vector3(currentMap.mapSize.y, 1, 1) * tileSize;

        Transform maskBottom = Instantiate(navmeshMaskPrefab, Vector3.back * (currentMap.mapSize.y + 1), Quaternion.identity) as Transform;
        maskBottom.parent = mapHolder;
        maskBottom.localScale = new Vector3(currentMap.mapSize.y, 1, 1) * tileSize;

        navmeshFloor.localScale = new Vector3(currentMap.mapSize.x / 2, 1, currentMap.mapSize.y / 2) / tileSize;
    }

    Vector3 CoordToPosition(int x, int y)
    {
        return new Vector3(-currentMap.mapSize.x / 2  + x, 0, -currentMap.mapSize.y / 2 + y) * tileSize;
    }

    public Coordinate GetRandomCoord()
    {
        Coordinate randomCoord = shuffledTileCoords.Dequeue();
        shuffledTileCoords.Enqueue(randomCoord);
        return randomCoord;
    }

    public Transform GetRandomOpenTIle()
    {
        Coordinate randomCoord = openTileCoords.Dequeue();
        openTileCoords.Enqueue(randomCoord);
        return tileMap[randomCoord.x, randomCoord.y];
    }

    bool MapIsFullyAccessible(bool[,] obstacleMap, int currentObstacleCount)
    {
        // flood fill
        bool[,] mapFlags = new bool[obstacleMap.GetLength(0), obstacleMap.GetLength(1)];
        Queue<Coordinate> queue = new Queue<Coordinate>();
        queue.Enqueue(currentMap.mapCenter);
        mapFlags[currentMap.mapCenter.x, currentMap.mapCenter.y] = true;

        int accessibleTileCount = 1;

        while (queue.Count > 0)
        {
            Coordinate tile = queue.Dequeue();

            for (int x = -1; x <= 1; x++)
            {
                for (int y = -1; y <= 1; y++)
                {
                    int neighbourX = tile.x + x;
                    int neighbourY = tile.y + y;
                    if (x == 0 || y == 0)
                    {
                        if 
                            (neighbourX >= 0 && neighbourX < obstacleMap.GetLength(0) && 
                            neighbourY >= 0 && neighbourY < obstacleMap.GetLength(1))
                        {
                            if (!mapFlags[neighbourX, neighbourY] && !obstacleMap[neighbourX, neighbourY])
                            {
                                mapFlags[neighbourX, neighbourY] = true;
                                queue.Enqueue(new Coordinate(neighbourX, neighbourY));
                                accessibleTileCount++;
                            }
                        }
                    }
                }
            }
        }

        int targetAccessibleTileCount = (int)currentMap.mapSize.x * (int)currentMap.mapSize.y - currentObstacleCount;
        return targetAccessibleTileCount == accessibleTileCount;

    }

    [System.Serializable]
    public struct Coordinate
    {
        public int x;
        public int y;

        public Coordinate(int _x, int _y)
        {
            x = _x;
            y = _y;
        }

        public static bool operator == (Coordinate c1, Coordinate c2)
        {
            return c1.x == c2.x && c1.y == c2.y;
        }

        public static bool operator != (Coordinate c1, Coordinate c2)
        {
            return !(c1 == c2);
        }
    }

    [System.Serializable]
    public class Map
    {
        public Coordinate mapSize;

        [Range(0, 1)]
        public float obstaclePercent;
        public int seed;

        public float minimumObstacleHeight;
        public float maximumObstacleHeight;

        public Color foregroundColor;
        public Color backgroundColor;

        public Coordinate mapCenter
        {
            get
            {
                return new Coordinate(mapSize.x/2, mapSize.y/2);
            }
        }
    }

    public void GenerateNextLevel()
    {
        mapIndex++;
        if (mapIndex >= maps.Count)
        {
            mapIndex = 0;
        }

        GenerateMap();
    }
}
