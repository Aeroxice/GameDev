using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyAI1 : BaseAI
{
    enum RoyAIState
    {
        Roam,
        Chase,
        Retreat
    }

    private RoyAIState state;
    public bool Shooting = false;
    //public Vector3 Target;
    public float TargetDistance = 1000;
    //public Vector3 ShipLocation;


    public override IEnumerator RunAI()
    {
        yield return BoatSpeed(120.0f);
        yield return Health(1000.0f);
        yield return FireRate(1f);

        state = RoyAIState.Roam;

        while (true)
        {
            if (state == RoyAIState.Roam)
            {               
                if (TargetDistance <= 250 && Shooting)
                {
                    yield return Ahead(0);
                    yield return FireFront(1);
                    Shooting = false;
                    yield return new WaitForSeconds(0.5f);
                    Debug.Log("poop2");
                }
                else if (TargetDistance > 250 && Shooting)
                {
                    state = RoyAIState.Chase;
                    Debug.Log("poop");
                }
                else
                {
                    yield return Ahead(Random.Range(30.0f, 50.0f));
                    yield return TurnRight(Random.Range(30.0f, 50.0f));
                    yield return Ahead(Random.Range(10.0f, 50.0f));
                    yield return TurnLeft(Random.Range(30.0f, 50.0f));
                }
            }
            else if (state == RoyAIState.Chase)
            {
                if (TargetDistance > 250 && Shooting)
                {
                    yield return Ahead(10f);
                }
                else if (Ship.Health <= 400)
                {
                    state = RoyAIState.Retreat;
                }
                else if (TargetDistance <= 250 && Shooting)
                {
                    yield return Ahead(0);
                    yield return FireFront(1);
                    Shooting = false;
                    yield return new WaitForSeconds(0.5f);
                }                
                else
                {
                    state = RoyAIState.Roam;
                }
            }
            else if (state == RoyAIState.Retreat)
            {
                if (Ship.Health <= 400 && Shooting)
                {
                    yield return Back(10f);
                }
            }

            /*{
                state = RoyAIState.Chase;

                //yield return FireFront(1);
                //yield return TurnLookoutLeft(90);
                //yield return TurnLeft(360);
                //yield return FireLeft(1);
                //yield return TurnLookoutRight(180);
                //yield return Back(200.0f);
                //yield return FireRight(1);
                //yield return TurnLookoutLeft(90);
                //yield return TurnRight(90);
            }*/
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e, Vector3 temp)
    {
        Ship.transform.LookAt(temp);
        //Target = e.transform.position;
        TargetDistance = e.Distance;
        Shooting = true;
        Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}
