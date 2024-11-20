using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Bullet : MonoBehaviour, IPooledObject
{
    [SerializeField] SpawnedObjectSO objectType;
    [SerializeField] Rigidbody rb;

    GameObject shooter;

    int damage;

    private Vector3 previousPosition;

    public void Fire(Vector3 direction,float moveSpeed, int damage, GameObject shooter)
    {
        transform.up = direction;
        rb.AddForce(direction * moveSpeed, ForceMode.Impulse);
        this.damage = damage;
        this.shooter = shooter;
    }

    public void OnObjectReturn()
    {
        rb.velocity = Vector3.zero;
    }

    public void OnObjectSpawn()
    {
        Invoke(nameof(AutoDestroy), 5f);
    }

    public void OnObjectDestroy()
    {
        
    }

    void AutoDestroy()
    {
        ObjectPoolManager.Instance.ReturnToPool(gameObject, objectType);
    }


    private void OnTriggerEnter(Collider other)
    {
        if (other.gameObject == shooter) return;

        if (other.CompareTag("Player") || other.CompareTag("Enemy"))
        {
            Debug.Log("hit");
            other.GetComponent<IDamageable>()?.Damage(damage);
            ObjectPoolManager.Instance.ReturnToPool(gameObject, objectType);
        }
            
    }
}
