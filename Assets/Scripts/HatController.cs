using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HatController : MonoBehaviour
{

    public Camera cam; // la caméra de la scène principale
    private Rigidbody2D rg2d; // le rigidBody 2D attaché au chapeau
    private float maxWidth; // la largueur maximale de l'écran en worldSpace pas en pixels
    private bool canControl; // peut-on déplacer le chapeau (a t'on commencé le jeu ?)

    // Start est appelé juste avant qu'une méthode Update soit appelée pour la première fois
    private void Start()
    {

        // je récupère la caméra principale, si elle n'a pas été défini avant
        if (cam == null)
        {
            cam = Camera.main;
        }

        // au début, je ne joue pas, donc je ne déplace pas le chapeau
        canControl = false;

        // on récupère le rigidBody2D attaché au chapeau
        rg2d = GetComponent<Rigidbody2D>();
        // on récupère le renderer qui fait qu'un objet apparait sur la scène
        Renderer ren = GetComponent<Renderer>();

        // la position du coin en haut de l'écran (en pixels). la profondeur en Z est sans importance
        Vector3 upperCorner = new Vector3(Screen.width, Screen.height, 0.0f);
        // la largeur maximale à droite et à gauche (l'écran est centré au milieu)
        Vector3 targetWidth = cam.ScreenToWorldPoint(upperCorner);
        // on récupère la largueur du chapeau
        // extends of the bound est toujours la moitié de la taille englobant l'objet
        float hatWidth = ren.bounds.extents.x;
        // on récupère juste la coordonnée en X
        maxWidth = targetWidth.x - hatWidth;
    }


    // Cette fonction est appelée pour chaque trame avec un taux fixe, si le MonoBehaviour est activé
    // FixedUpdate doit être utilisé à la place de Update quand on utilise des Rigidbody
    private void FixedUpdate()
    {
        // si je peux déplacer le chapeau
        if (canControl)
        {
            // transforme la position en pixels du curseur/doigt en coordonnées écran (indépendant de la résolution)
            Vector3 rawPosition = cam.ScreenToWorldPoint(Input.mousePosition);

            // la nouvelle position du chapeau, on ne bouge que suivant X
            Vector3 targetPosition = new Vector3(rawPosition.x, 0.0f, 0.0f);

            // on se limite à droite et à gauche pour ne pas sortir de l'écran (possible car le chapeau est au milieu de l'écran)
            float targetWidth = Mathf.Clamp(targetPosition.x, -maxWidth, maxWidth);
            targetPosition.x = targetWidth;
            // on bouge le chapeau au nouvel X
            rg2d.MovePosition(targetPosition);
        }
    }


    /// <summary>
    /// Change l'état du flag qui permet de déplacer le drapeau
    /// routine qui à vocation d'être appelée par une routine externe.
    /// </summary>
    /// <param name="toggle">valide ou non le déplacement du chapeau</param>
    public void ToggleControl(bool toggle)
    {
        canControl = toggle;
    }

}
