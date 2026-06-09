using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;

public class BloodGroup : object
{
    private bool a, b, rh;

    private BloodGroup(bool a, bool b, bool rh)
    {
        this.a = a;
        this.b = b;
        this.rh = rh;
    }

    public static readonly BloodGroup O = new BloodGroup(false, false, false);
    public static readonly BloodGroup Op = new BloodGroup(false, false, true);
    public static readonly BloodGroup A = new BloodGroup(true, false, false);
    public static readonly BloodGroup Ap = new BloodGroup(true, false, true);
    public static readonly BloodGroup B = new BloodGroup(false, true, false);
    public static readonly BloodGroup Bp = new BloodGroup(false, true, true);
    public static readonly BloodGroup AB = new BloodGroup(true, true, false);
    public static readonly BloodGroup ABp = new BloodGroup(true, true, true);

    public bool CanGetDontationFrom(BloodGroup from) => (a || !from.a) && (b || !from.b) && (rh || !from.rh);
    public bool CanDonateTo(BloodGroup to) => to.CanGetDontationFrom(this);
    public static List<BloodGroup> Iter() => new List<BloodGroup> { O, Op, A, Ap, B, Bp, AB, ABp };

    public static BloodGroup GetRandom()
    {
        switch (Random.Range(0, 8))
        {
            case 0: return O;
            case 1: return Op;
            case 2: return A;
            case 3: return Ap;
            case 4: return B;
            case 5: return Bp;
            case 6: return AB;
            case 7: return ABp;
            default: throw new ArgumentOutOfRangeException();
        }
    }

    public override string ToString()
    {
        if (!a && !b && !rh) return "O-";
        else if (!a && !b && rh) return "O+";
        else if (a && !b && !rh) return "A-";
        else if (a && !b && rh) return "A+";
        else if (!a && b && !rh) return "B-";
        else if (!a && b && rh) return "B+";
        else if (a && b && !rh) return "AB-";
        else if (a && b && rh) return "AB+";
        else throw new ArgumentOutOfRangeException();
    }

    public static void Test()
    {
        // shitty blood group test
        Debug.Log("blood group test");
        BloodGroup o = BloodGroup.O;
        BloodGroup op = BloodGroup.Op;
        BloodGroup a = BloodGroup.A;
        BloodGroup ap = BloodGroup.Ap;
        BloodGroup b = BloodGroup.B;
        BloodGroup bp = BloodGroup.Bp;
        BloodGroup ab = BloodGroup.AB;
        BloodGroup abp = BloodGroup.ABp;

        Debug.Assert(o.CanDonateTo(ab));
        Debug.Assert(!ab.CanGetDontationFrom(op));
        Debug.Assert(abp.CanGetDontationFrom(op));
        Debug.Assert(!ab.CanGetDontationFrom(bp));
        Debug.Assert(!ab.CanGetDontationFrom(ap));
        Debug.Assert(ab.CanGetDontationFrom(b));
        Debug.Assert(ab.CanGetDontationFrom(a));
        Debug.Assert(abp.CanGetDontationFrom(a));

        foreach (BloodGroup gr in BloodGroup.Iter())
        {
            Debug.Assert(gr.CanDonateTo(BloodGroup.ABp));
            Debug.Assert(gr.CanDonateTo(gr));
        }

        foreach (BloodGroup left in BloodGroup.Iter())
            foreach (BloodGroup right in BloodGroup.Iter())
            {
                Debug.Assert(left.CanDonateTo(right) == right.CanGetDontationFrom(left));
            }

        Debug.Log("blood group test done");
    }
}