using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Runtime.InteropServices;

public class GraphManager : MonoBehaviour
{
    public Carriage subject;                                                    // what object is the graph tracking
    public enum CarriageIDs {zero,one,two};                                     // an ID for the carriage
    public CarriageIDs carriageID;                                              // used for calling javascript to add point values to html table
    public int graphForSec = 10;                                                // how long does the graph get captured in seconds
    public int pointsPerSec = 1;                                                // how many graph points per second are captured
    public List<Vector2> pointVals = new List<Vector2>();                       // a list of values associated with each graph point
    public bool okToGraph = false;                                              // is it ok to start a graph
    private IEnumerator pointTimer;                                             // a coroutine that adds points to the graph
    private float startTime = -1f;                                              // holds the start time of a graph
    private bool graphDrawn = false;                                            // indicates if a grpah has finished drawing
    [DllImport("__Internal")]
    private static extern void AddPointCarriage00(float x, float y);            // add points values to carriage zero
    [DllImport("__Internal")]
    private static extern void AddPointCarriage01(float x, float y);            // add points values to carriage one
    [DllImport("__Internal")]
    private static extern void AddPointCarriage02(float x, float y);            // add points values to carriage two


    public void RestartPointTimer()
    {
        if (pointTimer != null)
        {
            StopCoroutine(pointTimer);
            pointTimer = null;
        }

        startTime = -1f;
        pointTimer = PointTimer();
        StartCoroutine(pointTimer);
    }

    IEnumerator PointTimer()
    {
        // if no graph has started
        if (startTime == -1f)
        {
            AddPoint(Time.time);
            // if the current time from the start time is under the amount of time alotted
        }
        else if (Time.time - startTime < graphForSec)
        {
            AddPoint(Time.time);
        }
        float secsUntilNextPoint = 1f / pointsPerSec;
        yield return new WaitForSeconds(secsUntilNextPoint);
        StopCoroutine(pointTimer);
        pointTimer = null;
        // while the carriage is released
        if (okToGraph)
        {
            // if the last point value captured for time is under the amount of time alotted
            if (pointVals.Count > 0 && pointVals[pointVals.Count - 1].x < graphForSec)
            {
                pointTimer = PointTimer();
                StartCoroutine(pointTimer);
            }
            // graph has finished drawing
            else if (!graphDrawn)
            {
                graphDrawn = true;
                startTime = -1f;
            }
        }
    }

    /// <summary>
    /// Adds a point to the lists for graphing.
    /// </summary>
    private void AddPoint(float _time)
    {
        if (subject != null)
        {
            float xVal = 0f;
            float yVal = 0f;

            if (pointVals.Count == 0)
            {
                // this is the start time
                startTime = _time;
            }

            // ticks along the x axis, starting at left maker and moving right
            xVal = _time - startTime;

            // current capturing x position and dsiplaying it in the vertical (y-axis) of graph
            // SHOULD BUILD IN OPTTIONS HERE
            yVal = subject.startingPosition.x - subject.transform.position.x;

            // create a new point to hold the values
            Vector2 point = new Vector2(xVal, yVal);

            // round float values
            point.x = Mathf.Round(point.x * 100.0f) / 100.0f;
            point.y = Mathf.Round(point.y * 1000.0f) / 1000.0f;

            // adds the point to the list
            pointVals.Add(point);

            // if running webgl 
            if (Application.platform == RuntimePlatform.WebGLPlayer)
            {
                // depending on the carriage ID
                // adds a point to the html table
                switch (carriageID)
                {
                    case CarriageIDs.zero:
                        AddPointCarriage00(point.x, point.y);
                        break;
                    case CarriageIDs.one:
                        AddPointCarriage01(point.x, point.y);
                        break;
                    case CarriageIDs.two:
                        AddPointCarriage02(point.x, point.y);
                        break;
                }
            }
        }
    }

    /// <summary>
    /// Clears the graph and lists that hold assets for the graph.
    /// </summary>
    public void ClearGraph()
    {
        // clear the lists
        pointVals.Clear();

        // the graph has not been drawn
        graphDrawn = false;
    }

    /// <summary>
    /// Resets the graph and sets it ready to draw a new one.
    /// </summary>
    public void ResetGraph()
    {
        ClearGraph();
        if (okToGraph)
        {
            RestartPointTimer();
        }
    }
}
