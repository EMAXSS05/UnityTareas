using UnityEngine;

public class EnemyBite : MonoBehaviour
{
    float damageCooldown = 1f;
    float nextDamageTime;

    private void OnTriggerStay(Collider other)
    {
        if (other.CompareTag("Player") && Time.time >= nextDamageTime)
        {
            other.SendMessage("ApplyDamage", 10);
            nextDamageTime = Time.time + damageCooldown;
        }
    }
}