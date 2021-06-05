using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class MapGeneratorTwoType : MonoBehaviour
{
    [Header("----------------------- Настройки карты -----------------------")]
    [Range(0, 0.3f)][SerializeField] private float border;
    [Range(6, 50)][SerializeField] private int xSize; //ширина карты
    [Range(6, 50)][SerializeField] private int ySize;
    [Space]
    [Range(0, 100)][SerializeField] private int Purity_CellTypeOne;
    [Range(0, 100)][SerializeField] private int Purity_CellTypeTwo;
    [Range(0, 100)][SerializeField] private int Purity_CellTypeThree;
    [Space]
    [SerializeField] private bool randomSeed; //включение рандомного семени
    [SerializeField] private string seed;
    [Space]
    [Header("--------------------------- Системное ---------------------------")]
    [SerializeField] private Tile CellTypeOne;
    [SerializeField] private Tile CellTypeTwo;
    [SerializeField] private Tile CellTypeThree;
	private int[,] coordinates;
	public Map map;
    
    private void Start()
	{
		coordinates = new int[xSize, ySize];
        Generation();
    }

    private void Generation()
    {
        if (randomSeed)
        {
            seed = "" + Random.Range(-9999f, 9999f);
        }

        System.Random randHash = new System.Random(seed.GetHashCode());
        for (int x = 0; x < xSize; x++)
        {
            for (int y = 0; y < ySize; y++)
            {
                var rand = randHash.Next(0, 100);
                if (Purity_CellTypeOne > rand) coordinates[x, y] = 0;
                else if (Purity_CellTypeTwo > rand) coordinates[x, y] = 1;
                else if (Purity_CellTypeThree > rand) coordinates[x, y] = 2;
                else
                {
	                coordinates[x, y] = 0;
                }
            }
        }

        Spawn();
    }

    private void Spawn()
    {
	    if (coordinates != null)
	    {
		    
		    

			    Tile[,] tiles = new Tile[xSize, ySize];
	    	
		    string nameGroupOne = "Ocean";
		    string nameGroupTwo = "Forest";
		    string nameGroupThree = "Desert";
		    string nameGroupFour = "Tundra";
		    string nameGroupFive = "SnowWasted";
		    string nameGroupSix = "Rock";
		    if (transform.Find(nameGroupOne))
		    {
			    DestroyImmediate(transform.Find(nameGroupOne).gameObject);
		    }

		    if (transform.Find(nameGroupTwo))
		    {
			    DestroyImmediate(transform.Find(nameGroupTwo).gameObject);
		    }

		    if (transform.Find(nameGroupThree))
		    {
			    DestroyImmediate(transform.Find(nameGroupThree).gameObject);
		    }

		    if (transform.Find(nameGroupFour))
		    {
			    DestroyImmediate(transform.Find(nameGroupFour).gameObject);
		    }

		    if (transform.Find(nameGroupFive))
		    {
			    DestroyImmediate(transform.Find(nameGroupFive).gameObject);
		    }

		    if (transform.Find(nameGroupSix))
		    {
			    DestroyImmediate(transform.Find(nameGroupSix).gameObject);
		    }


		    Transform mapGroupOne = new GameObject(nameGroupOne).transform;
		    mapGroupOne.parent = transform;
		    Transform mapGroupTwo = new GameObject(nameGroupTwo).transform;
		    mapGroupTwo.parent = transform;
		    Transform mapGroupThree = new GameObject(nameGroupThree).transform;
		    mapGroupThree.parent = transform;
		    Transform mapGroupFour = new GameObject(nameGroupFour).transform;
		    mapGroupFour.parent = transform;
		    Transform mapGroupFive = new GameObject(nameGroupFive).transform;
		    mapGroupFive.parent = transform;
		    Transform mapGroupSix = new GameObject(nameGroupSix).transform;
		    mapGroupSix.parent = transform;
		    for (int x = 0; x < xSize; x++)
		    {
			    for (int y = 0; y < ySize; y++)
			    {
				    int shance = Random.Range(0, 100);
				    int yRotation = 0;
				    if (shance < 25) yRotation = 0;
				    else if(shance < 50) yRotation = 90;
				    else if(shance < 75) yRotation = 180;
				    else if(shance < 100) yRotation = 270;
			    	Tile newCell = null;
				    if (coordinates[x, y] == 0)
				    {
					    Vector3 cellPosition = new Vector3(x, 0, y) * map.GridSize;
					    newCell = Instantiate(CellTypeOne, cellPosition, Quaternion.identity);
					    newCell.transform.localScale = Vector3.one * (1 - border);
					    newCell.transform.parent = mapGroupOne;
					    newCell.transform.eulerAngles = new Vector3(0, yRotation, 0);
				    }
				    else if (coordinates[x, y] == 1)
				    {
					    Vector3 cellPosition = new Vector3(x, 0, y) * map.GridSize;
					    newCell = Instantiate(CellTypeTwo, cellPosition, Quaternion.identity);
					    newCell.transform.localScale = Vector3.one * (1 - border);
					    newCell.transform.parent = mapGroupTwo;
					    newCell.transform.eulerAngles = new Vector3(0, yRotation, 0);
				    }
				    else if (coordinates[x, y] == 2)
				    {
					    Vector3 cellPosition = new Vector3(x, 0, y) * map.GridSize;
					    newCell = Instantiate(CellTypeThree, cellPosition, Quaternion.identity);
					    newCell.transform.localScale = Vector3.one * (1 - border);
					    newCell.transform.parent = mapGroupTwo;
					    newCell.transform.eulerAngles = new Vector3(0, yRotation, 0);
				    }
				    tiles[x,y] = newCell;
			    }
		    }
		    map.Init(xSize, ySize, tiles);
	    }
    }
}
