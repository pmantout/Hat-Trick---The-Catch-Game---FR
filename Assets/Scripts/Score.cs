using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Score : MonoBehaviour {

    // le champ text qui va afficher le score
    public Text scoreText;
    // la valeur de chaque balle récupérée
    public int ballValue;
    // le score réalisé par le joueur
    private int score;

	// Use this for initialization
	void Start () {
        // on initialise le score à 0
		score  = 0;

        // on met à jour l'affichage du score
        UpdateScore ();
    }

    // OnTriggerEnter2D est appelé quand le Collider2D other entre dans le déclencheur (moteur physique 2D uniquement)
    // ici le script est lancé car il y a un edgeCollider qui est isTrigger quand une balle le touche
    private void OnTriggerEnter2D(Collider2D other)
    {
        // on est au fond du chapeau
        // si c'est une balle, on augmente le score
        // si c'est une bombe qui n'a pa s touché les bords, on diminue le score
        if (other.gameObject.tag == "Ball")
        {
            // on a récupéré une balle dans le chapeau, on augmente le score
            score += ballValue;
            // on met à jour l'affichage du score
            UpdateScore();
        }
        else if (other.gameObject.tag == "Bomb")
        {
            // on a récupéré une bombe pas explosé
            score -= ballValue * 2;
            // on met à jour l'affichage du score
            UpdateScore();
        }
    }


    // OnCollisionEnter2D est appelé quand ce collider2D/rigidbody2D commence à toucher un autre rigidbody2D/collider2D (moteur physique 2D uniquement)
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si c'est une bombe, je mets une pénalité
        if (collision.gameObject.tag == "Bomb")
        {
            score -= ballValue * 2;
            // on met à jour l'affichage du score
            UpdateScore();
        }
    }

    // On ajoute la valeur de score dans le champ texte en haut à gauche
    void UpdateScore () {
        scoreText.text = "Score:\n" + score.ToString();
    }
}
