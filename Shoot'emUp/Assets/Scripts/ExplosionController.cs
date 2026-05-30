using UnityEngine;

public class ExplosionController : MonoBehaviour
{
    const float DELAY = 0.25f;
    [SerializeField] AudioClip explosionSound;

    void Start()
    {
        AudioSource.PlayClipAtPoint(explosionSound, Camera.main.transform.position);
        Destroy(gameObject, DELAY);
    }
}