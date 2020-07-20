using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spring : MonoBehaviour
{
    public GameObject end_A;                                            // a game object for first end of spring
    public GameObject end_B;                                            // a game object for second end of spring
    private List<Vector3> points = new List<Vector3>();                 // a list of Vector3s evenly distributed along a straight line between the end points of the spring
    private Vector3[] circPoints;                                       // an array of Vector3s used to represent the radius of the coil of the spring
    public int numCoils = 1;                                            // how many times does the spring coil around
    public int numPointsPerCoil = 8;                                    // how many point on the circle does one coil around have 
    private float degreesRotPerPoint;                                   // how many degrees does each point rotate
    public int numPoints;                                               // number of total points that make up the spring
    public float radius = 0.05f;                                        // the radius of th spring
    public GameObject pointGO;                                          // a game object to show the evenly distrubuted points in the spring which is then rotated around the coil
    private List<GameObject> pointGOs = new List<GameObject>();         // a list of pointGO game objects
    public GameObject circPointGO;                                      // a game object to represent the the outer circumference of the coil
    private List<GameObject> circPointGOs = new List<GameObject>();     // a list of circPointGO game objects
    public float gauge = 0.05f;                                         // the thickness of the line render of the spring
    public AnimationCurve AC;

    void Start()
    {
        // the number of points is numPointsPerCoil x number of coils
        // then plus two, one for each end
        numPoints = (numCoils * numPointsPerCoil) + 2;

        // define how many points
        circPoints = new Vector3[numPoints];

        // how much does each point rotate
        degreesRotPerPoint = 360 / numPointsPerCoil;

        // the distance beetween the end points
        float totalDist = Vector3.Distance(end_A.transform.position, end_B.transform.position);

        // the distance bewteen points
        float distBetweenPoints = totalDist / (numPoints - 1);


        for (int p = 0; p < numPoints; p++)
        {
            // make the first point at end_A
            if (p == 0)
            {
                points.Add(end_A.transform.position);
                circPoints[p] = end_A.transform.position;
            }
            // make last point at end_B
            else if (p == numPoints - 1)
            {
                points.Add(end_B.transform.position);
                circPoints[p] = end_A.transform.position;
            }
            else
            {
                // create point
                Vector3 direction = end_B.transform.position - end_A.transform.position;
                Vector3 newPoint = end_A.transform.position + (direction.normalized * (distBetweenPoints * p));
                points.Add(newPoint);
                circPoints[p] = newPoint;
                CreatePointObject(newPoint);
                CreateCircPointObject(newPoint);
            }
        }

        LineRenderer LR = GetComponent<LineRenderer>();
        LR.startWidth = gauge;
        LR.endWidth = gauge;
    }

    void Traverse(GameObject obj)
    {
        foreach (Transform child in obj.transform)
        {
            Traverse(child.gameObject);
        }
    }

    
    void Update()
    {
        UpdatePoints();
        DrawLine();
    }
    

    /// <summary>
    /// Create a new game object to represent the evenly distrubuted point.
    /// </summary>
    /// <param name="pos">The position of the evenly distrubuted point.</param>
    private void CreatePointObject(Vector3 pos)
    {
        GameObject gameObject = Instantiate(pointGO, pos, Quaternion.identity);
        gameObject.transform.SetParent(this.transform);
        pointGOs.Add(gameObject);
    }

    /// <summary>
    /// Create a new game object to represent the outer circumference of the coil. Makes it a child of the evenly distrubuted point.
    /// </summary>
    /// <param name="pos">The position of the evenly distrubuted point.</param>
    private void CreateCircPointObject(Vector3 pos)
    {
        // move the position away from the evenly distributed point far enough to represent the radius of the coil
        Vector3 newPos = pos + (Vector3.up * radius);
        GameObject gameObject = Instantiate(circPointGO, newPos, Quaternion.identity);
        gameObject.transform.SetParent(pointGOs[pointGOs.Count - 1].transform);
        circPointGOs.Add(gameObject);
    }

    /// <summary>
    /// As the spring move, this updates all the point evenly across the stretch of the spring.
    /// </summary>
    void UpdatePoints()
    {
        // the distance beetween the end points
        float totalDist = Vector3.Distance(end_A.transform.position, end_B.transform.position);

        // the evenly distrubuted distance bewteen points
        float distBetweenPoints = totalDist / (numPoints - 1);

        // start the rotaion at zero
        float rot = 0;

        // there is no point game object at the start or end, those are represented by end_A and end_B
        // so we must use a seperate counter
        int pointGOIndex = 0;

        // but we still need to update the ends
        points[0] = end_A.transform.position;
        points[numPoints - 1] = end_B.transform.position;
        circPoints[0] = end_A.transform.position;
        circPoints[numPoints - 1] = end_B.transform.position;

        // divide numpoints in half
        int halfWay = Mathf.RoundToInt(numPoints / 2);

        // loop through points list, skip ends
        for (int p = 1; p < numPoints - 1; p++)
        {

            float percOfTotalDist = ((p * 100f) / (numPoints - 1)) / 100f;
            Vector3 direction = end_B.transform.position - end_A.transform.position;

            // without animation curve applied, for even distrubuted point the entire length of spring
            Vector3 newPos = end_A.transform.position + (direction.normalized * ((totalDist * percOfTotalDist)));

            // with the animation curve applied, for tension on ends
            //Vector3 newPos = end_A.transform.position + (direction.normalized * ((totalDist * percOfTotalDist) * AC.Evaluate(percOfTotalDist)));

            points[p] = newPos;
            pointGOs[pointGOIndex].transform.position = newPos;

            // applies rotation to each point
            pointGOs[pointGOIndex].transform.localEulerAngles = new Vector3(rot, 0, 0);

            // rotates each consecutive point clockwise
            rot -= degreesRotPerPoint;

            // update these with where the objects that represnt the circumference of the coil
            circPoints[p] = circPointGOs[pointGOIndex].transform.position;

            // advance counter
            pointGOIndex += 1;

            // place the spring between the ends
            Vector3 midPoint = (end_A.transform.position + end_B.transform.position) / 2;
            transform.position = midPoint;
        }
    }

    void DrawLine()
    {
        LineRenderer LR = GetComponent<LineRenderer>();
        LR.enabled = true;
        LR.positionCount = circPoints.Length;
        LR.SetPositions(circPoints);
    }
}
