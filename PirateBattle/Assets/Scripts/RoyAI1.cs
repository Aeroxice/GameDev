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
    public float TargetDistance = 1000;


    public override IEnumerator RunAI()
    {
        yield return BoatSpeed(120.0f);
        yield return Health(1000.0f);

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
                }
                else if (TargetDistance > 250 && Shooting)
                {
                    state = RoyAIState.Chase;
                }
                else
                {
                    yield return Move();
                }
            }
            else if (state == RoyAIState.Chase)
            {
                if (TargetDistance > 250 && Shooting)
                {
                    yield return Ahead(10f);
                }
                else if (Ship.Health <= 200 && Shooting)
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
                if (Ship.Health <= 200 && Shooting)
                {
                    yield return Move();
                    Debug.Log("Chuckle I'm in Danger");

                    if (TargetDistance <= 250 && Shooting)
                    {
                        yield return FireFront(1);
                        yield return new WaitForSeconds(0.5f);
                    }
                    else
                    {
                        state = RoyAIState.Roam;
                    }
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
        TargetDistance = e.Distance;
        Shooting = true;
    }
}
