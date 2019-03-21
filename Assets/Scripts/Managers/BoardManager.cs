using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    public int[] rooms = new int[9];
    public int entranceRoom;
    public int exitRoom;
    public int roomWidth = 8;
    public int roomHeight = 8;
    public GameObject floorTile;
    public GameObject ceilingTile;
    public GameObject wallTile;
    public GameObject bgTile;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void PathFromEntranceToExit()
    {
        entranceRoom = Random.Range(0, 2);
        rooms[0] = entranceRoom;
    }

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 0; x < roomWidth; x++)
        {
            for (int y = 0; y < roomHeight; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for(int x = -1; x < roomWidth + 1; x++)
        {
            for (int y = -1; y < roomHeight + 1; y++)
            {
                GameObject toInstantiate = bgTile;

                if (x == -1 || x == roomWidth || y == -1 || y == roomHeight)
                    toInstantiate = wallTile;

                if (y == 0)
                    if(x != -1 && x != roomWidth)
                        toInstantiate = floorTile;

                if (y == roomHeight - 1)
                    if (x != -1 && x != roomWidth)
                        toInstantiate = ceilingTile;

                GameObject instance = Instantiate(toInstantiate, new Vector3(x, y, 0f), Quaternion.identity);

                instance.transform.SetParent(boardHolder);
            }
        }
    }

    public void SetupScene()
    {
        InitialiseList();
        BoardSetup();
    }
}
