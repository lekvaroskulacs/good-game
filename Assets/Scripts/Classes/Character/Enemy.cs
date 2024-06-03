using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Enemy : Character
{
    
    [SerializeField] private Overworld_Enemy owEnemyPrefab;

    public override void victory(Character opponent) {

    }

    public override void defeat(Character opponent) {
        foreach (var i in items) {
            Debug.Log("Destroying: " + i.name);
            i.destroy();
        }
        Destroy(gameObject);
    }

    public override Overworld_Character createOverworldAvatar()
    {
        var avatar = Instantiate(owEnemyPrefab);
        avatar.data = this;
        return avatar;
    }

    public abstract void onSpawn();

}