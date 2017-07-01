using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class GameController : MonoBehaviour {

    public Camera cam; // la caméra de la scène principale
    public GameObject[] balls; // un tableau de GameObject contenant des balles et des bombes
    public float timeLeft; // le temps qu'il reste pour jouer

    public Text timerText; // le texte qui va contenir le temps restant

    private float maxWidth; // la largueur maximale de l'écran en worldSpace pas en pixels

    public GameObject gameOverText; // le texte de fin de partie à la fin du jeu
    public GameObject restartButton; // le bouton de redémarrage à la fin du jeu

    public GameObject splashScreen; // l'image au démarrage du jeu
    public GameObject startButton; // le bouton de lancement du jeu

    public HatController hatController; // lien à la sous-routine qui contrôle le mvt du chapeau

    private bool playing; // on est ou non en train de jouer

    // Start est appelé juste avant qu'une méthode Update soit appelée pour la première fois
    private void Start()
    {
        // si je n'ai pas déjà récupéré la caméra principale
        if (cam == null) {
            cam = Camera.main;
        }

        // on ne joue pas au début
        playing = false;

        // on récupère le renderer de la première balle qui fait qu'un objet apparait sur la scène
        Renderer renBall = balls[0].GetComponent<Renderer>();

        // la position du coin en haut de l'écran (en pixels). la profondeur en Z est sans importance
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        // la largeur maximale à droite et à gauche (l'écran est centré au milieu)
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        // on récupère la largueur de la balle
        // extends of the bound est toujours la moitié de la taille englobant l'objet
        float ballWidth = renBall.bounds.extents.x;
        // on récupère juste la coordonnée en X
        maxWidth = targetWidth.x - ballWidth;

        // on inscrit le temps restant dans le champ texte
        UpdateText();
    }

    // Cette fonction est appelée pour chaque trame avec un taux fixe, si le MonoBehaviour est activé
    // FixedUpdate doit être utilisé à la place de Update quand on utilise des Rigidbody
    // l'intervalle de temps entre chaque appel est toujours constant contrairement à Update
    private void FixedUpdate()
    {
        // si je suis en train de jouer
        // je mets à jour l'affichage
        if (playing)
        {
            // le temps du jeu diminue à chaque appel
            timeLeft -= Time.deltaTime;

            // on ne peut pas avoir un temps négatif
            if (timeLeft < 0.0f)
            {
                timeLeft = 0.0f;
            }

            // on inscrit le temps restant dans le champ texte
            UpdateText();
        }
    }

    // attaché sur le buton de démarrage du jeu
    public void StartGame()
    {
        // on cache l'écran de démarrage
        splashScreen.SetActive(false);
        // on cache le bouton de démarrage du jeu
        startButton.SetActive(false);

        // on valide le déplacement du chapeau
        hatController.ToggleControl(true);
        
        // on lance la co-routine
        StartCoroutine(Spawn());
    }

    IEnumerator Spawn()
    {
        // au début du jeu, on donne 2 secondes au joueur
        // avant de lancer le premier ballon
        yield return new WaitForSeconds(2.0f);

        // puis on dit que l'on lance le jeu
        playing = true;

        // la co-routine fait une boucle infinie
        // mais chaque yield return la fait quitter périodiquement

        // tant que le temps du jeu n'est pas fini
        while (timeLeft > 0.0f)
        {
            // on récupère une balle ou une bombe
            GameObject ball = balls[Random.Range(0, balls.Length)];

            // la position de démarrage de la balle
            // en X, on choisit aléatoirement sur toute la largueur de l'écran
            // en Y, on reste où on est car on a mis le script tout en haut de l'écran
            // en Z, on est à 0.0
            Vector3 spawnPosition = new Vector3(
                Random.Range(-maxWidth, maxWidth),
                transform.position.y,
                0.0f
            );

            // la rotation de la balle (identitée)
            Quaternion spawnRotation = Quaternion.identity;
            // création de la balle en haut de l'écran
            Instantiate(ball, spawnPosition, spawnRotation);

            // on quitte la boucle infinie et on revient entre une ou deux secondes
            yield return new WaitForSeconds(Random.Range(1.0f,balls.Length));
        }

        // il n'y a plus de temps restant, on affiche le bouton et le texte de redémarrage

        // on affiche le texte gameOver au bout de 2 seconds
        yield return new WaitForSeconds(2.0f);
        gameOverText.SetActive(true);
        // on affiche le bouton de restart au bout de 2 seconds
        yield return new WaitForSeconds(2.0f);
        restartButton.SetActive(true);
    }


    void UpdateText()
    {
        // on inscrit le temps restant dans le champ texte
        timerText.text = "Time Left:\n" + (Mathf.RoundToInt(timeLeft)).ToString();
    }

}