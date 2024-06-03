using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : Character
{

    [SerializeField] private Overworld_Player owPlayerPrefab;
    [SerializeField] protected float experience;

    // Start is called before the first frame update
    

    public override void victory(Character opponent) {
        //gain experience based on opponents type
        experience += 2;
    }

    public override void defeat(Character opponent) {

    }

    public override Overworld_Character createOverworldAvatar()
    {
        var avatar = Instantiate(owPlayerPrefab);
        avatar.data = this;
        return avatar;
    }

}
