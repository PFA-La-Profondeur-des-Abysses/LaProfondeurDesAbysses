﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Spawner : MonoBehaviour {

    public enum GizmoType { Never, SelectedOnly, Always }

    public Boid prefab;
    public float spawnRadius = 10;
    public int spawnCount = 10;
    public Color colour;
    public GizmoType showSpawnRegion;

    void Awake () {
        for (int i = 0; i < spawnCount; i++) {
            Vector3 pos = transform.position + new Vector3(Random.insideUnitCircle.x, Random.insideUnitCircle.y,0) * spawnRadius;

            Boid boid = Instantiate (prefab);
            boid.transform.position = pos;
            boid.transform.right = new Vector3(1,0,0);


        }
    }

    private void OnDrawGizmos () {
        if (showSpawnRegion == GizmoType.Always) {
            DrawGizmos ();
        }
    }

    void OnDrawGizmosSelected () {
        if (showSpawnRegion == GizmoType.SelectedOnly) {
            DrawGizmos ();
        }
    }

    void DrawGizmos () {

        Gizmos.color = new Color (colour.r, colour.g, colour.b, 0.3f);
        Gizmos.DrawSphere (transform.position, spawnRadius);
    }

}