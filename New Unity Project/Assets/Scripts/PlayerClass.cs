public class PlayerClass {
    //Player Name
    private string playerName;
    //Player Money
    private int playerMoney;
    //If they are still in
    private bool stillIn;
    //constructor
    public PlayerClass() {
        playerName = "Default";
        playerMoney = 10;
        stillIn = true;
    }
    public PlayerClass(string Name) {
        playerName = Name;
        playerMoney = 10;
        stillIn = true;
    }
    //Gets the variables
    public string PlayerName { get { return playerName; } }
    public float PlayerMoney { get { return playerMoney; } }
    public bool PlayerStillIn { get { return stillIn; } }
    //Sets the amount of money the player has
    public void SetPlayerMoney(int i) {
        playerMoney += i;
        if (playerMoney <= 0) playerMoney = 0;
    }
}
