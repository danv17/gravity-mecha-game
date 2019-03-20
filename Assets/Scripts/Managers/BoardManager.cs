using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoardManager : MonoBehaviour
{
    [SerializeField]
    public class Count
    {
        public int minimum;
        public int maximum;

        public Count(int min, int max)
        {
            minimum = min;
            maximum = max;
        }
    }

    public int columns = 8;
    public int rows = 8;
    public GameObject floorTile;
    public GameObject ceilingTile;
    public GameObject wallTile;
    public GameObject bgTile;

    private Transform boardHolder;
    private List<Vector3> gridPositions = new List<Vector3>();

    void InitialiseList()
    {
        gridPositions.Clear();

        for (int x = 0; x < columns; x++)
        {
            for (int y = 0; y < rows; y++)
            {
                gridPositions.Add(new Vector3(x, y, 0f));
            }
        }
    }

    void BoardSetup()
    {
        boardHolder = new GameObject("Board").transform;
        for(int x = -1; x < columns + 1; x++)
        {
            for (int y = -1; y < rows + 1; y++)
            {
                GameObject toInstantiate = bgTile;

                if (x == -1 || x == columns || y == -1 || y == rows)
                    toInstantiate = wallTile;

                if (y == 0)
                    if(x != -1 && x != columns)
                        toInstantiate = floorTile;

                if (y == rows - 1)
                    if (x != -1 && x != columns)
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
