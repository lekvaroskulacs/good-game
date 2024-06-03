
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class ItemSprite : MonoBehaviour { 

    private Sprite sprite;

    private Item item;

    
    private SpriteRenderer spriteRenderer;

    [SerializeField] public TMP_Text cooldownIndicator;

    public void init(Sprite _sprite, Item _item) {
        this.sprite = _sprite;
        this.item = _item;

        spriteRenderer = GetComponent<SpriteRenderer>();
        spriteRenderer.sprite = sprite;

    }

    private void Update() {
        var cdString = item.getCurrentCooldown().ToString();
        if (cdString.Length >= 5)
            cdString = cdString.Remove(4);
        cooldownIndicator.text = cdString;
    }

}