using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    

    public enum GameState
    {
        Init,
        Ready,
        Running,
        Finished
    }

    public GameState State { get; private set; } = GameState.Init;

    private void Start()
    {
        _historicPlayer = GameObject.Find("Player").GetComponent<HistoricLocation>();
    }


    public delegate int GameStartedDelegate();

    public GameStartedDelegate OnGameStarted;

    
    public delegate void GameFinishedDelegate();

    public GameFinishedDelegate OnGameFinished;

    private int _score = 0;
    private int _scoreMax = 0;
    private List<HistoricLocation> _historicLocationsScript = new List<HistoricLocation>();
    private HistoricLocation _historicPlayer;


    private void CheckEndGame()
    {
        if (_score == _scoreMax)
        {
            Debug.Log("Victoire !");
            State = GameState.Finished;
        }
    }

    public void IncrementScore()
    {
        ++_score;
        CheckEndGame();
    }
    public void DecrementScore()
    {
        --_score;
    }
    

    public void StartGame()
    {
        _scoreMax = OnGameStarted();
        State = GameState.Running;
    }

    public void PushToHistoricLocationScript(HistoricLocation moveBox)
    {
        _historicLocationsScript.Add(moveBox);
        _historicPlayer.SavePositionAndRotation();
    }
    
    public void Undo()
    {
        if (State == GameState.Running)
        {
            int lastIndex = _historicLocationsScript.Count - 1;
            if (lastIndex >= 0)
            {
                _historicLocationsScript[lastIndex].UndoPosition();
                _historicLocationsScript.RemoveAt(lastIndex);
            }
            _historicPlayer.UndoPositionAndRotation();
        }
    }
}
