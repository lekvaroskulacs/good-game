using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Sword : Item
{


    protected override void itemEffect() {
        getBattleOpponent().takeDamage(1);
        
    }
    private void Update() {
        
        if (!inBattle) return;

        if (!onCooldown) 
            startCooldown();

    }

}
