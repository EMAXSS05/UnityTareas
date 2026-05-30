using UnityEngine;
using System.Collections;

public class ShipController : MonoBehaviour
{
    [SerializeField] private float force = 5f;
    [SerializeField] private Vector3 endPosition;
    [SerializeField] private float duration;
    [SerializeField] int blinkNum;
    [SerializeField] GameObject explosion;
    [SerializeField] GameObject shootPrefab;
    [SerializeField] float shootOffset = 0.5f;

    private bool active = false;
    private Rigidbody2D rb;
    private Vector3 initialPosition;

    private void Start()
    {
        initialPosition = transform.position;
        rb = GetComponent<Rigidbody2D>();
        StartCoroutine("StartPlayer");
    }

    private void FixedUpdate()
    {
        if (active)
            CheckMove();
    }

    void Update()
    {
        if (active && Input.GetKeyDown(KeyCode.Space))
        {
            Vector3 shootPosition = transform.position + Vector3.up * shootOffset;
            Instantiate(shootPrefab, shootPosition, Quaternion.identity);
        }
    }

    private void CheckMove()
    {
        Vector2 direction = new Vector2(Input.GetAxis("Horizontal"), Input.GetAxis("Vertical"));
        direction.Normalize();
        rb.AddForce(direction * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        string tag = other.gameObject.tag;
    if (tag == "Enemy" || tag == "Asteroid")
    {
        DestroyShip();
    }
    }

    void DestroyShip(){
    GameManager.GetInstance().LoseLife();
    active = false;
    Instantiate(explosion, transform.position, Quaternion.identity);
    
    if(GameManager.GetInstance().GetLives() > 0){
        transform.position = initialPosition;
        StartCoroutine("StartPlayer");
    }
}

    IEnumerator StartPlayer()
    {
        Material mat = GetComponent<SpriteRenderer>().material;
        Color color = mat.color;
        Collider2D collider = GetComponent<Collider2D>();
        collider.enabled = false;

        Vector3 initialPos = transform.position;
        float t = 0, t2 = 0;

        while (t < duration)
        {
            t += Time.deltaTime;
            Vector3 newPosition = Vector3.Lerp(initialPos, endPosition, t / duration);
            transform.position = newPosition;

            t2 += Time.deltaTime;
            float newAlpha = blinkNum * (t2 / duration);
            if (newAlpha > 1) t2 = 0;
            color.a = newAlpha;
            mat.color = color;

            yield return null;
        }

        color.a = 1;
        mat.color = color;
        collider.enabled = true;
        active = true;
    }
}