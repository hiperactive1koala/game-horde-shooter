using System.Collections.Generic;
using UnityEngine;
using Utilities;

public class Gun : MonoBehaviour
{
    [SerializeField] private Transform _shootPoint;
    [SerializeField] private Bullet _bullet;
    [SerializeField] private float _delayToNextShoot = 1f;
    [SerializeField] private float _bulletSpeed = 5f;
    
    private float _timeToNextShoot;
    private Vector3 _direction;
    private Vector3 _lastDirection;
    private Queue<Bullet> _bulletPool = new Queue<Bullet>();
 
    
    private void Update()
    {
        if (Physics.Raycast(Camera.main.ScreenPointToRay(Input.mousePosition), out var raycastHit, Mathf.Infinity))
        {
            _direction = raycastHit.point - _shootPoint.position;
            _direction.Normalize();
            if (!(Vector3.Distance(raycastHit.point, transform.position) <= 1.65f))
            {
                _direction = new Vector3(_direction.x, 0f, _direction.z);
                _lastDirection = _direction;
            }
            else
            {
                _direction = _lastDirection;
            }
            transform.forward = _direction;
        }
        
        if (CanShoot())
        {
            Shoot();
        }
    }

    private bool CanShoot()
    {
        if (!GameManager.Instance.IsPlaying()) return false;
        _timeToNextShoot -= Time.deltaTime;
        if (_timeToNextShoot > 0) return false;
        _timeToNextShoot = _delayToNextShoot;
        return true;
    }

    private void Shoot()
    {
        var bullet = GetBullet();
        bullet.transform.position = _shootPoint.position;
        bullet.transform.rotation = _shootPoint.rotation;
        bullet.GetComponent<Rigidbody>().velocity = _direction * _bulletSpeed;
    }

    private Bullet GetBullet()
    {
        if (_bulletPool.Count > 0)
        {
            var bullet = _bulletPool.Dequeue();
            bullet.gameObject.SetActive(true);
            return bullet;
        }
        else
        {
            var bullet = Instantiate(_bullet, _shootPoint.position, _shootPoint.rotation);
            bullet.SetGun(this);
            return bullet;
        }
    }

    public void AddToPool(Bullet bullet)
    {
        _bulletPool.Enqueue(bullet);
    }
}