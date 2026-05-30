using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using System.Collections;

public class PlayerDamage : MonoBehaviour
{
    [SerializeField] int maxHealth = 100;
    [SerializeField] TextMeshProUGUI txtHealth;
    [SerializeField] TextMeshProUGUI txtGameOver;
    [SerializeField] float restartDelay = 3f;

    int currentHealth;

    void Start()
    {
        currentHealth = maxHealth;
        txtGameOver.gameObject.SetActive(false);
        UpdateUI();
    }

    public void ApplyDamage(int damage)
    {
        currentHealth -= damage;
        currentHealth = Mathf.Max(currentHealth, 0);
        UpdateUI();

        if (currentHealth <= 0)
        {
            StartCoroutine(GameOver());
        }
    }

    IEnumerator GameOver()
    {
        txtGameOver.gameObject.SetActive(true);

        Cursor.lockState = CursorLockMode.None;

        yield return new WaitForSeconds(restartDelay);
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }

    void UpdateUI()
    {
        txtHealth.text = "HP: " + currentHealth;
    }
}