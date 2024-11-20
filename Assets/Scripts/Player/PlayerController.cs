using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using DG.Tweening;

public class PlayerController : MonoBehaviour, IDeathHandler, IHitHandler
{
    [SerializeField] float moveSpeed = 5f;
    [SerializeField] Joystick joystick;


    [SerializeField] float detectionRadius = 10f;
    [SerializeField] float detectionRate = .5f;

    [SerializeField] int bulletDamage = 20;
    [SerializeField] float bulletSpeed = 5f;

    [SerializeField] SpawnedObjectSO bullet;
    [SerializeField] LayerMask enemyLayer;
    

    CharacterController characterController;
    Animator anim;

    void Start()
    {
        characterController = GetComponent<CharacterController>();
        anim = GetComponent<Animator>();
        StartCoroutine(EnemyDetection());
    }

    void Update()
    {
        Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;

        //if (direction.sqrMagnitude > 0.01f) // Check if there is significant input
        //{
        //    // Calculate target rotation based on move direction
        //    Quaternion targetRotation = Quaternion.LookRotation(direction);

        //    // Smoothly rotate towards the target direction
        //    transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, 10f * Time.deltaTime);
        //}

        characterController.Move(direction * moveSpeed * Time.deltaTime);

        if (characterController.velocity.magnitude > 0)
            anim.SetBool("isMoving", true);
        else
            anim.SetBool("isMoving", false);

    }

    IEnumerator EnemyDetection()
    {
        while (true)
        {
            DetectEnemies();
            yield return new WaitForSeconds(detectionRate);
        }
    }

    void DetectEnemies()
    {
        Collider[] colliders = Physics.OverlapSphere(transform.position, detectionRadius, enemyLayer);

        if (colliders.Length > 0)
            Shoot(colliders[0].transform.position - transform.position);
        else
        {
            //Quaternion targetRotation = Quaternion.LookRotation(transform.forward);
            //transform.DORotateQuaternion(targetRotation, .3f);

            Vector3 direction = Vector3.forward * joystick.Vertical + Vector3.right * joystick.Horizontal;

            if (direction.sqrMagnitude > 0.01f) // Check if there is significant input
            {
                Quaternion targetRotation = Quaternion.LookRotation(direction);

                transform.DORotateQuaternion(targetRotation, .3f);
            }
        }

    }

    void Shoot(Vector3 enemyDirection)
    {
        Bullet bullet = ObjectPoolManager.Instance.SpawnFromPool(this.bullet, transform.position, Quaternion.identity).GetComponent<Bullet>();

        bullet.Fire(enemyDirection,bulletSpeed, bulletDamage, gameObject);
        SoundManager.Instance.PLayerShootSFX();

        Quaternion targetRotation = Quaternion.LookRotation(enemyDirection);

        transform.DORotateQuaternion(targetRotation, .3f);
    }

    public void OnDeath()
    {
        ScoreManager.Instance.GameOver();
        Destroy(gameObject);
    }

    public void OnHit()
    {
        GetComponent<HapticsHandler>()?.Vibrate();
    }
}
