using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class atelier: MonoBehaviour {


    //  CUTTING PLANE
    /* Returns all elements in Selection within this cutting splane
    */
    public static List<DataObject> cuttingPlane(
        Vector3 p0, Vector3 p1, Vector3 p2,
        Selection s, 
        float minDistance)
    {

        Plane plane = new Plane(p0, p1, p2);

        // test if object is intersecting the cutting plane
        List<UnityEngine.GameObject> visualElements = s.getVisualElements();
        List<DataObject> dataObjects = s.getDataObjects();
        List<DataObject> intersectedDataObjects = new List<DataObject>();
        Vector3 pos;
        float distance;
        for (int i = 0; i < visualElements.Count; i++)
        {
            pos = visualElements[i].transform.position;
            distance = plane.GetDistanceToPoint(pos);
            if(Mathf.Abs(distance) <= minDistance){
                intersectedDataObjects.Add(dataObjects[i]);
            }
        }

        return intersectedDataObjects;

    }

    /* Returns all elements in the selection 
    /* within a certain range of the the cursor.
    */
    public static List<DataObject> point(
        Vector3 cursor,
        Selection s, 
        float minDistance)
    {

        // test if object is intersecting the cutting plane
        List<UnityEngine.GameObject> visualElements = s.getVisualElements();
        List<DataObject> dataObjects = s.getDataObjects();
        List<DataObject> intersectedDataObjects = new List<DataObject>();
        Vector3 pos;
        float distance;
        for (int i = 0; i < visualElements.Count; i++)
        {
            pos = visualElements[i].transform.position;
            distance = Vector3.Distance(cursor, pos);
            if(Mathf.Abs(distance) <= minDistance){
                intersectedDataObjects.Add(dataObjects[i]);
            }
        }

        return intersectedDataObjects;

    }

     /* Returns all elements in the selection 
    /* within a certain range of the the cursor.
    */
    public static List<DataObject> cuttingLine(
        Vector3 line1,
        Vector3 line2,
        Selection s, 
        float minDistance)
    {
        Ray ray = new Ray(line1, line2 - line1) ;

        // test if object is intersecting the cutting plane
        List<UnityEngine.GameObject> visualElements = s.getVisualElements();
        List<DataObject> dataObjects = s.getDataObjects();
        List<DataObject> intersectedDataObjects = new List<DataObject>();
        Vector3 pos;
        float distance;
        for (int i = 0; i < visualElements.Count; i++)
        {
            pos = visualElements[i].transform.position;
            distance = Vector3.Cross(ray.direction, pos - ray.origin).magnitude;
            print("distance: "  + distance);

            if(Mathf.Abs(distance) <= minDistance){
                intersectedDataObjects.Add(dataObjects[i]);
            }
        }

        return intersectedDataObjects;

    }


}