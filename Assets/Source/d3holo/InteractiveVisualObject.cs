using UnityEngine;

public class InteractiveVisualObject : MonoBehaviour {

    Ray ray;
    RaycastHit hit;
     
    void Update()
    {
        ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        if(Physics.Raycast(ray, out hit))
        {
            // print (hit.collider.name);
            print("HIT!");
        }
    }



}