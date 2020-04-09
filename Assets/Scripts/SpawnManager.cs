﻿using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;


public class SpawnManager : MonoBehaviour
{
    public Transform plane;

    public List<GameObject> SpawnableMonsters;
    public List<GameObject> SpawnableBosses;

    public int MonsterAmount = 3;
    public int BossAmount = 0;

    public TextMeshProUGUI tmpMonstersSlain;
    public TextMeshProUGUI tmpBossesSlain;
    public TextMeshProUGUI tmpCountDown;

    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        Collider planeMesh = plane.GetComponent<Collider>();
        if (planeMesh.bounds.extents == Vector3.zero)
        {
            Debug.Log("plane needs a collider");
        }
        else
        {
            spawner.Setup(plane, MonsterAmount, BossAmount, planeMesh.bounds.size.x, planeMesh.bounds.size.z, tmpMonstersSlain, tmpBossesSlain, tmpCountDown);

            StartCoroutine(spawner.spawn(SpawnableMonsters, SpawnableBosses));
        }
    }

    void Update()
    {
        if (spawner.GetLivingMonsters() == 0 && spawner.GetLivingBosses() == 0 && !spawner.isCoroutineRunning())
        {
            //spawner.UpMonsterAmount(3);
            StartCoroutine(spawner.spawn(SpawnableMonsters, SpawnableBosses));
        }
    }
}
