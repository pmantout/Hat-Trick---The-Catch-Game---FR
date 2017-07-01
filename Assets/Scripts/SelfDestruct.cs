using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelfDestruct : MonoBehaviour {

    public float lifeTime; // le temps en secondes avant que l'objet ne se détruisse

	// Dès l'initialisation, on dit que l'objet auquel
    // est attaché ce script va se détruire
	void Start () {
        Destroy(gameObject, lifeTime);
	}
}
