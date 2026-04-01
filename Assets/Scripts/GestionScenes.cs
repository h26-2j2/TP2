using UnityEngine;
using UnityEngine.SceneManagement;

public class GestionScenes : MonoBehaviour
{
    public string sceneIntro = "TitreIntro";
    public string sceneJeu = "niveau1";

    public void DemarrerJeu()
    {
        SceneManager.LoadScene(sceneJeu);
    }
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
