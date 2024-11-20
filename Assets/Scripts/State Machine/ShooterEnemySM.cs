using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ShooterEnemySM : EnemySM
{
    [SerializeField] SpawnedObjectSO bullet;
    [SerializeField] float bulletSpeed;

    public override void AttackPlayer()
    {
        Shoot();
        Animator.CrossFadeInFixedTime("Attack", .2f);
    }

    public void Shoot()
    {
        Bullet bullet = ObjectPoolManager.Instance.SpawnFromPool(this.bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();

        Vector3 fireDirection = playerTransform.position - transform.position;

        bullet.Fire(fireDirection, bulletSpeed, Damage, gameObject);

        SoundManager.Instance.EnemyShootSFX();
    }
}
