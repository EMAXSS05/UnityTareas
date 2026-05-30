using UnityEngine;

public class HitController : MonoBehaviour
{
    const float DELAY = 0.25f;
    [SerializeField] AudioClip clip;

    void Start()
    {
        AudioSource.PlayClipAtPoint(clip, Camera.main.transform.position);
        Destroy(gameObject, DELAY);
    }
}