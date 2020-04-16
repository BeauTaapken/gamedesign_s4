using System;
using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
using Random = UnityEngine.Random;

[CreateAssetMenu]

public class Spawner : ScriptableObject
{
    public LivingCounterUI LivingCounterUiMonster;
    public LivingCounterUI LivingCounterUiBoss;

    public int maxMonsters;
    
    private Transform plane;

    private TextMeshProUGUI tmpCountDown;

    private int MonsterAmount;
    private int BossAmount;

    private float _planeX;
    private float _planeZ;

    private bool coroutineRunning = false;

    public void Setup(Transform plane, int MonsterAmount, int BossAmount, float _planeX, float _planeZ, TextMeshProUGUI tmpMonstersSlain, TextMeshProUGUI tmpBossesSlain, TextMeshProUGUI tmpCountDown)
    {
        this.plane = plane;
        this._planeX = _planeX;
        this._planeZ = _planeZ;
        this.MonsterAmount = MonsterAmount;
        this.BossAmount = BossAmount;
        this.tmpCountDown = tmpCountDown;
        LivingCounterUiMonster.SetTextMeshPro(tmpMonstersSlain);
        LivingCounterUiBoss.SetTextMeshPro(tmpBossesSlain);
    }

    public int GetLivingMonsters()
    {
        return LivingCounterUiMonster.GetLivingMonsters();
    }

    public int GetLivingBosses()
    {
        return LivingCounterUiBoss.GetLivingMonsters();
    }

    public void UpMonsterAmount(int ExtraMonsters)
    {
        MonsterAmount = MonsterAmount * ExtraMonsters;
    }

    public void SetBossAmount(int BossAmount)
    {
        this.BossAmount = BossAmount;
    }
    
    public IEnumerator spawn(List<GameObject> MonsterList, List<GameObject> BossList)
    {
        coroutineRunning = true;
        for (int i = 3; i > 0; i--)
        {
            SetCountDown(i);
            yield return new WaitForSeconds(1);
        }
        tmpCountDown.text = String.Empty;

        SetCounters();

        for (int bossIndex = 0; bossIndex < BossAmount; bossIndex++)
        {
            spawnMonsters(BossList);
        }

        for (int monsterIndex = 0; monsterIndex < MonsterAmount; monsterIndex++)
        {
            while (GetAmountOfEnemiesInField() >= maxMonsters)
            {
                Debug.Log("waiting to spawn");
                yield return new WaitForSeconds(0.1f);
            }

            spawnMonsters(MonsterList);
        }

        yield return null;
        coroutineRunning = false;
    }

    private int GetAmountOfEnemiesInField()
    {
        return GameObject.FindGameObjectsWithTag("Monster").Length + GameObject.FindGameObjectsWithTag("Boss").Length;
    }

    private void SetCounters()
    {
        LivingCounterUiMonster.SetNumberOfMonsters(MonsterAmount);
        LivingCounterUiBoss.SetNumberOfMonsters(BossAmount);
    }

    private void SetCountDown(int second)
    {
        tmpCountDown.text = "Next wave in " + second;
    }

    public bool isCoroutineRunning()
    {
        return coroutineRunning;
    }

    private void spawnMonsters(List<GameObject> MonsterList)
    {
        int monsterNumber = Random.Range(0, MonsterList.Count);

        Vector3 spawnLocation = Vector3.zero;

        while (spawnLocation == Vector3.zero)
        {
            float _randomX = Random.Range(-_planeX / 3.0f, _planeX / 3.0f);
            float _randomZ = Random.Range(-_planeZ / 3.0f, _planeZ / 3.0f);

            Vector3 randomLocation = new Vector3(_randomX, 0.0f, _randomZ);
            spawnLocation = randomLocation;

            if (Physics.OverlapSphere(randomLocation, 2.0f).Length <= 1)
            {
                spawnLocation = randomLocation;
            }
        }

        GameObject obj = Instantiate(MonsterList[monsterNumber], spawnLocation, Quaternion.identity);

        obj.transform.parent = null;
    }
}
