using UnityEngine;

public class FireProjectiles : MonoBehaviour
{
    [SerializeField] GameObject projectile;  // Objeto del proyectil
    [SerializeField] Transform firePoint;    // Punto desde donde sale la bala
    [SerializeField] float delay;            // Retardo antes de destruir el proyectil
    [SerializeField] AudioSource shootSound; 

    void Update()
    {
        if (Input.GetButtonDown("Fire1"))
        {
            if (shootSound != null)
                shootSound.Play();
                
            GameObject clone = Instantiate(
                projectile,
                firePoint.position,
                firePoint.rotation
            );

            clone.GetComponent<ProjectileMovement>().SetDirection(firePoint.forward);
            Destroy(clone, delay);
        }
    }
}