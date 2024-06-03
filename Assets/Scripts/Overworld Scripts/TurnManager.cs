using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UIElements.Experimental;
using System.Linq;

/// <summary>
/// PROBLEMS:
/// TurnManager is not restrained to one instance, which it should be. This way,
/// there is a possibility of async r/w anomalies in the singleton class, if there is more
/// than one instance of TurnManager at a single time.
/// SOLUTION:
/// restrict the number of instances (gameObjects) to one.
/// </summary>

public class TurnManager : MonoBehaviour {

    [SerializeField] private List<Overworld_Character> characters = new List<Overworld_Character>();
    [SerializeField] private Overworld_Character currentChar;
    private Overworld owManager;

    

    private void Awake() {
        
        owManager.enemyAddedEvent += enemyAdded;

    }

    private void enemyAdded(Overworld_Enemy overworld_Enemy, Enemy enemyData) {

        characters.Add(overworld_Enemy);

    }

    public void initCharacters(List<Overworld_Character> chars) {
        characters.AddRange(chars);
        
        currentChar = characters[0];
        currentChar.isCurrentPlayer = true;

        owManager = GameObject.FindGameObjectWithTag("OverworldManager").GetComponent<Overworld>();
        //Debug.Log("Current char" + currentChar.name);
    }

    public void nextTurn() {   
        currentChar.isCurrentPlayer = false;
        if (characters.Count > characters.IndexOf(currentChar) + 1)
            currentChar = characters[characters.IndexOf(currentChar) + 1];
        else {
            currentChar = characters[0];
            roundOver();
        }
        turnOver();
        //currentChar.isCurrentPlayer = true;
        //Debug.Log("Current char" + currentChar.name);
    }

    private void LateUpdate() {
        currentChar.isCurrentPlayer = true;
    }

    private void turnOver() {

        owManager.tryCreateBattlefield();

        //Debug.Log("turn over");

    }

    private void roundOver() {
        //Debug.Log("round over");
    }

}
