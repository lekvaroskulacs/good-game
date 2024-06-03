using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConnectTiles : MonoBehaviour
{

    [SerializeField] private Line linePrefab; 
    
    private bool waitingForFirstTile = false;
    private bool waitingForSecondTile = false;
    private bool processOngoing = false;
    private Tile first = null;
    private Tile second = null;


    public void startConnectSequence() {

        if (processOngoing)
            return;

        Tile.moveable = false;

        waitingForFirstTile = true;
        waitingForSecondTile = false;
        Debug.Log("Please select a tile");

    }

    private void startWaitingForSecondTile() {
        waitingForFirstTile = false;
        waitingForSecondTile = true;
        
        Debug.Log("Please select another tile");
    }

    private void endConnectSequence() {
        waitingForFirstTile = false;
        waitingForSecondTile = false;

        first.addNeighbor(second);
        second.addNeighbor(first);

        createLine();
        
        Debug.Log($"{first.name} and {second.name} are connected");
        first = null;
        second = null;

        Tile.moveable = true;
    }

    private void Update() {
        
        if (waitingForFirstTile) {

            first = Tile.getClickedTile();
            if (first != null) {
                startWaitingForSecondTile();
                return;
            }

        }

        if (waitingForSecondTile) {
            second = Tile.getClickedTile();
            if (second != null) {
                endConnectSequence();
                return;
            }
        }

    }

    
    public void createLine() {

        if (first == null || second == null) return;    
        
        Line line = Instantiate(linePrefab, Vector3.zero, Quaternion.identity);
        line.First = first;
        line.Second = second;

    } 

    


}
