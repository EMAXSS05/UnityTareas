using UnityEngine;

public class AsteroidsController : MonoBehaviour
{    
    [SerializeField] float minSpeedY = 2f;   
    [SerializeField] float maxSpeedY = 5f;   
    [SerializeField] float minSpeedX = -2f;  
    [SerializeField] float maxSpeedX = 2f;  
    
    [SerializeField] GameObject explosionPrefab;    
    
    Rigidbody2D rb;    
    const float DESTROY_Y = -7f;    

    void Start()    
    {        
        rb = GetComponent<Rigidbody2D>();        
        
        // Calcular velocidad aleatoria en X e Y
        float speedY = Random.Range(minSpeedY, maxSpeedY);        
        float speedX = Random.Range(minSpeedX, maxSpeedX);        
        
        // Aplicar la velocidad al Rigidbody (Sintaxis moderna de Unity)
        rb.linearVelocity = new Vector2(speedX, -speedY);    
    }    

    void Update()    
    {        
        // Si el asteroide pasa del límite inferior, se destruye para no gastar memoria
        if (transform.position.y < DESTROY_Y)        
        {            
            Destroy(gameObject);        
        }    
    }    

    private void OnCollisionEnter2D(Collision2D other)    
    {        
        if (other.gameObject.CompareTag("Shoot") || other.gameObject.CompareTag("Player"))        
        {            
            Explode();        
        }    
    }    

    void Explode()    
    {        
        if (explosionPrefab != null)            
            Instantiate(explosionPrefab, transform.position, Quaternion.identity);        
        
        Destroy(gameObject);    
    }
    private void OnTriggerEnter2D(Collider2D other)
{
    if (other.gameObject.CompareTag("Shoot"))
    {
        GameManager.GetInstance().AddScore(50);
        Explode();
    }
}
}