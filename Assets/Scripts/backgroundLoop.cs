using UnityEngine;

public class BackgroundLoop : MonoBehaviour
{
    public float speed = 2f; // Vitesse de déplacement
    public float resetPositionX; // Position X où l’image doit être replacée à droite
    public float startPositionX; // Position X de départ à droite

    void Update()
    {
        // Déplacement vers la gauche
        transform.Translate(Vector3.left * speed * Time.deltaTime);

        // Si l’image est complètement sortie de l’écran à gauche
        if (transform.position.x <= resetPositionX)
        {
            // On la replace à droite
            Vector3 newPos = transform.position;
            newPos.x = startPositionX;
            transform.position = newPos;
        }
    }
}
