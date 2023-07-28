using System;
using UnityEngine;
using UnityEngine.AI;
using UnityEngine.SceneManagement;
using Utilities;

public class Enemy : MonoBehaviour
{
    #region Events
    
    public static event EventHandler<OnEnemyStatusChangedEventArgs> OnEnemyDead;
    public class OnEnemyStatusChangedEventArgs: EventArgs
    {
        public int ScorePoint;
        public OnEnemyStatusChangedEventArgs(int scorePoint) => ScorePoint = scorePoint;
    }
    #endregion
    
    [SerializeField] private GameObject _hitParticle;
    [SerializeField] private GameObject _explosionParticle;
    [SerializeField] private int _health = 3;
    [SerializeField] private int _scorePoint = 100;
    
    private int _currentHealth;
    private Player _player;
    private NavMeshAgent _nav;
    private void OnEnable()
    {
        _currentHealth = _health;
        _player = FindObjectOfType<Player>();
        _nav = GetComponent<NavMeshAgent>();
    }

    private void Update()
    {
        if (!GameManager.Instance.IsPlaying()) return;
        _nav.SetDestination(_player.transform.position);
    }

    public void TakeDamage(Vector3 pos)
    {
        _currentHealth--;
        
        Instantiate(_hitParticle, pos, _hitParticle.transform.rotation);
        if (_currentHealth <= 0)
        {
            Instantiate(_explosionParticle, transform.position, _explosionParticle.transform.rotation);
            OnEnemyDead?.Invoke(this, new OnEnemyStatusChangedEventArgs(scorePoint: _scorePoint));
            gameObject.SetActive(false);
        }
    }

    private void OnCollisionEnter(Collision collision)
    {
        var player = collision.collider.GetComponent<Player>();
        if (player == null) return;
        SceneManager.LoadScene(0);
    }
}