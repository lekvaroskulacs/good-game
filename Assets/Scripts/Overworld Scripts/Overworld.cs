using System.Collections;
using System.Collections.Generic;
using System.IO;
using Unity.Mathematics;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Overworld : MonoBehaviour
{
    [SerializeField] private Line linePrefab;
    [SerializeField] private Tile tilePrefab;
    [SerializeField] private Enemy enemyPrefab;
    [SerializeField] private Overworld_Enemy owEnemyPrefab;
    

    [SerializeField] public List<Tile> tiles;
    [SerializeField] public List<Line> lines;
    
    [SerializeField] public List<Overworld_Character> characters;

    public delegate void enemyAddedDelegate(Overworld_Enemy overworld_Enemy, Enemy enemy);

    public event enemyAddedDelegate enemyAddedEvent;


    private void Awake() {
        loadMap();
        GameObject.FindGameObjectWithTag("TurnManager").GetComponent<TurnManager>().initCharacters(characters);
        enemyAddedEvent += enemyAdded;
    }

    private void enemyAdded(Overworld_Enemy overworld_Enemy, Enemy enemy) {
        characters.Add(overworld_Enemy);
    }

    public void loadMap() {

        if (MapState.instance.hasValidData) {
            wipeInitialState();
            loadFromState();
        }
        connectTilesWithLines();
        MapState.instance.clearState();
    }
    
    private void wipeInitialState() {
        foreach(var t in tiles) {
            Destroy(t.gameObject);
        }
        tiles.Clear();
        foreach(var l in lines) {
            Destroy(l.gameObject);
        }
        lines.Clear();
        foreach(var c in characters) {
            foreach(var item in c.getData().items)
                item.destroy();
            Destroy(c.gameObject);
        }
        characters.Clear();
    }

    private void loadFromState() {
        foreach(var pos in MapState.instance.tilePositions) {
            var tile = Instantiate(tilePrefab);
            tile.transform.position = pos;
            tiles.Add(tile);
        }
        foreach(var link in MapState.instance.neigboringTiles) {
            tiles[link[0]].addNeighbor(tiles[link[1]]);
        }
        foreach(var pair in MapState.instance.characterTilePairs) {
            if (pair.Key != null) {
                var avatar = pair.Key.createOverworldAvatar();
                avatar.currentTile = tiles[pair.Value];
                characters.Add(avatar);
            }
        }
    }

    private void connectTilesWithLines() {
        foreach(Tile tile in tiles) {

            var neighbors = tile.getNeighbors();

            foreach(Tile neighbor in neighbors) {

                if (!lineExistsWithNeighbors(tile, neighbor)) {
                    Line line = Instantiate(linePrefab);
                    line.First = tile;
                    line.Second = neighbor;
                    lines.Add(line);
                }

            }
        }
    }

    private bool lineExistsWithNeighbors(Tile first, Tile second) {
        foreach (Line l in lines) {

            if (l.First == first && l.Second == second) 
                return true;
            if (l.Second == first && l.First == second)
                return true;

        }

        return false;
    }


    /// <summary>
    /// ALWAYS CREATE A BATTLEFIELD IF TWO CHARACTERS ARE ON THE SAME TILE (no other conditions)
    /// might need to change algorithm according to game rules
    /// </summary>
    public void tryCreateBattlefield() {
        //Debug.Log("trycreate");
        foreach(var character1 in characters) {
            foreach(var character2 in characters) {
                
                if(character1.currentTile == character2.currentTile && character1 != character2) {
                    createBattlefield(character1.getData(), character2.getData());
                    return;
                }

            }
        }

    }

    private void createBattlefield(Character c1, Character c2) {
        
        MapState.instance.frontsideBattlingCharacter = c1;
        MapState.instance.backSideBattlingCharacter = c2;
        Debug.Log("1. " + c1.name);
        Debug.Log("2. " + c2.name);
        swapScene();
    }


    private void swapScene() {

        MapState.instance.populateData(tiles, characters);

        SceneManager.LoadScene(1);

    }


    public void spawnEnemy() {

        List<Tile> tilesWithoutCharacters = new List<Tile>();

        tilesWithoutCharacters.AddRange(tiles);

        foreach(var c in characters) {
            tilesWithoutCharacters.Remove(c.currentTile);
        }
        if (tilesWithoutCharacters.Count <= 0) return;

        int randVal = UnityEngine.Random.Range(0, tilesWithoutCharacters.Count);

        var enemyData = Instantiate(enemyPrefab, GameObject.FindWithTag("CharacterData").GetComponent<Transform>());
        var owEnemy = Instantiate(owEnemyPrefab);

        owEnemy.data = enemyData;
        owEnemy.currentTile = tilesWithoutCharacters[randVal];

        enemyAddedEvent.Invoke(owEnemy, enemyData);
        
    }

    public void saveMap() {
        StreamWriter sw = new StreamWriter("map.data");
        MapState.instance.populateData(tiles, characters);
        sw.Write(JsonUtility.ToJson(MapState.instance));
        sw.Flush();
        sw.Close();
    }

}
