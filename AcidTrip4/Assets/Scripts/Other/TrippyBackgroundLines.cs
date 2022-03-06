using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TrippyBackgroundLines : MonoBehaviour
{
    // Makes trippy lines that wobble and move in a certain direction.
    // Spawns them from this object's position. This object should be in the bottom left of the screen.

    List<GameObject> lines = new List<GameObject>();

    bool moveLines = true; // If false, stop moving lines. Used by ToggleLines(), which is used by the button that disables the background.

    public enum Orientation { horizontal, vertical };
    public Orientation orientation; // Are the lines flat or do they go upwards?

    // Line creation.
    public int numOfLines;
    public float spacing; // The distance between each line.

    // Parameters of the lines' makeup.
    public float lineLength;
    public int lineGranulation; // The number of segments in each line. Shouldn't be <2.
    float distanceBetweenPoints;

    // Visual parameters.
    public float width;
    public Color color;
    public Material material;

    // Wobble parameters.
    public float wobbleSpeed;
    public float wobbleAmplitude;

    // Translation parameters.
    public float lineHorizontalSpeed;
    public float lineVerticalSpeed;
    public float recallDistance; // The distance at which to recall lines.

    float timer = 0;

    void Start()
    {
        distanceBetweenPoints = lineLength / (lineGranulation - 1);

        // Make numOfLines lines.
        for (int i = 0; i < numOfLines; i++)
        {
            // Spawn the line object.
            GameObject newLine = new GameObject("BG Line");
            newLine.transform.position = transform.position;
            newLine.transform.parent = transform;
            lines.Add(newLine);

            // Displace the line object, depending on orientation.
            switch (orientation)
            {
                case Orientation.horizontal: // If lines are horizontal, displace them upwards.
                    newLine.transform.localPosition += new Vector3(0, spacing * i, 0);
                    break;
                case Orientation.vertical: // If lines are vertical, displace them rightwards.
                    newLine.transform.localPosition += new Vector3(spacing * i, 0, 0);
                    break;
            }

            // Initialize line renderer.
            LineRenderer newLineRenderer = newLine.AddComponent<LineRenderer>();
            newLineRenderer.positionCount = lineGranulation;
            newLineRenderer.useWorldSpace = false;

            newLineRenderer.startColor = color; newLineRenderer.endColor = color;
            newLineRenderer.startWidth = width; newLineRenderer.endWidth = width;
            newLineRenderer.material = material;
            newLineRenderer.sortingLayerName = "Background";

            // Set the position of each point in the line, depending on orientation.
            switch (orientation)
            {
                case Orientation.horizontal:
                    for (int j = 0; j < lineGranulation; j++) // Starting from the origin, add distanceBetweenPoints to each point's x-displacement.
                    {
                        newLineRenderer.SetPosition(j, new Vector3(j * distanceBetweenPoints, 0, 0));
                    }
                    break;
                case Orientation.vertical:
                    for (int j = 0; j < lineGranulation; j++) // Starting from the origin, add distanceBetweenPoints to each point's y-displacement.
                    {
                        newLineRenderer.SetPosition(j, new Vector3(0, j * distanceBetweenPoints, 0));
                    }
                    break;
            }
        }
    }

    void Update()
    {
        if (moveLines)
        {
            foreach (GameObject line in lines)
            {
                LineRenderer lineRenderer = line.GetComponent<LineRenderer>();

                // Wobble the line in a sine wave.
                for (int i = 0; i < lineRenderer.positionCount; i++)
                {
                    float displacement = Mathf.Sin((timer + (i * distanceBetweenPoints)) * wobbleSpeed) * wobbleAmplitude;

                    Vector3 oldPos = lineRenderer.GetPosition(i);
                    Vector3 newPos;

                    switch (orientation) // Completely replaces the displaces axis with the new sine-derived value.
                    {
                        case Orientation.horizontal: // Wobble a horizontal line vertically.
                            newPos = new Vector3(oldPos.x, displacement, oldPos.z);
                            lineRenderer.SetPosition(i, newPos);
                            break;
                        case Orientation.vertical: // Wobble a vertical line horizontally.
                            newPos = new Vector3(displacement, oldPos.y, oldPos.z);
                            lineRenderer.SetPosition(i, newPos);
                            break;
                    }
                }

                // Move the line.
                line.transform.position += new Vector3(lineHorizontalSpeed, lineVerticalSpeed, 0) * Time.deltaTime;

                // Check if the line is too far away from the parent object (the one this component is attached to). If so, move it back here.
                if (line.transform.localPosition.magnitude >= recallDistance)
                {
                    line.transform.localPosition = Vector3.zero;
                }
            }

            timer += Time.deltaTime;
        }
    }

    public void ToggleLines()
    {
        // Stop or start moving the lines, to avoid nausea.
        moveLines = !moveLines;
    }
}

