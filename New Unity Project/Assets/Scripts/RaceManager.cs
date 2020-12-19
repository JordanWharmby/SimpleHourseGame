using System.Collections;
using System.Collections.Generic;
using UnityEditor.VersionControl;
using UnityEditorInternal;
using UnityEngine;
using UnityEngine.XR.WSA.Input;

public class RaceManager : MonoBehaviour {
    //Singlton
    public static RaceManager RM;
    //Start position
    public Transform StartPos, EndPos;
    //The Amount of Horses
    public int AmountOfHorses = 7;
    //End game scene
    public GameObject EndGameScene;
    //State
    public enum RaceState {
        SETUP,
        BETTING,
        RACING,
        IDLE,
        FINISHED,
        RESET
    }
    public RaceState CurrentState;
    //Base horse
    public GameObject BaseHorse;
    //List of horses in the game
    public List<GameObject> RaceHorses = new List<GameObject>();
    //Position the next horse will be given
    private int nextFinishPosition = 1;
    private void Awake() {
        if (RM == null) RM = this;
        else if (RM != this) Destroy(gameObject);
    }
    // Start is called before the first frame update
    void Start() {
        //Sets the state
        if (RaceHorses.Count <= 0) CurrentState = RaceState.SETUP;
    }
    // Update is called once per frame
    void Update() {
        //Switch based on the state
        switch (CurrentState) {
            case RaceState.SETUP:
                //if there are horses in the game
                if (RaceHorses.Count > 0) {
                    //Sets the state of the horses
                    foreach (var horse in RaceHorses) horse.GetComponent<Hourse>().SetWatingState();
                    //Sets the racing state
                    CurrentState = RaceState.RACING;
                    //breaks
                    break;
                }
                //Gets the middle
                int midle = Mathf.CeilToInt(AmountOfHorses / 2);
                int posx = 1 * midle;
                for (int i = 0; i < AmountOfHorses; i++) {
                    //position of the horse
                    Vector2 pos = new Vector2(StartPos.position.x, posx);
                    posx -= 1;
                    //Sets up the horses
                    GameObject temp = Instantiate(BaseHorse, pos, Quaternion.identity);
                    //sets the stats
                    temp.GetComponent<Hourse>().SetUpVeriables();
                    //sets the state
                    temp.GetComponent<Hourse>().SetWatingState();
                    //Adds it to the list
                    RaceHorses.Add(temp);
                }
                //Sets to the idle state
                CurrentState = RaceState.RACING;
                break;
            case RaceState.BETTING:
                break;
            case RaceState.RACING:
                //Sets it back to idle
                bool flag = false;
                //checks that the horses are racing
                foreach (var item in RaceHorses) if (item.GetComponent<Hourse>().state != Hourse.HoursState.RACING) flag = true;
                //if the flag has not been flipped
                if (!flag) CurrentState = RaceState.IDLE;
                break;
            case RaceState.IDLE:
                break;
            case RaceState.FINISHED:
                //Creates the end game scene
                Instantiate(EndGameScene);
                //changes the state
                CurrentState = RaceState.IDLE;
                break;
            case RaceState.RESET:
                Reset();
                break;
        }
    }
    public int GetNextFinishPoition() {
        //If all horses have crosed the line
        if (nextFinishPosition == AmountOfHorses) CurrentState = RaceState.FINISHED;
        return nextFinishPosition++;
    }
    //Resets the race manager
    public void Reset() {
        //resets the horses
        foreach (var horse in RaceHorses) horse.GetComponent<Hourse>().Reset();
        //sets the next finish point
        nextFinishPosition = 1;
        //Sets the next state
        CurrentState = RaceState.SETUP;
    }
}
