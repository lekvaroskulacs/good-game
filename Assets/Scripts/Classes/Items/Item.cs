using System;
using System.Collections;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using TMPro;
using UnityEngine;

public abstract class Item : MonoBehaviour
{
    static int namectr = 0;
    [SerializeField] public Character owner;
    [SerializeField] protected float cooldown;
    [SerializeField] protected TextMeshPro cooldownIndicator;
    [SerializeField] public Battlefield currentBattle;
    [SerializeField] private Sprite sprite;
    [SerializeField] private ItemSprite itemSpritePrefab;
    
    
    [SerializeField] protected bool onCooldown = false;
    [SerializeField] protected float cdCounter;
    [SerializeField] protected bool inBattle = false;

    
    private void Awake() {
        gameObject.SetActive(true);
        name = namectr++.ToString();
    }
    public void init(Character _owner, float _cooldown) {
        owner = _owner;
        cooldown = _cooldown;
    }
    
    protected abstract void itemEffect();   

    public virtual void beginBattle() {
        inBattle = true;
    }

    public virtual void endBattle() {
        inBattle = false;
        onCooldown = false;
        cdCounter = 0;
        stopCooldown();
    }

    protected void startCooldown() {

        if (onCooldown) return;

        StartCoroutine(cooldownCoroutine());

    }

    protected void stopCooldown() {

        //StopCoroutine(cooldownCoroutine());
        StopAllCoroutines();
    }

    private IEnumerator cooldownCoroutine() {
        
        
        onCooldown = true;
        cdCounter = cooldown;

        while (cdCounter >= 0)
        {
            updateCooldownIndicator();

            cdCounter -= Time.deltaTime;
            yield return null;
            
        }

        cdCounter = 0;

        itemEffect();
        
        onCooldown = false;

    }

    private void updateCooldownIndicator()
    {
        var cdString = cdCounter.ToString();
        if (cdString.Length >= 5)
            cdString = cdString.Remove(4);
        cooldownIndicator.text = cdString;
    }

    public float getCurrentCooldown() {
        return cdCounter;
    }

    protected Character getBattleOpponent() {

        if (currentBattle.frontSide == owner)
            return currentBattle.backSide;
        else
            return currentBattle.frontSide;

    }

    public ItemSprite createSprite(bool backSide) {
        var itemSprite = Instantiate(itemSpritePrefab);
        itemSprite.init(sprite, this);
        if (backSide) {
            itemSprite.cooldownIndicator.transform.localPosition = new Vector3(0, -2, 0); 
        }
        return itemSprite;
    }

    public virtual void destroy() {
        Debug.Log(name + " should be destroyed");
        Destroy(gameObject);

    }

}
