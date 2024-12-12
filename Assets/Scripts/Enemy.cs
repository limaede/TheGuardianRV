using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    public float speed = 6f; // Velocidad normal
    public float dashSpeed = 12f; // Velocidad durante el dash
    public float dashDuration = 0.5f; // Duración del dash en segundos
    public float dashCooldown = 3f; // Tiempo mínimo entre dashes

    private bool isDashing = false;
    private float dashTimer = 0f; // Contador para la duración del dash
    private float cooldownTimer = 0f; // Contador para el tiempo entre dashes

    void Update()
    {
        if (isDashing)
        {
            DashMovement();
        }
        else
        {
            NormalMovement();
        }

        HandleDash();
    }

    void NormalMovement()
    {
        transform.Translate(Vector3.forward * speed * Time.deltaTime);
    }

    void DashMovement()
    {
        transform.Translate(Vector3.forward * dashSpeed * Time.deltaTime);
        dashTimer += Time.deltaTime;

        if (dashTimer >= dashDuration)
        {
            isDashing = false;
            dashTimer = 0f; // Reinicia el contador de duración del dash
        }
    }

    void HandleDash()
    {
        // Incrementa el tiempo del cooldown
        cooldownTimer += Time.deltaTime;

        // Activa un dash de forma aleatoria si el cooldown lo permite
        if (!isDashing && cooldownTimer >= dashCooldown)
        {
            // 20% de probabilidad de hacer un dash cada frame tras el cooldown
            if (Random.value < 0.2f)
            {
                isDashing = true;
                cooldownTimer = 0f; // Reinicia el contador del cooldown
            }
        }
    }
}