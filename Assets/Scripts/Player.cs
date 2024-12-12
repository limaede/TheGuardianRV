using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public float rotationSpeed = 30f; // Velocidad de rotación constante
    public Camera mainCamera; // Exponemos la cámara en el Inspector para asignarla manualmente si es necesario

    void Start()
    {
        if (mainCamera == null)
        {
            // Si la cámara no se asigna manualmente, buscamos la cámara principal
            mainCamera = Camera.main;
            if (mainCamera == null)
            {
                Debug.LogError("Main Camera not found. Please assign it in the Inspector or tag it as 'MainCamera'.");
            }
        }
    }

    void Update()
    {
        if (mainCamera != null)
        {
            Ray ray = mainCamera.ScreenPointToRay(Input.mousePosition);
            Plane plane = new Plane(Vector3.up, transform.position);

            if (plane.Raycast(ray, out float distance))
            {
                Vector3 mousePositionInWorld = ray.GetPoint(distance);
                Vector3 directionToMouse = (mousePositionInWorld - transform.position).normalized;

                Quaternion targetRotation = Quaternion.LookRotation(directionToMouse);
                transform.rotation = Quaternion.Slerp(transform.rotation, targetRotation, rotationSpeed * Time.deltaTime);
            }
        }
    }
    /* public float speed = 7;
     Rigidbody rb;
     public Transform firePoint;
     public Bullet bulletPrefab;
     //public Bullet powerPrefab;



     void Start()
     {
         rb = GetComponent<Rigidbody>();
     }

     void Update()
     {
         //Shoot();
         rb.velocity = new Vector3(Input.GetAxis("Horizontal") * speed,
             rb.velocity.y, Input.GetAxis("Vertical") * speed);
     }*/
    void Shoot()
    {
        if (Input.GetMouseButtonDown(0))
        {
            ///onshoot?.Invoke(bulletPrefab, null);
            //Instantiate(bulletPrefab, firePoint.position, transform.rotation);
        }
        else if (Input.GetMouseButtonDown(1) )///&& SkillManager.instance.skills.Count > 0)
        {
            //Skill_SO skill = SkillManager.instance.UseSkill();
            //onshootP?.Invoke(powerPrefab,skill);
            ///onshootP?.Invoke(powerPrefab, SkillManager.instance.UseSkill());

        }
    }
}
