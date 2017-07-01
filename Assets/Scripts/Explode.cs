using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Explode : MonoBehaviour
{

    public GameObject explosion; // l'explosion qui va arriver quand la bombe touche le chapeau
    public ParticleSystem[] effects; // le tableau contenant les 2 effets

    // OnCollisionEnter2D est appelé quand ce collider2D/rigidbody2D commence à toucher un autre rigidbody2D/collider2D (moteur physique 2D uniquement)
    // il est lié à la bombe qui va détecter si elle touche le chapeau
    private void OnCollisionEnter2D(Collision2D collision)
    {
        // si l'objet de la collision contient le tag "Hat"
        if (collision.gameObject.tag == "Hat")
        {
            // à l'endroit du choc (position et rotation) on met une explosion
            Instantiate(explosion, transform.position, transform.rotation);

            // on ne détruit pas tout de suite la bombe
            // car sinon les particules s'arreteraient net
            // on va arrêter effet par effet
            foreach (var effect in effects)
            {
                // on coupe l'effet de son parent
                effect.transform.parent = null;
                // on arrête l'animation des particules
                effect.Stop();

                // on détruit l'objet proprement dit, on attend 1 seconde avant de le faire
                Destroy(effect.gameObject, 1.0f);
            }

            // on détruit la bombe qui contenanit les 2 effets
            Destroy(gameObject);
        }
    }

}