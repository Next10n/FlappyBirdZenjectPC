using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Zenject;



public class ScoreController
{


    private int _score =0;
    private int _maxScore =0;

    public void SetScore(int score)
    {
        _score = score;
    }

    public int GetScore()
    {
        return _score;
    }

    public void AddScore()
    {
        _score++;
        SetMaxScore(_score);
    }

    public int GetMaxScore()
    {
        return _maxScore;
    }

    public void DecreaseScore(int decrease)
    {
        _score -= decrease;
    }

    public void SetMaxScore(int maxScore)
    {
        if(maxScore > _maxScore)
        {
            _maxScore = maxScore;
        }
    }
}
