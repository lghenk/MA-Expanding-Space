using System.Collections; 
using System.Collections.Generic; 
using UnityEngine;
using System;
using UnityEngine.Events;

public class BoardCreator : MonoBehaviour {
    public enum TileType {
        Wall,
        Floor,
        Void,
        Vertical,
        HorizontalLeft,
        HorizontalRight,
        VerticalTop,
        VerticalBottom,
        Corner_left_up,
        Corner_left_down,
        Corner_right_up,
        Corner_right_down
    }

    public int columns = 100;
    public int rows = 100;
    public IntRange numRooms = new IntRange(15, 20);
    public IntRange roomWidth = new IntRange(3, 10);
    public IntRange roomHeight = new IntRange(3, 10);
    public IntRange corridorLength = new IntRange(6, 10);
    
    public GameObject[] wallTiles;
    public GameObject[] outerWallTiles;

    [Header("Gameplay Object")]
    public GameObject player;
    public GameObject rocket;

    [Header("Loot variables")]
    public int numChest = 15;
    public int numStomes = 5;
    public GameObject stoneGO;
    public GameObject chestGO;

    public List<GameObject> chests = new List<GameObject>();
    public List<GameObject> stones = new List<GameObject>();
    public List<Vector2> placeAble = new List<Vector2>();

    [Header("Floor Pieces")]
    public GameObject[] floorTiles;

    [Header("Corner Wall Pieces")]
    public GameObject[] CornerRightUp;
    public GameObject[] CornerRightDown;
    public GameObject[] CornerLeftDown;
    public GameObject[] CornerLeftUp;

    [Header("Straight Wall Pieces")]
    public GameObject[] wallNorth;
    public GameObject[] wallSouth;
    public GameObject[] wallEast;
    public GameObject[] wallWest;

    private TileType[][] tiles;
    private Room[] rooms;
    private Corridor[] corridors;
    private GameObject boardHolder;

    [Header("Done Loading Event")]
    public UnityEvent loaded = new UnityEvent();

    private Vector3 rocketPos;
    private Vector3 playerPos;

    void Start() {
        // Create the board holder.
        boardHolder = new GameObject("BoardHolder");

        SetupTilesArray();

        CreateRoomsAndCorridors();

        SetTilesValuesForRooms();
        SetTilesValuesForCorridors();
        CorrectWallTiles();

        InstantiateTiles();
        InstantiateOuterWalls();

        SpawnLoots();

        loaded.Invoke();
    }

    public Vector3 getRocketPos() {
        return rocketPos;
    }

    void SetupTilesArray() {
        tiles = new TileType[columns][];

        for (int i = 0; i < tiles.Length; i++) {
            tiles[i] = new TileType[rows];
        }
    }



    void CreateRoomsAndCorridors() {

        rooms = new Room[numRooms.Random];
        corridors = new Corridor[rooms.Length - 1];

        rooms[0] = new Room();
        corridors[0] = new Corridor();

        rooms[0].SetupRoom(roomWidth, roomHeight, columns, rows);

        corridors[0].SetupCorridor(rooms[0], corridorLength, roomWidth, roomHeight, columns, rows, true);

        for (int i = 1; i < rooms.Length; i++) {
            // Create a room.
            rooms[i] = new Room();

            rooms[i].SetupRoom(roomWidth, roomHeight, columns, rows, corridors[i - 1]);

            if (i < corridors.Length) {
                corridors[i] = new Corridor();

                corridors[i].SetupCorridor(rooms[i], corridorLength, roomWidth, roomHeight, columns, rows, false);
            }

            if (i == Mathf.Floor(rooms.Length * .5f)) {
                rocketPos = new Vector3(rooms[i].xPos + 1, 0, rooms[i].yPos + 2);
                playerPos = new Vector3(rooms[i].xPos, 0, rooms[i].yPos);
                Instantiate(rocket, rocketPos, Quaternion.Euler(90, 0, 0));
                Instantiate(player, playerPos, Quaternion.Euler(90,0,0));
            }
        }

    }


    void SetTilesValuesForRooms() {
        // Go through all the rooms...
        for (int i = 0; i < rooms.Length; i++) {
            Room currentRoom = rooms[i];

            for (int j = 0; j < currentRoom.roomWidth; j++) {
                int xCoord = currentRoom.xPos + j;

                for (int k = 0; k < currentRoom.roomHeight; k++) {
                    int yCoord = currentRoom.yPos + k;

                    tiles[xCoord][yCoord] = TileType.Floor;
                }
            }
        }
    }


