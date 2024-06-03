using System.Collections;
using System.Collections.Generic;

using UnityEngine;


public class OverworldData {

    public List<Character> characters;

    public List<Tile> tiles;

    public List<Line> lines;

}


public interface Serializable {

    public string loadFromJson();
    public void saveToJson();

}