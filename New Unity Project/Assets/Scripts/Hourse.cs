using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hourse : MonoBehaviour {
    //Name of the hours
    public string HoursName;
    //State
    public enum HoursState {
        WAITING,
        RACING,
        IDLE,
        FINISHED
    }
    public HoursState state;
    //Position the horse ends up in
    public int FinishPlace;
    //Position the horse needs to get to
    private Vector3 startPoint,  endPoint;
    //Speed of the horse;
    private float speed;
    //For if the speed needs to change
    private float baseDelta = 0.05f, curentdelt;
    private float maxSpeed = 1.75f, minSeed = 0.25f, veriant = 0.4f, baseSpeed = 1;
    //Sets up the variables
    public void SetUpVeriables() {
        //sets the base time
        curentdelt = baseDelta;
        HoursName = "" + Random.Range(1, 99);
        transform.name = HoursName;
        //Sets the start point
        startPoint = transform.position;
        //sets the end point
        float posY = transform.position.y;
        endPoint = new Vector3(RaceManager.RM.EndPos.position.x, posY, 0);
        //Sets the speed
        speed = baseSpeed;
    }
    //Sets the current state
    public void SetWatingState() { state = HoursState.WAITING; }

    void Update() {
        switch (state) {
            case HoursState.WAITING:
                //if the race has started
                if (RaceManager.RM.CurrentState == RaceManager.RaceState.RACING) state = HoursState.RACING;
                break;
            case HoursState.RACING:
                //if the horse is not at the end point
                if (GoToEndPoint()) {
                    //Sets the state to finished
                    state = HoursState.FINISHED;
                }
                //if it is still racing
                else {
                    //counts down
                    curentdelt -= Time.deltaTime;
                    //if it is at 0
                    if(curentdelt <= 0) {
                        //New speed
                        float min = speed - veriant;
                        if (min < minSeed) min = minSeed;
                        float max = speed + veriant;
                        if (max > minSeed) max = maxSpeed;
                        speed = Random.Range(min, max);
                        //Resets time
                        curentdelt = baseDelta;
                    }
                }
                break;
            case HoursState.IDLE:
                break;
            case HoursState.FINISHED:
                //Gets the finished position
                FinishPlace = RaceManager.RM.GetNextFinishPoition();
                //Sets the state
                state = HoursState.IDLE;
                break;
        }
    }
    //Moves the houser to the end point
    public bool GoToEndPoint() {
        //Sets the new position
        transform.position = Vector3.MoveTowards(transform.position, endPoint, speed * Time.deltaTime);
        //Test the new position
        if(Vector3.Distance(transform.position,endPoint) <= 0.01) {
            //sets it to the end point
            transform.position = endPoint;
            return true;
        }
        //if it is not there
        return false;
    }
    //Resets the horse
    public void Reset() {
        //Sets the position
        transform.position = startPoint;
        //sets the countdown
        curentdelt = baseDelta;
        //Sets the speed
        speed = baseSpeed;
    }
}
