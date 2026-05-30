using UnityEngine;
using TMPro;

public class GUIController : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI txtScore;
    [SerializeField] TextMeshProUGUI txtlives;

    // Usamos OnGUI o Update para refrescar los textos constantemente
    private void OnGUI() 
    {
        // Formateamos el score a 3 dígitos (000)
        txtScore.text = string.Format("{0:D3}", GameManager.Score);
        
        // Actualizamos las vidas
        txtlives.text = GameManager.Lives.ToString();
    }
}