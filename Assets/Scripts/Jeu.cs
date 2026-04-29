using UnityEngine;
using TMPro;

public class GenereEquation : MonoBehaviour
{   
    public int nombreDeQuestions = 5; // Nombre total de questions à poser
    public TMP_Text GameOverText; // Texte pour afficher le message de fin de jeu
    public TMP_Text GameWinText; // Texte pour afficher le message de victoire
    public TMP_Text question; // Texte pour afficher la question
    public TMP_Text reponse1; // Exemple de réponse 1
    public TMP_Text reponse2; // Exemple de réponse 2
    public TMP_Text reponse3; // Exemple de réponse 3
    public TMP_Text EquationTexte; // Texte pour afficher l'équation
    public TMP_Text texteTemps; // Texte pour afficher le temps restant
    public TMP_Text pointDeVieText; // Texte pour afficher les points de vie
    public float tempsInitial = 10f; // Temps initial pour répondre à l'équation
    public int nombre1; // Premier nombre de l'équation
    public int nombre2; // Deuxième nombre de l'équation
    public int pointDeVie = 3; // Exemple de points de vie
    

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        // Générer un nombre aléatoire entre 0 et 10 pour les deux variables
        nombre1 = Random.Range(0, 11);
        nombre2 = Random.Range(0, 11);

        // Afficher l'équation dans le texte
        EquationTexte.text = $"{nombre1} + {nombre2} = ?";

        // Génerer les reponses possibles
        int reponseCorrecte = nombre1 + nombre2;
        int reponseFausse1 = reponseCorrecte + Random.Range(1, 5); // Réponse fausse 1
        int reponseFausse2 = reponseCorrecte - Random.Range(1, 5); // Réponse fausse 2

        // Afficher les réponses dans les textes
        reponse1.text = reponseCorrecte.ToString();
        reponse2.text = reponseFausse1.ToString();
        reponse3.text = reponseFausse2.ToString();

        // Afficher les points de vie
        pointDeVieText.text = $"Points de vie : {pointDeVie}";
        

      
        

        
        
        
    }

    // Update is called once per frame
    void Update()
    {

        // Ajouter un temps limité pour répondre à l'équation
        tempsInitial -= Time.deltaTime;
        texteTemps.text = $"{tempsInitial:F1} S";


        // lorsque le joueur clique sur une réponse, vérifier si c'est la bonne réponse, si c'est la bonne réponse, afficher un message de victoire et générer une nouvelle équation, sinon afficher un message d'échoue et pert une 1 vie
        
        
        if (tempsInitial <= 0) // si le temps atteint 0
        {
            // Afficher un message d'échoue et pert une 1 vie
            EquationTexte.text = "Temps écoulé !";
            pointDeVie--; // enleve une vie
            pointDeVieText.text = $"Points de vie : {pointDeVie}"; // Met à jour le texte des points de vie
            
            // Arrête le temps pour un couert instantt
            tempsInitial = 0f; // Assure que le temps ne devient pas négatif

            // Attendre un court instant avant de générer une nouvelle équation, réinitilaiser le temps et continuer le jeu

            tempsInitial = 10f; // Réinitialiser le temps pour la prochaine équation
            Invoke("Start", 2f); // Appelle la méthode Start après 2 secondes
            
            

        }
        else if (pointDeVie <= 0) // si les points de vie atteignent 0
        {
            // Afficher un message de fin de jeu
            GameOverText.text = "Game Over !";

            // Le temps s'arrête et le jeu ne génère plus de nouvelles équations
            enabled = false; // Désactive ce script pour arrêter les mises à jour

        }
        {
        
    }
}}
