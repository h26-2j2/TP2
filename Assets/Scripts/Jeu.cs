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
    public TMP_Text reponse1; // Exemple de réponse 1
    public TMP_Text reponse2; // Exemple de réponse 2
    public TMP_Text reponse3; // Exemple de réponse 3
    public Button bouton1; // Bouton de réponse 1
    public Button bouton2; // Bouton de réponse 2
    public Button bouton3; // Bouton de réponse 3


    public TMP_Text EquationTexte; // Texte pour afficher l'équation
    public TMP_Text texteTemps; // Texte pour afficher le temps restant
    public TMP_Text pointDeVieText; // Texte pour afficher les points de vie
    public float tempsInitial = 10f; // Temps initial pour répondre à l'équation
    public int nombre1; // Premier nombre de l'équation
    public int nombre2; // Deuxième nombre de l'équation
    public int pointDeVie = 3; // Exemple de points de vie
    public bool GameOver = false; // Variable pour suivre l'état de fin de jeu
    public bool EnAttente = false; // Variable pour suivre si le jeu est en attente entre les questions
    public string prochaineScene = "niveau2"; // Scène à charger après la victoire
    public float tempsParNiveau = 10f; // Temps disponible par niveau
    public int viesParNiveau = 3; // Vies disponibles par niveau

    public Musique musique;

    




    public BackgroundLoop backgroundLoop; // Référence au script BackgroundLoop

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        GameOverText.enabled = false;
        GameWinText.enabled = false;
        InitialiserJeu();
        NouvelleEquation();
        
    }

    void InitialiserJeu()
    {
        // initialiser le nombre de questions restantes
        nombreDeQuestions = 5; // Par exemple, le joueur doit répondre à 5 questions pour gagner
        pointDeVie = viesParNiveau; // Utiliser les vies définies par niveau
        GameOver = false;
        EnAttente = false;
        tempsInitial = tempsParNiveau; // Utiliser le temps défini par niveau
    }

    void NouvelleEquation()
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
        SetButtonsInteractable(true);
    }

    void SetButtonsInteractable(bool etat)
    {
        if (bouton1 != null) bouton1.interactable = etat;
        if (bouton2 != null) bouton2.interactable = etat;
        if (bouton3 != null) bouton3.interactable = etat;
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


            Invoke("NouvelleEquation", 3f); // Appelle la méthode NouvelleEquation après 3 secondes
            Invoke("ReinitialiserTemps", 3f);



        }

        if (pointDeVie <= 0) // si les points de vie atteignent 0
        {
            musique.ArreterMusique();
            // Afficher un message de fin de jeu
            GameOverText.enabled = true; // Affiche le texte de fin de jeu
            GameOverText.text = "Partie Terminée ! Appuyez sur (R) pour recommencer.";
            // Le temps s'arrête, l'arriere plan arrete de bouger, la musique arrete de jouer et le jeu ne génère plus de nouvelles équations et désactive les interactions du joueur
            backgroundLoop.enabled = false; // Désactive le script BackgroundLoop pour arrêter le mouvement de l'arrière-plan
            enabled = false; // Désactive ce script pour arrêter les mises à jour
            GameOver = true; // Met à jour l'état de fin de jeu
            SetButtonsInteractable(false); // Désactive les boutons de réponse
            
        }

        // si le joueur répond correctement à chacune des questions, il gagne la partie
        if (nombreDeQuestions <= 0)
        {
            Invoke("GameWin", 0f); // Appelle la méthode GameWin immédiatement
        }
    }

    // Fonction pour gérer le clic sur les boutons de réponse
    public void BoutonClique(int index)
    {
        if (EnAttente)
            return;

        string texteChoisi = index == 1 ? reponse1.text :
                             index == 2 ? reponse2.text :
                                           reponse3.text;
        string bonneReponse = (nombre1 + nombre2).ToString();

        SetButtonsInteractable(false);
        EnAttente = true;

        if (texteChoisi == bonneReponse)
        {
            EquationTexte.text = "Bonne réponse !";
            nombreDeQuestions--;
            Invoke("NouvelleEquation", 3f);
            Invoke("ReinitialiserTemps", 3f);
        }
        else
        {
            EquationTexte.text = "Mauvaise réponse !";
            pointDeVie--;
            pointDeVieText.text = $"Points de vie : {pointDeVie}";
            nombreDeQuestions--; // Compte la question comme posée même si la réponse est incorrecte
            Invoke("NouvelleEquation", 3f);
            Invoke("ReinitialiserTemps", 3f);
        }
    }

    // Fonction pour gérer la victoire du joueur
    void GameWin()
    {
        GameWinText.text = "Félicitations, vous avez gagné ! Chargement du niveau suivant...";
        GameWinText.enabled = true; // Affiche le texte de victoire
        enabled = false; // Désactive ce script pour arrêter les mises à jour
        GameOver = true; // Met à jour l'état de fin de jeu
        Invoke("ChangerScene", 3f); // Charge la scène suivante après 3 secondes
    }

    void ChangerScene()
    {
        SceneManager.LoadScene(prochaineScene);
    }

    // Fonction pour réinitialiser le temps après chaque question
    void ReinitialiserTemps()
    {
        EnAttente = false;
        tempsInitial = tempsParNiveau; // Réinitialiser le temps pour la prochaine équation
        SetButtonsInteractable(true);
    }
}