using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DestroyOnContact : MonoBehaviour
{
    // un des 2 triggers posés sur le chapeau qui se déclenche
    // quand la balle tombe au fond du chapeau
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // on détruit le gameObject qui est attaché au sprite qui a fait la collision
        Destroy(collision.gameObject);
    }
}