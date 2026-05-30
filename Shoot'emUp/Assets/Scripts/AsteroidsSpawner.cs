using UnityEngine;
using System.Collections;

public class AsteroidsSpawner : MonoBehaviour
{    
    [SerializeField] float interval = 1.5f; 
    [SerializeField] float delay = 2f;       
    [SerializeField] GameObject AsteroidBig; 

    const float MIN_X = -4.5f;     
    const float MAX_X = 4.5f;     

    void Start()    
    {        
        StartCoroutine("AsteroidSpawn");    
    }    

    IEnumerator AsteroidSpawn()    
    {        
        yield return new WaitForSeconds(delay);        
        while(true)        
        {            
            Vector3 position = new Vector3(Random.Range(MIN_X, MAX_X), transform.position.y, 0);               
            Instantiate(AsteroidBig, position, Quaternion.identity);            
            yield return new WaitForSeconds(interval);        
        }    
    }
}