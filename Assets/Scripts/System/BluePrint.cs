using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BluePrint
{

    public string itemName;

    public string Req1;
    public string Req2;

    public int Req1amount;
    public int Req2amount;

    public int numOfRequirements;

    public  BluePrint (string name, int reqNum, string r1, int r1Num, string r2, int r2Num)
    {
        itemName = name;

        numOfRequirements = reqNum;

        Req1 = r1;
        Req2 = r2;

        Req1amount = r1Num;
        Req2amount = r2Num;

    }
}

