using System.Collections;
using System.Collections.Generic;
using System.Linq.Expressions;
using Unity.VisualScripting;
using UnityEngine;

public abstract class Character : MonoBehaviour
{
    [SerializeField] public float health;
    [SerializeField] public Battlefield currentBattlefield;
    

    public List<Item> items = new List<Item>();

    public void takeDamage(float dmg) {

        health -= dmg;
        if (health <= 0)
            die();

    }

    public void die() {
        //Debug.Log("dead");
        currentBattlefield.endBattle();
    }

    public void addItem(Item item) {
        items.Add(item);
        item.owner = this;
        
    }

    public void beginBattle() {
        foreach(Item i in items) {
            i.beginBattle();
        }
    }

    public void endBattle() {
        currentBattlefield = null;
        foreach(Item i in items) {
            i.endBattle();
        }
    }

    public abstract void victory(Character opponent);

    public abstract void defeat(Character opponent);

    public abstract Overworld_Character createOverworldAvatar();
}
