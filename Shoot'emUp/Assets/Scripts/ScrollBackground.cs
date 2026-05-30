using UnityEngine;

public class ScrollBackground : MonoBehaviour
{
    [SerializeField] float speed;
    float height;

    void Start()
    {
        height = GetComponent<SpriteRenderer>().bounds.size.y;
    }

    void Update()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);

        if (transform.position.y < -height)
        {
            transform.Translate(Vector3.up * 2 * height);
        }
    }
}