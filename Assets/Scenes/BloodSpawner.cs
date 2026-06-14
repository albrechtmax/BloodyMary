using System;
using System.Collections.Generic;
using UnityEngine;
using Random = UnityEngine.Random;


public enum SpawnerPreset
{
    Level1, // only A+ and B+
    Level2, // O+, A+, B+, AB+;
    Level3, // all
}

public class BloodSpawner : MonoBehaviour
{
    public GameObject bloodDropScene;
    public SpawnerPreset preset = SpawnerPreset.Level1;
    private float lastUpdate;
    private float period = 1;

    private Transform markerStart, markerEnd;

    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        lastUpdate = Time.fixedTime;
        markerStart = transform.Find("SpawnMarkerStart");
        markerEnd = transform.Find("SpawnMarkerEnd");
    }

    // Update is called once per frame
    void Update()
    {
        if (Time.fixedTime - lastUpdate > period)
        {
            GameObject drop = Instantiate(bloodDropScene, GetRandomPosition(), transform.rotation);
            drop.GetComponent<BloodDrop>().bloodGroup = preset.GetRandom();
            drop.GetComponent<Rigidbody2D>().linearVelocity = Vector2.down * 1.0f;
            lastUpdate = Time.fixedTime;
        }

    }

    Vector2 GetRandomPosition()
    {
        float rand = Random.Range(0.0f, 1.0f);
        return markerStart.position * (1 - rand) + markerEnd.position * rand;
    }
}

static class SpawnerPresetMethods
{
    public static readonly List<BloodGroup> bloodGroupsLevel1 = new List<BloodGroup> { BloodGroup.Ap, BloodGroup.Bp };
    public static readonly List<BloodGroup> bloodGroupsLevel2 = new List<BloodGroup> { BloodGroup.Op, BloodGroup.Ap, BloodGroup.Bp, BloodGroup.ABp };
    public static readonly List<BloodGroup> bloodGroupsLevel3 = BloodGroup.Iter();

    public static List<BloodGroup> GetAvailable(this SpawnerPreset preset)
    {
        switch (preset)
        {
            case SpawnerPreset.Level1: return bloodGroupsLevel1;
            case SpawnerPreset.Level2: return bloodGroupsLevel2;
            case SpawnerPreset.Level3: return bloodGroupsLevel3;
            default: throw new ArgumentOutOfRangeException();
        }

    }

    public static BloodGroup GetRandom(this SpawnerPreset preset)
    {
        var available = preset.GetAvailable();
        return available[Random.Range(0, available.Count)];
    }
}