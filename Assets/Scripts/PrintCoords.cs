// Convert the 2D position of the mouse into a
// 3D position.  Display these on the game window.

using UnityEngine;

public class PrintCoords : MonoBehaviour
{
    // l'IMGUI (Immediate Mode GUI) est utilisé en face de développement pour afficher une interface
    void OnGUI()
    {
        Vector3 p = new Vector3();
        Camera c = Camera.main;
        Event e = Event.current;
        Vector2 mousePos = new Vector2();

        // on récupère la position souris en pixels de cet évènement
        // on remarque la postion en Y est inversé
        mousePos.x = e.mousePosition.x;
        mousePos.y = c.pixelHeight - e.mousePosition.y;

        // on transforme les coordonnées écran vers les coordonnées du monde
        p = c.ScreenToWorldPoint(new Vector3(mousePos.x, mousePos.y, c.nearClipPlane));

        // affichage des résultats
        GUILayout.BeginArea(new Rect(20, 70, 250, 170));
        GUILayout.Label("Screen pixels: " + c.pixelWidth + ":" + c.pixelHeight);
        GUILayout.Label("Mouse position: " + mousePos);
        GUILayout.Label("World position: " + p.ToString("F3"));
        GUILayout.EndArea();
    }
}