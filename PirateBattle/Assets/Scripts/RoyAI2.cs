using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class RoyAI2 : BaseAI
{
    public override IEnumerator RunAI() {

        yield return BoatSpeed(125.0f);
        yield return Health(600.0f);

        while (true)
        {
            yield return Ahead(800);
            yield return TurnRight(90);
            /*yield return TurnLeft(90);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);
            yield return FireFront(1);

            yield return Back(200);
            yield return FireRight(1);
            yield return TurnLookoutLeft(90);
            yield return TurnRight(90);*/
        }
    }

    public override void OnScannedRobot(ScannedRobotEvent e)
    {
        //Debug.Log("Ship detected: " + e.Name + " at distance: " + e.Distance);
    }
}
