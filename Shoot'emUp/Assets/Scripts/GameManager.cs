using UnityEngine;
using TMPro;
using System.Collections;

public class GameManager : MonoBehaviour
{
    const int LIVES = 3;
    const int EXTRA_LIFE_SCORE = 1000; 
    bool extraLifeGiven = false;

    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] TextMeshProUGUI txtMaxScore;
    [SerializeField] TextMeshProUGUI txtMessage;
    [SerializeField] GameObject[] imgLives;

    int score;
    int maxScore;
    int lives = LIVES;

    static GameManager instance;

    public static GameManager GetInstance(){
        return instance;
    }

    void Awake() {
        if(instance == null){
            instance = this;
            DontDestroyOnLoad(gameObject);
        } else if(instance != this) {
            Destroy(gameObject);
        }
    }

    private void OnGUI() {
        for(int i = 0; i < imgLives.Length; i++){
            imgLives[i].SetActive(i < lives);
        }
        txtScore.text = string.Format("{0,4:D4}", score);
    }
    public void AddScore(int points){
    score += points;
    if(score > maxScore)
        maxScore = score;
    txtMaxScore.text = string.Format("{0,4:D4}", maxScore);
    if(score >= EXTRA_LIFE_SCORE && !extraLifeGiven){
        extraLifeGiven = true;
        lives++;
        if(lives > LIVES) lives = LIVES;
        txtMessage.text = "EXTRA LIFE";
        StartCoroutine(ClearMessage());
    }

}
IEnumerator ClearMessage(){
    yield return new WaitForSeconds(2f);
    txtMessage.text = "";
}

public void LoseLife(){
    lives--;
    if(lives <= 0){
        lives = 0;
        txtMessage.text = "GAME OVER\nPulsa R para reiniciar";
        Time.timeScale = 0;
    }
}
public int GetLives(){
    return lives;
}
void Update(){
    if(Time.timeScale == 0 && Input.GetKeyDown(KeyCode.R)){
        RestartGame();
    }
}

void RestartGame(){
    score = 0;
    lives = LIVES;
    extraLifeGiven = false;
    txtMessage.text = "";
    Time.timeScale = 1;
    UnityEngine.SceneManagement.SceneManager.LoadScene(
        UnityEngine.SceneManagement.SceneManager.GetActiveScene().name
    );
}
void OnEnable(){
    UnityEngine.SceneManagement.SceneManager.sceneLoaded += OnSceneLoaded;
}

void OnDisable(){
    UnityEngine.SceneManagement.SceneManager.sceneLoaded -= OnSceneLoaded;
}

void OnSceneLoaded(UnityEngine.SceneManagement.Scene scene, UnityEngine.SceneManagement.LoadSceneMode mode){
    txtScore = GameObject.FindWithTag("Score").GetComponent<TextMeshProUGUI>();
    txtMaxScore = GameObject.FindWithTag("MaxScore").GetComponent<TextMeshProUGUI>();
    txtMessage = GameObject.FindWithTag("Message").GetComponent<TextMeshProUGUI>();
    imgLives = GameObject.FindGameObjectsWithTag("Life");
}
}