using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameController : MonoBehaviour
{
    public GameObject objectPrefab;
    public Flock predatorFlock;
    private int objectCount = 0;
    private int key = 1;
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.Alpha1))
            key = 1;
        else if (Input.GetKeyDown(KeyCode.Alpha2))
            key = 2;

        if (Input.GetMouseButtonDown(0))
        {
            objectCount += 1;

            Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit2D hit = new RaycastHit2D();
            hit = Physics2D.Raycast(ray.origin, ray.direction);

            if (key == 1)    // Place an object for the boids to avoid when LMB is clicked
            {
                GameObject newObject = Instantiate(
                    objectPrefab,
                    new Vector3(hit.point.x, hit.point.y, 0),
                    Quaternion.Euler(0f, 0f, 0f),
                    transform
                );
                newObject.name = "Object " + objectCount;
            }
            else if (key == 2)    // Place a predator boid where the LMB is clicked
            {
                predatorFlock.AddAgent(hit.point);
            }
        }
    }
}