    void SetTilesValuesForCorridors() {
        // Go through every corridor...
        for (int i = 0; i < corridors.Length; i++) {
            Corridor currentCorridor = corridors[i];

            for (int j = 0; j < currentCorridor.corridorLength; j++) {
                int xCoord = currentCorridor.startXPos;
                int yCoord = currentCorridor.startYPos;

                switch (currentCorridor.direction) {
                    case Direction.North:
                        yCoord += j;
                        break;
                    case Direction.East:
                        xCoord += j;
                        break;
                    case Direction.South:
                        yCoord -= j;
                        break;
                    case Direction.West:
                        xCoord -= j;
                        break;
                }

                tiles[xCoord][yCoord] = TileType.Floor;
            }
        }
    }
    
    void CorrectWallTiles() {
        for (int i = 0; i < tiles.Length; i++) {
            for (int j = 0; j < tiles[i].Length; j++) {
                if (tiles[i][j] != TileType.Floor) {
                    if(i != 0 && j != 0 && i != columns - 1 && j != rows - 1) {
                        if (tiles[i - 1][j] == TileType.Floor)
                            tiles[i][j] = TileType.HorizontalRight;

                        if (tiles[i + 1][j] == TileType.Floor)
                            tiles[i][j] = TileType.HorizontalLeft;

                        if (tiles[i][j - 1] == TileType.Floor)
                            tiles[i][j] = TileType.VerticalTop;

                        if (tiles[i][j + 1] == TileType.Floor)
                            tiles[i][j] = TileType.VerticalBottom;
                    } else {
                        if ((i == 99 && j != 99) && tiles[i][j + 1] == TileType.Floor) {
                            tiles[i][j] = TileType.VerticalBottom;
                        }

                        if ((i == 99 && j != 0) && tiles[i][j - 1] == TileType.Floor) {
                            tiles[i][j] = TileType.VerticalTop;
                        }

                        if ((i == 0 && j != 99) && tiles[i][j + 1] == TileType.Floor) {
                            tiles[i][j] = TileType.VerticalBottom;
                        }

                        if ((i == 0 && j != 0) && tiles[i][j - 1] == TileType.Floor) {
                            tiles[i][j] = TileType.VerticalTop;
                        }

                        if ((j == 99 && i != 0) && tiles[i - 1][j] == TileType.Floor) {
                            tiles[i][j] = TileType.HorizontalRight;
                        }

                        if ((j == 99 && i != 99) && tiles[i + 1][j] == TileType.Floor) {
                            tiles[i][j] = TileType.HorizontalLeft;
                        }

                        if (j == 99 && tiles[i][j - 1] == TileType.Floor) {
                            tiles[i][j] = TileType.VerticalTop;
                        }

                        if (j == 0 && tiles[i][j + 1] == TileType.Floor) {
                            tiles[i][j] = TileType.VerticalBottom;
                        }

                        if (i == 0 && tiles[i + 1][j] == TileType.Floor) {
                            tiles[i][j] = TileType.HorizontalLeft;
                        }

                        if (i == 99 && tiles[i - 1][j] == TileType.Floor) {
                            tiles[i][j] = TileType.HorizontalRight;
                        }
                    }
                } else if (tiles[i][j] == TileType.Floor) {
                    InstantiateFromArray(floorTiles, i, j);
                }
            }
        }

        for (int i = 0; i < tiles.Length; i++) {
            for (int j = 0; j < tiles[i].Length; j++) {
                if (i != 0 && j != 0 && i != columns - 1 && j != rows - 1) {
                    if (tiles[i][j] == TileType.Floor)
                        continue;

                    if (tiles[i + 1][j] == TileType.VerticalBottom && tiles[i][j - 1] == TileType.HorizontalRight) {
                        tiles[i][j] = TileType.Corner_right_down;
                    }                

                    if (tiles[i - 1][j] == TileType.VerticalBottom && tiles[i][j - 1] == TileType.HorizontalLeft) {
                        tiles[i][j] = TileType.Corner_left_down;
                    }

                    if (tiles[i - 1][j] == TileType.VerticalTop && tiles[i][j + 1] == TileType.HorizontalLeft) {
                       tiles[i][j] = TileType.Corner_left_up;
                    }

                    if (tiles[i + 1][j] == TileType.VerticalTop && tiles[i][j + 1] == TileType.HorizontalRight) {
                       tiles[i][j] = TileType.Corner_right_up;
                    }                   
                }
            }
        }
    }

