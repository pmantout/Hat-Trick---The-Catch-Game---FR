using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Restart : MonoBehaviour {

    // Ici la routine doit être public pour pouvoir l'appeler de l'extérieur
	public void RestartGame ()
    {
        // on recharge la scene courante
        SceneManager.LoadScene( SceneManager.GetActiveScene().buildIndex, LoadSceneMode.Single);
    }
}
