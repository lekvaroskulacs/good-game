using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Battlefield : MonoBehaviour
{

    [SerializeField] private Battlefield battleFieldPrefab;

    [SerializeField] public Character frontSide;
    [SerializeField] public Character backSide;
    [SerializeField] private TextMeshPro frontHP;
    [SerializeField] private TextMeshPro backHP;
    [SerializeField] private ItemSort frontItemHolder;
    [SerializeField] private ItemSort backItemHolder;
    


    [SerializeField] private bool battleOngoing = false;

    public void beginBattle() {
        //Debug.Log("batlte begins!");
        battleOngoing = true;
        frontSide.beginBattle();
        backSide.beginBattle();
    }

    private void Awake() {
        
        frontSide = MapState.instance.frontsideBattlingCharacter;
        backSide = MapState.instance.backSideBattlingCharacter;

        frontSide.currentBattlefield = this;
        backSide.currentBattlefield = this;

        foreach(var item in frontSide.items) {
            item.currentBattle = this;
            item.gameObject.SetActive(true);
        }
        foreach(var item in backSide.items) {
            item.currentBattle = this;
            item.gameObject.SetActive(true);
        }

        List<Transform> frontItems = new List<Transform>();
        foreach(var item in frontSide.items) {
            frontItems.Add(item.createSprite(false).gameObject.GetComponent<Transform>());
        }

        List<Transform> backItems = new List<Transform>();
        foreach(var item in backSide.items) {
            backItems.Add(item.createSprite(true).gameObject.GetComponent<Transform>());
        }
        frontItemHolder.sortItems(frontItems);
        backItemHolder.sortItems(backItems);

        
    }

    private void Update() {
        if (battleOngoing) {
            frontHP.text = frontSide.health.ToString();
            backHP.text = backSide.health.ToString();
            //Debug.Log(backHP.text);
        }
    }

    public void endBattle() {
        //Debug.Log("battle end");

        frontHP.text = frontSide.health.ToString();
        backHP.text = backSide.health.ToString();
        
        battleOngoing = false;
        frontSide.endBattle();
        backSide.endBattle();

        if (backSide.health <= 0) {
            frontSide.victory(backSide);
            backSide.defeat(frontSide);
        }
        else if(frontSide.health <= 0) {
            backSide.victory(frontSide);
            frontSide.defeat(backSide);
        }

        

        swapScene();

    }

    private void swapScene() {

        SceneManager.LoadScene(0);

    }


}
