using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    public static int Score { get; private set; } = 0;
    public static int Lives { get; private set; } = 3;

    // Cantidad de ladrillos destructibles por escena (Intro, Escena 1, Escena 2)
    public static int[] totalBricks = new int[] { 0,64, 39, 0 }; 

    public static void UpdateScore(int points) { Score += points; }
    public static void UpdateLives() { Lives--; }
    public static void ResetGame()
{
    Score = 0;

    Lives = 3;

    SceneManager.LoadScene(1);
}
}