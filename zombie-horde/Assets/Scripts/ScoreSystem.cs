using System;
using TMPro;
using UnityEngine;
using Utilities;

public class ScoreSystem : Singleton<ScoreSystem>
{
    private const string HIGH_SCORE = "HighScore";
    [SerializeField] private TMP_Text _scoreText;
    [SerializeField] private TMP_Text _highScoreText;

    private int _points;
    private int _highScore;

    private void OnEnable()
    {
        _highScore = PlayerPrefs.GetInt(HIGH_SCORE);
        _scoreText.SetText(_points.ToString());
        _highScoreText.SetText(_highScore.ToString());
        Enemy.OnEnemyDead += Enemy_OnEnemyDead;
    }

    private void OnDisable()
    {
        Enemy.OnEnemyDead -= Enemy_OnEnemyDead;
    }

    private void Enemy_OnEnemyDead(object sender, Enemy.OnEnemyStatusChangedEventArgs e)
    {
        AddPoints(e.ScorePoint);
    }

    public void AddPoints(int points)
    {
        _points += points;
        _scoreText.SetText(_points.ToString());
        if (_points > _highScore)
        {
            _highScore = _points;
            _highScoreText.SetText(_highScore.ToString());
            PlayerPrefs.SetInt(HIGH_SCORE, _highScore);
        }
    }
    
}