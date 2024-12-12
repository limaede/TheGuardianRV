using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Shooter : MonoBehaviour
{
    public float speed = 6f; // Velocidad de movimiento
    public float rotationSpeed = 5f; // Velocidad de rotación hacia el jugador
    public GameObject bulletPrefab; // Prefab del proyectil
    public Transform firePoint; // Punto desde donde se disparan los proyectiles
    public float shootCooldown = 2f; // Tiempo mínimo entre disparos
    public float randomShootFactor = 1f; // Variación aleatoria en el tiempo entre disparos

    private Transform player; // Referencia al jugador
    private float shootTimer = 0f; // Contador para controlar el disparo

    void Start()
    {
        // Encuentra al jugador (debe tener la etiqueta "Player")
        GameObject playerObject = GameObject.FindGameObjectWithTag("Player");
        if (playerObject != null)
        {
            player = playerObject.transform;
        }
        else
        {
            Debug.LogError("No se encontró un objeto con la etiqueta 'Player'");
        }
    }

    void Update()
    {
        if (player == null) return; // No hace nada si no hay jugador

        RotateTowardsPlayer();
        NormalMovement();
        HandleShooting();
    }

    void NormalMovement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void RotateTowardsPlayer()
    {
        // Dirección hacia el jugador
        Vector3 direction = (player.position - transform.position).normalized;

        // Rotación solo en el eje Y (importante para VR)
        Quaternion lookRotation = Quaternion.LookRotation(new Vector3(direction.x, 0, direction.z));
        transform.rotation = Quaternion.Slerp(transform.rotation, lookRotation, Time.deltaTime * rotationSpeed);
    }

    void HandleShooting()
    {
        shootTimer += Time.deltaTime;

        // Tiempo aleatorio entre disparos
        float randomShootCooldown = shootCooldown + Random.Range(-randomShootFactor, randomShootFactor);

        if (shootTimer >= randomShootCooldown)
        {
            Shoot();
            shootTimer = 0f; // Reinicia el contador
        }
    }

    void Shoot()
    {
        if (bulletPrefab != null && firePoint != null)
        {
            Instantiate(bulletPrefab, firePoint.position, firePoint.rotation);
            Debug.Log("Disparo realizado");
        }
        else
        {
            Debug.LogWarning("BulletPrefab o FirePoint no asignados");
        }
    }
}