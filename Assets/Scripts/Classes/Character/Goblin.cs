using UnityEngine;

public class Goblin : Enemy {

    [SerializeField] private Sword swordPrefab;

    private void Awake() {
        onSpawn();
    }

    public override void onSpawn()
    {
        var sword = Instantiate(swordPrefab, GameObject.FindWithTag("ItemData").GetComponent<Transform>());
        sword.init(this, 2.0f);

        addItem(sword);
    }

}