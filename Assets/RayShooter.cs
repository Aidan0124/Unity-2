using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class NewBehaviourScript : MonoBehaviour
{
    private Camera cam;
    void Start()
    {
        cam = GetComponent<Camera>(); 

        Cursor.lockState = CursorLockMode.Locked;
        Cursor.visible = false; 
    }
    void Update()
    {
        if (Input.GetMouseButtonDown(0))
        {
            Vector3 point = new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0);
            Ray ray = cam.ScreenPointToRay(point);
            RaycastHit hit;
            if (Physics.Raycast(ray, out hit))
            {
                GameObject hitObject = hit.transform.gameObject;
                ReactiveTarget target = hitObject.GetComponent<ReactiveTarget>();
                if (target != null)
                {
                    target.ReactToHit();
                }
                else
                {
                    StartCoroutine(SphereIndicator(hit.point));
                }
            }
        }
    }

    void OnGUI()
    {
        int size = 40;
        float posX = cam.pixelWidth / 2 - size / 4;
        float posY = cam.pixelHeight / 2 - size / 2;
        GUI.Label(new Rect(posX, posY, size, size), "*");
            
        Ray ray = cam.ScreenPointToRay(new Vector3(cam.pixelWidth / 2, cam.pixelHeight / 2, 0));
        RaycastHit hit;
    
    if (Physics.Raycast(ray, out hit))
        {
        string coordinatesText = $"Hit Coordinates: {hit.point.x:F2}, {hit.point.y:F2}, {hit.point.z:F2}"; 
        //formats the coordinates to 2 decimal places
        GUI.Label(new Rect(400, 200, 200, 20), coordinatesText); //Displays the cordinates as text on screen
        }
    }

    private IEnumerator SphereIndicator(Vector3 pos)
    {
        GameObject sphere = GameObject.CreatePrimitive(PrimitiveType.Sphere);
        sphere.transform.position = pos;
        
        yield return new WaitForSeconds(1);
        
        Destroy(sphere);
    }
}
