using UnityEngine;

public class ParallaxLoopFull : MonoBehaviour
{
    [SerializeField] private Transform cameraTransform; // La cámara principal
    [SerializeField] private float spriteWidth;         // El ancho del sprite
    [SerializeField] private float spriteHeight;        // El alto del sprite
    [SerializeField] private Vector2 parallaxMultiplier; // Factor de movimiento (X, Y)

    [Header("Control de Parallax")]
    [SerializeField] private bool enableHorizontalParallax = true; // Activar parallax horizontal
    [SerializeField] private bool enableVerticalParallax = true;   // Activar parallax vertical

    private Vector3 lastCameraPosition;
    private SpriteRenderer spriteRenderer;

    private void Start()
    {
        if (cameraTransform == null)
        {
            cameraTransform = Camera.main.transform;
        }

        lastCameraPosition = cameraTransform.position;

        // Calcula automáticamente el ancho y alto del sprite si no están configurados
        if (spriteWidth <= 0 || spriteHeight <= 0)
        {
            spriteRenderer = GetComponent<SpriteRenderer>();
            if (spriteRenderer != null)
            {
                spriteWidth = spriteRenderer.bounds.size.x - 0.1f;
                spriteHeight = spriteRenderer.bounds.size.y;
            }
            else
            {
                Debug.LogError("No se encontró un SpriteRenderer. Asegúrate de que el objeto tiene uno o configura el ancho/alto manualmente.");
            }
        }
    }

    private void LateUpdate()
    {

        // Calcula el movimiento de la cámara desde el último cuadro
        Vector3 deltaMovement = cameraTransform.position - lastCameraPosition;

        // Mueve el fondo en función del parallaxMultiplier
        Vector3 movement = Vector3.zero;

        if (enableHorizontalParallax)
        {
            movement.x = deltaMovement.x * parallaxMultiplier.x;
        }

        if (enableVerticalParallax)
        {
            movement.y = deltaMovement.y * parallaxMultiplier.y;
        }

        transform.position += movement;
        lastCameraPosition = cameraTransform.position;

        // Reposiciona horizontalmente si está habilitado
        if (enableHorizontalParallax)
        {
            if (cameraTransform.position.x - transform.position.x >= spriteWidth)
            {
                transform.position += new Vector3(spriteWidth * 2, 0, 0);
            }
            else if (transform.position.x - cameraTransform.position.x >= spriteWidth)
            {
                transform.position -= new Vector3(spriteWidth * 2, 0, 0);
            }
        }

        // Reposiciona verticalmente si está habilitado
        if (enableVerticalParallax)
        {
            if (cameraTransform.position.y - transform.position.y >= spriteHeight)
            {
                transform.position += new Vector3(0, spriteHeight * 2, 0);
            }
            else if (transform.position.y - cameraTransform.position.y >= spriteHeight)
            {
                transform.position -= new Vector3(0, spriteHeight * 2, 0);
            }
        }
    }
}
