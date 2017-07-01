using UnityEngine;
using System.Collections;

public class SetParticleSortingLayer : MonoBehaviour
{
	public string sortingLayerName;  // Le nom du calque de tri qui va contenir les particules
	public int sortingOrder; // l'ordre de priorité au sein de ce calque


	void Start ()
	{
        // on définit le calque de tri utilisé pour le système de particules
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingLayerName = sortingLayerName;
		GetComponent<ParticleSystem>().GetComponent<Renderer>().sortingOrder = sortingOrder;
	}
}
