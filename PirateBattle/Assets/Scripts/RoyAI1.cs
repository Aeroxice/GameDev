using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyAI1 : BaseAI
{
    public override IEnumerator RunAI()
    {
        yield return BoatSpeed(200.0f);
        yield return Health(1000.0f);

        while (true)
        {

            yield return Ahead(1125);
            yield return TurnLeft(180);
            yield return Ahead(1125);
            yield return TurnRight(180);
            //yield return FireLeft(1);
            //yield return TurnLookoutRight(180);
            //yield return Back(200.0f);
            //yield return FireRight(1);
            //yield return TurnLookoutLeft(90);
            //yield return TurnRight(90);
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}

