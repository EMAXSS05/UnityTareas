using UnityEngine;

public class MouseLook : MonoBehaviour
{
    const float CLAMP_MIN = -45f;
    const float CLAMP_MAX = 45f;

    float lookSensitivity = 2f;

    GameObject player;
    Vector2 rotation = Vector2.zero;
    Vector2 velRot = Vector2.zero;
    float smoothRotationY = 0f;

    void Start()
    {
        // La cámara es hija del Player, obtenemos referencia al padre
        player = transform.parent.gameObject;
    }

    void Update()
    {
        // Rotación horizontal: gira el Player sobre el eje Y
        player.transform.Rotate(Vector3.up, Input.GetAxis("Mouse X") * lookSensitivity);

        // Rotación vertical: gira la cámara arriba/abajo
        rotation.y += Input.GetAxis("Mouse Y");
        rotation.y = Mathf.Clamp(rotation.y, CLAMP_MIN, CLAMP_MAX);

        // Suavizado del giro vertical
        smoothRotationY = Mathf.SmoothDamp(smoothRotationY, rotation.y, ref velRot.y, 0.1f);

        // Aplicar rotación suavizada a la cámara
        transform.localEulerAngles = new Vector3(-smoothRotationY, 0, 0);
    }
}