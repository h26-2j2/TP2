using UnityEngine;
using UnityEngine.InputSystem;

public class Recommencer : MonoBehaviour
{
    
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        // lorsque que le joueur appuie sur la touche "R", le jeu recommence depuis la scène "TitreIntro"
        if (Keyboard.current.rKey.wasPressedThisFrame)
        {
            RecommencerJeu();
        }
    }

    public void RecommencerJeu()
    {
        // Charger la scène "TitreIntro" pour recommencer le jeu
        UnityEngine.SceneManagement.SceneManager.LoadScene("TitreIntro");
    }
}
