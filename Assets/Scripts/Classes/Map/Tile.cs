using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class Tile : MonoBehaviour
{


    [SerializeField] private List<Tile> neighbors = new List<Tile>();
    private bool isClicked;
    public bool IsClicked {
        get;
    }
    public static bool moveable = true;

    // Update is called once per frame
    void Update()
    {

        if (!moveable) return;
        
        detectInteraction();
        dragTile();
    }

    private void detectInteraction() {
   
        if (getClickedTile() == this && Input.GetMouseButtonDown(0))
                isClicked = true;

        
        
        
        
        if (Input.GetMouseButtonUp(0)) {
            isClicked = false;
        }


    }

    private void dragTile() {

        if (!isClicked) return;

        Vector3 mouseWorldPoint = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        Vector3 mouseOnCameraPlane = new Vector3(mouseWorldPoint.x, mouseWorldPoint.y, 0);
        transform.position = mouseOnCameraPlane;
    }

    public static Tile getClickedTile() {
 
        if (!Input.GetMouseButtonDown(0) && !Input.GetMouseButtonDown(1))
            return null;

        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        RaycastHit2D hit = Physics2D.GetRayIntersection(ray);
        if (hit != null && hit.collider != null)
            return hit.collider.gameObject.GetComponent<Tile>();
        else
            return null;
            

    }


    public void addNeighbor(Tile neighbor) {
        neighbors.Add(neighbor);
    }

    public List<Tile> getNeighbors() {
        return neighbors;
    }

    public bool canMoveHere(Tile characterTile) {
        if (neighbors.Contains(characterTile)) {
            return true;
        }
        return false;
    }

}
