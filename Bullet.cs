using System;
using UnityEngine;

public class Bullet : MonoBehaviour
{
    private Gun _gun;
    
    public void SetGun(Gun gun) => _gun = gun;

    private void OnCollisionEnter(Collision collision)
    {
        gameObject.SetActive(false);
        _gun.AddToPool(this);

        var enemy = collision.collider.GetComponent<Enemy>();
        if (enemy != null)
        {
            enemy.TakeDamage(collision.contacts[0].point);
        }
    }
}