    void InstantiateTiles() {
        // Go through all the tiles in the jagged array...
        for (int i = 0; i < tiles.Length; i++) {
            for (int j = 0; j < tiles[i].Length; j++) {
                if(tiles[i][j] == TileType.Corner_left_down) {
                    InstantiateFromArray(CornerLeftDown, i, j);
                } else if(tiles[i][j] == TileType.Corner_left_up) {
                    InstantiateFromArray(CornerLeftUp, i, j);
                } else if (tiles[i][j] == TileType.Corner_right_down) {
                    InstantiateFromArray(CornerRightDown, i, j);
                } else if (tiles[i][j] == TileType.Corner_right_up) {
                    InstantiateFromArray(CornerRightUp, i, j);
                }

                if (tiles[i][j] == TileType.VerticalTop) {
                    InstantiateFromArray(wallNorth, i, j);
                } else if (tiles[i][j] == TileType.VerticalBottom) {
                    InstantiateFromArray(wallSouth, i, j);
                } else if(tiles[i][j] == TileType.HorizontalRight) {                   
                    InstantiateFromArray(wallEast, i, j);
                } else if (tiles[i][j] == TileType.HorizontalLeft) {
                    InstantiateFromArray(wallWest, i, j);
                }
            }
        }
    }


    void InstantiateOuterWalls() {
        // The outer walls are one unit left, right, up and down from the board.
        int leftEdgeX = -1;
        int rightEdgeX = columns;
        int bottomEdgeY = 0;
        int topEdgeY = rows - 1;

        InstantiateVerticalOuterWall(leftEdgeX, bottomEdgeY, topEdgeY);
        InstantiateVerticalOuterWall(rightEdgeX, bottomEdgeY, topEdgeY);

        InstantiateHorizontalOuterWall(leftEdgeX + 1, rightEdgeX - 1, bottomEdgeY);
        InstantiateHorizontalOuterWall(leftEdgeX + 1, rightEdgeX - 1, topEdgeY);
    }


    void InstantiateVerticalOuterWall(int xCoord, int startingY, int endingY) {
        // Start the loop at the starting value for Y.
        int currentY = startingY;

        while (currentY <= endingY) {
            if(xCoord == -1 && tiles[0][currentY] == TileType.Floor)
                InstantiateFromArray(wallWest, xCoord, currentY);

            if (xCoord == columns && tiles[columns - 1][currentY] == TileType.Floor)
                InstantiateFromArray(wallEast, xCoord, currentY);

            currentY++;
        }
    }


    void InstantiateHorizontalOuterWall(int startingX, int endingX, int yCoord) {
        // Start the loop at the starting value for X.
        int currentX = startingX;
        yCoord = (yCoord == 0) ? -1 : rows;
        while (currentX <= endingX) {
            if (yCoord == -1 && tiles[currentX][0] == TileType.Floor)
                InstantiateFromArray(wallSouth, currentX, yCoord);

            if (yCoord == rows && tiles[currentX][rows - 1] == TileType.Floor)
                InstantiateFromArray(wallNorth, currentX, yCoord);

            currentX++;
        }
    }


    void InstantiateFromArray(GameObject[] prefabs, float xCoord, float yCoord) {
        // Create a random index for the array.
        int randomIndex = UnityEngine.Random.Range(0, prefabs.Length);

        Vector3 position = new Vector3(xCoord, 0f, yCoord);

        GameObject tileInstance = Instantiate(prefabs[randomIndex], position, Quaternion.Euler(90,0,0)) as GameObject;

        tileInstance.transform.parent = boardHolder.transform;
    }

    void SpawnLoots() {      
        for (int i = 0; i < tiles.Length; i++) {
            for (int j = 0; j < tiles[i].Length; j++) {
                if(tiles[i][j] == TileType.Floor) {
                    placeAble.Add(new Vector2(i, j));
                }
            }
        }

        while(chests.Count < numChest) {
            Vector2 tile = placeAble[UnityEngine.Random.Range(0, placeAble.Count)];
            if (Vector3.Distance(new Vector3(tile.x, 0, tile.y), rocketPos) > 10) {
                placeAble.Remove(tile);
                GameObject chest = Instantiate(chestGO, new Vector3(tile.x, 0, tile.y), Quaternion.identity);
                chest.transform.eulerAngles = new Vector3(90, 0, 0);
                chests.Add(chest);
            }
        }

        while(stones.Count < numStomes) {
            Vector2 tile = placeAble[UnityEngine.Random.Range(0, placeAble.Count)];
            if(Vector3.Distance(new Vector3(tile.x, 0, tile.y), rocketPos) > 10) {
                placeAble.Remove(tile);
                GameObject stone = Instantiate(stoneGO, new Vector3(tile.x, 0, tile.y), Quaternion.identity);
                stone.transform.eulerAngles = new Vector3(90, 0, 0);
                stones.Add(stone);
            }
        }

    }
}