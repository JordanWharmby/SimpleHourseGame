using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour {
    //Singleton
    public static GameManager GM;
    //List of players
    public List<PlayerClass> Players;
    //States
    public enum GameState {
        MENUE,
        ADD_PlAYERS,
        GAME,
        RESET
    }
    public GameState CurrentState;
    private void Awake() {
        //Singleton pattern
        if (GM == null) GM = this;
        else if (GM != this) Destroy(gameObject);
    }
    private void Start() {
        CurrentState = GameState.MENUE;
    }
    private void Update() {
        
    }
    //Adds player to the game
    public void AddPlayer(string name) {
        if (Players == null) Players = new List<PlayerClass>();
        Players.Add(new PlayerClass(name)); 
    }
    public string GetAllPlayersNames() {
        string s = "Players: ";
        if (Players == null) return s;
        for (int i = 0; i < Players.Count; i++) {
            s += Players[i].PlayerName;
            if (i + 1 != Players.Count) s += ", ";
        }
        return s;
    }
}
