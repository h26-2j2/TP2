using UnityEngine;
using TMPro;
using UnityEngine.UI;
using UnityEngine.InputSystem;
using Unity.VisualScripting;
using UnityEngine.SceneManagement;
using System.Threading.Tasks;
using Unity.VisualScripting.Dependencies.NCalc;

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
    public bool GameOver = false; // Variable pour suivre l'état de fin de jeu
    public bool EnAttente = false;

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

        // Alterner les réponses pour éviter que la bonne réponse soit toujours au même endroit
        int randomIndex = Random.Range(0, 3);
        if (randomIndex == 0)
        {
            reponse1.text = reponseCorrecte.ToString();
            reponse2.text = reponseFausse1.ToString();
            reponse3.text = reponseFausse2.ToString();
        }
        else if (randomIndex == 1)
        {
            reponse1.text = reponseFausse1.ToString();
            reponse2.text = reponseCorrecte.ToString();
            reponse3.text = reponseFausse2.ToString();
        }
        else
        {
            reponse1.text = reponseFausse1.ToString();
            reponse2.text = reponseFausse2.ToString();
            reponse3.text = reponseCorrecte.ToString();
        }

        // Afficher les points de vie
        pointDeVieText.text = $"Points de vie : {pointDeVie}";


        // initialiser le nombre de questions restantes
        nombreDeQuestions = 5; // Par exemple, le joueur doit répondre à 5 questions pour gagner






    }

    // Update is called once per frame
    void Update()
    {
        if (tempsInitial > 0)
        {
            // Ajouter un temps limité pour répondre à l'équation
            tempsInitial -= Time.deltaTime;
            texteTemps.text = $"{tempsInitial:F1} S";
        }


        if (tempsInitial <= 0 && !GameOver && EnAttente == false) // si le temps atteint 0
        {
            // Afficher un message d'échoue et pert une 1 vie
            EquationTexte.text = "Temps écoulé !";

            pointDeVieText.text = $"Points de vie : {pointDeVie}"; // Met à jour le texte des points de vie
            tempsInitial = 0f; // Assure que le temps ne devient pas négatif

            // Arrête le temps pour un couert instantt
            EnAttente = true;
            pointDeVie--; // enleve une vie

            // Attendre un court instant avant de générer une nouvelle équation, réinitilaiser le temps et continuer le jeu


            Invoke("Start", 3f); // Appelle la méthode Start après 3 secondes
            Invoke("ReinitialiserTemps", 3f);



        }
        if (pointDeVie <= 0) // si les points de vie atteignent 0
        {
            // Afficher un message de fin de jeu
            GameOverText.text = "Partie Terminée !";

            // Le temps s'arrête et le jeu ne génère plus de nouvelles équations et désactive les interactions du joueur
            enabled = false; // Désactive ce script pour arrêter les mises à jour
            GameOver = true; // Met à jour l'état de fin de jeu
        }

        // si le joueur répond correctement à chacune des questions, afficher un message de victoire, arrêter le temps, attendre avant que le jeu change de scène ou niveau
        if (nombreDeQuestions <= 0)
        {
            GameWinText.text = "Félicitations, vous avez gagné !";
            enabled = false; // Désactive ce script pour arrêter les mises à jour
            GameOver = true; // Met à jour l'état de fin de jeu
            Task.Delay(2000).ContinueWith(_ => SceneManager.LoadScene("niveau2")); // Charge la scène suivante (niveau 2) après un délai de 2 secondes
        }
    }

    public void BoutonClique()
    { // créer une fonction pour vérifier la réponse du joueur

        // créer une fonction pour vérifier la réponse du joueur
        if (reponse1.text == (nombre1 + nombre2).ToString()) // si la réponse1 est correcte
        {
            nombreDeQuestions--; // enleve une question du total
            EquationTexte.text = "Bonne réponse !";
            EnAttente = true;
            Start(); // génère une nouvelle équation
            ReinitialiserTemps();

        }
        else if (reponse2.text == (nombre1 + nombre2).ToString()) // si la réponse2 est correcte
        {
            nombreDeQuestions--; // enleve une question du total
            EquationTexte.text = "Bonne réponse !";
            EnAttente = true;
            Start(); // génère une nouvelle équation
            ReinitialiserTemps();

        }
        else if (reponse3.text == (nombre1 + nombre2).ToString()) // si la réponse3 est correcte
        {
            nombreDeQuestions--; // enleve une question du total
            EquationTexte.text = "Bonne réponse !";
            EnAttente = true;
            Start(); // génère une nouvelle équation
            ReinitialiserTemps();
        }
        else
        {
            // si la réponse est incorrecte, afficher un message d'erreur et perdre une vie
            EquationTexte.text = "Mauvaise réponse !";
            pointDeVie--; // enleve une vie
            pointDeVieText.text = $"Points de vie : {pointDeVie}"; // Met à jour le texte des points de vie

            // Attendre un court instant avant de générer une nouvelle équation, réinitilaiser le temps et continuer le jeu
            EnAttente = true;
            Invoke("Start", 3f); // Appelle la méthode Start après 2 secondes
            Invoke("ReinitialiserTemps", 3f); // Réinitialiser le temps après 3 secondes
        }
    }

    void ReinitialiserTemps()
    {
        EnAttente = false;
        tempsInitial = 10f; // Réinitialiser le temps pour la prochaine équation
    }
}
