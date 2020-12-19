using TMPro;
using UnityEngine;

public class MainMenu : MonoBehaviour {
    //Pages of the menu
    public GameObject[] Pages;
    //List of players in the game
    public TextMeshProUGUI PlayersInTheGameText;
    public TextMeshProUGUI PlayerToAdd;
    //Name of the player to add
    private string newPlayerName;
    private TouchScreenKeyboard keyboard;
    // Start is called before the first frame update
    void Start() {
        //Sets the first page to be visible
        SetPageVisible(0);
        //Sets the text of all of the players in the game
        PlayersInTheGameText.text = GameManager.GM.GetAllPlayersNames();
    }
    private void Update() {
        if (keyboard != null) if (keyboard.active) {
                newPlayerName = keyboard.text;
                PlayerToAdd.text = "Name: " + newPlayerName;
            }
    }
    
    //Sets the page to be visible
    public void SetPageVisible(int page) { for (int i = 0; i < Pages.Length; i++) Pages[i].SetActive(i == page); }
    //Adds a player to the game
    public void AddPlayerToTheGame() {
        if (newPlayerName == "") return;
        GameManager.GM.AddPlayer(newPlayerName);
        PlayersInTheGameText.text = GameManager.GM.GetAllPlayersNames();
        newPlayerName = "Name:";
        PlayerToAdd.text = newPlayerName;
    }
    //Opens a keyboard
    public void SetPlayerName() {
        keyboard = TouchScreenKeyboard.Open("", TouchScreenKeyboardType.Default, false, false, true, true);
    }
}
