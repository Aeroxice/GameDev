using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyAI1 : BaseAI
{
    enum RoyAIState //Define AI States
    {
        Roam,
        Chase,
        Retreat
    }

    private RoyAIState state;
    public bool Shooting = false;   //Set Shooting to false so it doesn't always shoot
    public float TargetDistance = 1000;     //Set TargetDistance to 1000 so it doesn't keep shooting


    public override IEnumerator RunAI()
    {
        yield return BoatSpeed(120.0f);     //Set BoatSpeed
        yield return Health(1000.0f);       //Set Health

        state = RoyAIState.Roam;     //Set AI State to Roam

        while (true)     //While AI script is active
        {
            if (state == RoyAIState.Roam)   //If Roaming
            {
                if (TargetDistance <= 250 && Shooting)      //Checks for distance and checks if shooting = true
                {
                    yield return Ahead(0);      //Stops moving
                    yield return FireFront(1);  //Shoots
                    Shooting = false;           //Sets shooting back to false
                    yield return new WaitForSeconds(0.5f);
                }
                else if (TargetDistance > 250 && Shooting)  //Checks for distance and checks if shooting = true
                {
                    state = RoyAIState.Chase;      //Sets state to the Chasing state
                }
                else
                {
                    yield return Move();     //Start moving to a random location
                }
            }
            else if (state == RoyAIState.Chase)     //If Chasing
            {
                if (TargetDistance > 250 && Shooting)   //Checks for distance and checks if shooting = true
                {
                    yield return Ahead(10f);
                }
                else if (Ship.Health <= 200 && Shooting)    //Checks for Health and checks if shooting = true
                {
                    state = RoyAIState.Retreat;     //Sets state to the Retreat state
                }
                else if (TargetDistance <= 250 && Shooting)     //Checks for distance and checks if shooting = true
                {
                    yield return Ahead(0);
                    yield return FireFront(1);
                    Shooting = false;       //Sets Shooting to false
                    yield return new WaitForSeconds(0.5f);
                }
                else
                {
                    state = RoyAIState.Roam;    //Set State back to Roaming
                }
            }
            else if (state == RoyAIState.Retreat)   //If Retreating
            {
                yield return Move();    //Start moving into a random direction
                state = RoyAIState.Roam;    //Set State back to Roaming
            }
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e, Vector3 temp)  //Added Vector3 temp to check for TargetDistance and LookAt
    {
        Ship.transform.LookAt(temp);    //Look at the location of the target
        TargetDistance = e.Distance;    //Checks distance to target
        Shooting = true;    //Sets Shooting to true when ScannedRobotEvent is called
    }
}
