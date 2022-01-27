using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class GameBoardGeneration : MonoBehaviour
{
    [SerializeField] private GameObject prefabBaseGround;
    [SerializeField] private GameObject prefabDestination;
    [SerializeField] private GameObject prefabBox;
    [SerializeField] private GameObject prefabWall;
    [SerializeField] private GameObject Camera;
    [SerializeField] private int minScore = 2;
    [SerializeField] private int maxScore = 6;
    [SerializeField] private int nbrRowAndColumn = 15;
    
    private GameManager _gameManager;
    private List<Vector3> _coordDestinations = new List<Vector3>();
    private List<Vector3> _coordBoxes = new List<Vector3>();
    private int _nbrScore;

    private int _minBoxDistanceFromWall = 2;


    // Start is called before the first frame update
    void Start()
    {
        InitializeCoordsDestinations();
        
        _gameManager = GameObject.Find("GameManager").GetComponent<GameManager>();
        _gameManager.OnGameStarted += InitGame;
        
        _gameManager.StartGame();
    }

    bool CoordsAlreadyTaken(int x, int z, List<Vector3> vector3)
    {
        for (int i = 0; i < vector3.Count; i++)
        {
            Vector3 coord = vector3[i];
            if (coord.x == x && coord.z == z)
            {
                return true;
            }
        }
        return false;
    }

    void InitializeCoordsDestinations()
    {
        _nbrScore = Random.Range(minScore, maxScore + 1);

        for (int i = 0; i < _nbrScore; i++)
        {
            Vector3 newCoordDestination = new Vector3();
            do
            {
                newCoordDestination.Set( 
                    Random.Range(1, nbrRowAndColumn - 1),
                    1,
                    Random.Range(1, nbrRowAndColumn - 1));
            } while (CoordsAlreadyTaken((int)newCoordDestination.x, (int)newCoordDestination.z, _coordDestinations));
            
            _coordDestinations.Add(newCoordDestination);
        }

    }

    bool IsInBorderOfBoardGame(int row, int column) => row == 0 || column == nbrRowAndColumn - 1 || row == nbrRowAndColumn - 1 || column == 0;

    void SpawnBoxes()
    {
        for (int i = 0; i < _nbrScore; i++)
        {
            Vector3 coordsBox = new Vector3();
            do
            {
                coordsBox.Set( 
                    Random.Range(_minBoxDistanceFromWall, nbrRowAndColumn - _minBoxDistanceFromWall),
                    2,
                    Random.Range(_minBoxDistanceFromWall, nbrRowAndColumn - _minBoxDistanceFromWall));
            } while (CoordsAlreadyTaken((int)coordsBox.x, (int)coordsBox.z, _coordDestinations));      
            
            _coordBoxes.Add(coordsBox);
            Instantiate(prefabBox, coordsBox, Quaternion.identity, transform);
        }
    }

    private bool WallCanSpawnInsideBoardGame(int x, int z) => CoordsAlreadyTaken(x, z, _coordDestinations)
                                                              || CoordsAlreadyTaken(x, z, _coordBoxes)
                                                              || CoordsAlreadyTaken(x + 1, z, _coordBoxes)
                                                              || CoordsAlreadyTaken(x - 1, z, _coordBoxes)
                                                              || CoordsAlreadyTaken(x, z + 1, _coordBoxes)
                                                              || CoordsAlreadyTaken(x, z - 1, _coordBoxes)
                                                              || CoordsAlreadyTaken(x + 1, z - 1, _coordBoxes)
                                                              || CoordsAlreadyTaken(x - 1, z - 1, _coordBoxes)
                                                              || CoordsAlreadyTaken(x - 1, z + 1, _coordBoxes)
                                                              || CoordsAlreadyTaken(x + 1, z + 1, _coordBoxes);

    void SpawnWalls()
    {
        int nbrWalls = Random.Range(1, nbrRowAndColumn * 2);
        for (int i = 0; i < nbrWalls; i++)
        {
            Vector3 coordsWall = new Vector3();
            do
            {
                coordsWall.Set( 
                    Random.Range(1, nbrRowAndColumn),
                    1.75f,
                    Random.Range(1, nbrRowAndColumn));
            } while (WallCanSpawnInsideBoardGame((int)coordsWall.x, (int)coordsWall.z));      
            
            Instantiate(prefabWall, coordsWall, Quaternion.identity, transform);
        }
    }

    void GenerateBoardGame()
    {
        for (int row  = 0; row  < nbrRowAndColumn; row ++)
        {
            for (int column = 0; column < nbrRowAndColumn; column++)
            {
                if (CoordsAlreadyTaken(row, column, _coordDestinations))
                {
                    Instantiate(prefabDestination, new Vector3(row, 1, column), Quaternion.identity, transform);
                }
                else
                {
                    if (IsInBorderOfBoardGame(row, column))
                    {
                        Instantiate(prefabWall, new Vector3(row, 1.75f, column), Quaternion.identity, transform);
                    }
                    else
                    {
                        Instantiate(prefabBaseGround, new Vector3(row, 1, column), Quaternion.identity, transform);
                    }
                }
            }
        }
        
        Camera.transform.position = new Vector3(nbrRowAndColumn / 2, nbrRowAndColumn / 2 + 1 , -(nbrRowAndColumn / 6));
    }

    int InitGame()
    {
        GenerateBoardGame();
        SpawnBoxes();
        SpawnWalls();
        
        return _nbrScore;
    }
}
