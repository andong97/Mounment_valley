using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class playCtrl : MonoBehaviour
{
    public Transform currentCube;
    public Transform clickCube;

    public List<Transform> finalPath;
    void Start()
    {
       
    }

    public void Update() {
        RayCastDown();

        if (Input.GetMouseButtonDown(0)) {
            Ray mouseRay = Camera.main.ScreenPointToRay(Input.mousePosition);
            RaycastHit mouseHit;
            if (Physics.Raycast(mouseRay, out mouseHit))
            {
                if (mouseHit.transform.GetComponent<Walkeable>() != null)
                { 
                    clickCube = mouseHit.transform;
                }
            }
        }
    }



    public void RayCastDown() {
        Ray playerRay = new Ray(transform.GetChild(0).position, -transform.up);
        RaycastHit playerHit;
        if (Physics.Raycast(playerRay, out playerHit)) {
            if (playerHit.transform.GetComponent<Walkeable>() != null)
            {
                currentCube = playerHit.transform;
            }
        }
    }



    void FindWay() {
        List<Transform> nextCube = new List<Transform>();
        List<Transform> pastCube = new List<Transform>();

        foreach (WalkPath path in currentCube.GetComponent<Walkeable>().possiblePaths) {
            if (path.active) {
                nextCube.Add(path.target);
                path.target.GetComponent<Walkeable>().previousCube = currentCube;
            }
        }

        pastCube.Add(currentCube);
    }

    void BuildPath() {
        Transform cube = clickCube;
        while (cube != currentCube) {
            finalPath.Add(cube);
            if (cube.GetComponent<Walkeable>().previousCube != null)
            {
                cube = cube.GetComponent<Walkeable>().previousCube;
            }
            else
                return;
        }
    }

    void FollowPath() {

    }

    void ExplorCube(List<Transform> nextCubes,List<Transform> visiteCubes) {
        Transform current = nextCubes[0];
        nextCubes.Remove(currentCube);
        if (current == clickCube) { return; }
        foreach (WalkPath path in current.GetComponent<Walkeable>().possiblePaths) {
            if (!visiteCubes.Contains(path.target) && path.active) {
                nextCubes.Add(path.target);
                path.target.GetComponent<Walkeable>().previousCube = current;
            }
        }
        visiteCubes.Add(current);
        if (nextCubes.Count > 0) {
            ExplorCube(nextCubes, visiteCubes);
        }
    }
 
}
