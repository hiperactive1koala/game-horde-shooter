using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    [SerializeField] private float _speed;
    [SerializeField] private GameObject _controlDir;
    [SerializeField] private Animator _playerAnimator;

    private void Start()
    {
        QualitySettings.vSyncCount = 0;
        Application.targetFrameRate = 60;
    }

    private void Update()
    {
        var horizontal = Input.GetAxis("Horizontal");
        var vertical = Input.GetAxis("Vertical");

        _playerAnimator.SetFloat("X", horizontal);
        _playerAnimator.SetFloat("Y", vertical);
        
        var dir = new Vector3(horizontal, 0f, vertical).normalized;
        transform.Translate(dir * (_speed * Time.deltaTime) , _controlDir.transform);
    }
}