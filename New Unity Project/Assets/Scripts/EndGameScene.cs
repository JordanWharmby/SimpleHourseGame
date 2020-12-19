using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EndGameScene : MonoBehaviour {
    //Reset Button
    public void ResetGame() {
        RaceManager.RM.Reset();
        Destroy(gameObject);
    }
}