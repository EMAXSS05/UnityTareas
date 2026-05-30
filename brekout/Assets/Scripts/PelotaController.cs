using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PelotaController : MonoBehaviour
{
    Rigidbody2D rb; 
    AudioSource sfx;

    [Header("Sonidos")]
    [SerializeField] AudioClip sfxPaddle;
    [SerializeField] AudioClip sfxBrick;
    [SerializeField] AudioClip sfxWall;
    [SerializeField] AudioClip sfxFail;
    [SerializeField] AudioClip sfxNextLevel; 

    [Header("Configuración")]
    [SerializeField] float force = 10f;
    [SerializeField] float delay = 1f;

    int brickCount; 
    int sceneId;

    Dictionary<string, int> ladrillosSolidos = new Dictionary<string, int>()
    {
        {"Ladrillo-Amarillo", 10},
        {"Ladrillo-Verde", 15},
        {"Ladrillo-Naranja", 20},
        {"Ladrillo-Rojo", 25},
        {"Ladrillo-Morado", 30}
    };

    int puntosAtravesable = 25;

    void Start()
    {
        sfx = GetComponent<AudioSource>();
        rb = GetComponent<Rigidbody2D>();
        sceneId = SceneManager.GetActiveScene().buildIndex;
        Invoke(nameof(LanzarPelota), delay);
    }

    void LanzarPelota()
    {
        transform.position = Vector3.zero; 
        rb.linearVelocity = Vector2.zero;

        float dirX = Random.Range(0, 2) == 0 ? -1 : 1;
        Vector2 dir = new Vector2(dirX, -1).normalized;

        rb.AddForce(dir * force, ForceMode2D.Impulse);
    }

    private void OnCollisionEnter2D(Collision2D other) 
    {
        string tagObjeto = other.gameObject.tag;

        if (ladrillosSolidos.ContainsKey(tagObjeto)) 
        {
            DestroyBrick(other.gameObject, ladrillosSolidos[tagObjeto]); 
        }
        else if (tagObjeto == "Pala") 
        {
            sfx.PlayOneShot(sfxPaddle);
            ManejarRebotePala(other);
        }
        else if (tagObjeto == "Pared") 
        {
            sfx.PlayOneShot(sfxWall);
        }
    }


    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Ladrillo-Atravesable"))
        {
            ProcesarLadrilloAtravesable(other.gameObject);
        }

        if (other.CompareTag("ParedInferior"))
        {
            ProcesarFallo();
        }
    }

  
    void DestroyBrick(GameObject obj, int puntos)
    {
        sfx.PlayOneShot(sfxBrick);
        GameManager.UpdateScore(puntos);

        Destroy(obj);
        CheckWinCondition();
    }

 
    void ProcesarLadrilloAtravesable(GameObject obj)
    {
        sfx.PlayOneShot(sfxBrick);
        GameManager.UpdateScore(puntosAtravesable);

        obj.GetComponent<Collider2D>().enabled = false;
        Destroy(obj, 0.1f);

        CheckWinCondition();
    }

    void CheckWinCondition()
    {
        brickCount++;
        Debug.Log("Ladrillos destruidos: " + brickCount + " / " + GameManager.totalBricks[sceneId]);

        if (brickCount == GameManager.totalBricks[sceneId]) 
        {
            sfx.PlayOneShot(sfxNextLevel);
            rb.linearVelocity = Vector2.zero;
            Invoke(nameof(NextScene), 3);
        }
    }

    void NextScene()
    {
        int nextId = sceneId + 1;

        if (nextId == SceneManager.sceneCountInBuildSettings)
            nextId = 0;

        SceneManager.LoadScene(nextId);
    }

    void ProcesarFallo()
    {
        sfx.PlayOneShot(sfxFail);
        GameManager.UpdateLives();

        if (GameManager.Lives <= 0)
        {
            rb.linearVelocity = Vector2.zero;
            gameObject.SetActive(false);
            return;
        }

        Invoke(nameof(LanzarPelota), delay);
    }

   void ManejarRebotePala(Collision2D other)
{
    Vector3 paddlePos = other.transform.position;
    float distanciaX = transform.position.x - paddlePos.x;

    float nuevaDireccionX = distanciaX * 2f;

    Vector2 nuevaDireccion = new Vector2(nuevaDireccionX, 1f).normalized;

    float velocidadActual = rb.linearVelocity.magnitude;

    rb.linearVelocity = nuevaDireccion * velocidadActual;
}
}