using System;
using System.Collections;
using System.Collections.Generic;
using Unity.Mathematics;
using UnityEditorInternal;
using UnityEngine;

public class MapState 
{

    private MapState() {}

    static private MapState _instance;

    public static MapState instance {

        get {
            if (_instance == null)
                _instance = new MapState();
            
            return _instance;
        }

        private set {}

    }
    
    public List<Vector3> tilePositions = new List<Vector3>();

    public List<int2> neigboringTiles = new List<int2>();

    public List<KeyValuePair<Character, int>> characterTilePairs = new List<KeyValuePair<Character, int>>();


    private Character _frontsideBattlingCharacter;
    public Character frontsideBattlingCharacter {
        get {return _frontsideBattlingCharacter;}
        set {_frontsideBattlingCharacter = value;}
    }


    private Character _backSideBattlingCharacter;
    public Character backSideBattlingCharacter {
        get {return _backSideBattlingCharacter;}
        set {_backSideBattlingCharacter = value;}
    }

    private bool _hasValidData = false;
    public bool hasValidData {
        get {return _hasValidData;}
        set {_hasValidData = value;}
    }



    public void populateData(List<Tile> tiles, List<Overworld_Character> characters) {

        populateTilePositions(tiles);
        populateNeighboringTiles(tiles);
        populateCharacterTilePairs(characters);

        
        

        hasValidData = true;

    }


    public void populateTilePositions(List<Tile> tiles) {

        foreach(var t in tiles) {

            tilePositions.Add(t.transform.position);

        }
        
    }

    /// <summary>
    /// relies on tilePosition being in same order as tiles in overworld 
    /// (which populateTilePositions should fill out in the correct order)
    /// </summary>
    /// <param name="tiles"></param>
    public void populateNeighboringTiles(List<Tile> tiles) {

        for (int i = 0; i < tiles.Count; i++) {

            var neighbors = tiles[i].getNeighbors();
            foreach(var n in neighbors) {

                int nIdx = tiles.IndexOf(n);
                //hopefully contains works
                if (!neigboringTiles.Contains(new int2(i, nIdx))) 
                    neigboringTiles.Add(new int2(i, nIdx));
            }

        }
       

    }

    public void populateCharacterTilePairs(List<Overworld_Character> overworld_Characters) {

        foreach(var c in overworld_Characters) {
            int idx = tilePositions.IndexOf(c.currentTile.transform.position);
            characterTilePairs.Add(new KeyValuePair<Character, int>(c.getData(), idx));
        }


    }

    public void clearState() {

        tilePositions.Clear();
        neigboringTiles.Clear();
        characterTilePairs.Clear();
        frontsideBattlingCharacter = null;
        backSideBattlingCharacter = null;

    }

}
