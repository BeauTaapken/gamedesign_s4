using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu]

public class Spawner : ScriptableObject
{
    public LivingCounterUI LivingCounterUiMonster;
    public LivingCounterUI LivingCounterUiBoss;

    private Transform plane;

    private int MonsterAmount;
    private int BossAmount;

    private float _planeX;
    private float _planeZ;

    public void Setup(Transform plane, int MonsterAmount, int BossAmount, float _planeX, float _planeZ, TextMeshProUGUI tmpMonstersSlain, TextMeshProUGUI tmpBossesSlain)
    {
        this.plane = plane;
        this.MonsterAmount = MonsterAmount;
        this.BossAmount = BossAmount;
        this._planeX = _planeX;
        this._planeZ = _planeZ;
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
        MonsterAmount += ExtraMonsters;
    }

    public void UpBossAmount(int ExtraBosses)
    {
        BossAmount += ExtraBosses;
    }

    //TODO change to IEnumerator so it can be yielded to have a countdown timer for the player to know when new monsters spawn
    public void spawn(List<GameObject> MonsterList, List<GameObject> BossList)
    {
        SetCounters();

        for (int monsterIndex = 0; monsterIndex < MonsterAmount; monsterIndex++)
        {
            int monsterNumber = Random.Range(0, MonsterList.Count);

            float _randomX = Random.Range(-_planeX / 2.0f, _planeX / 2.0f);
            float _randomZ = Random.Range(-_planeZ / 2.0f, _planeZ / 2.0f);

            Vector3 spawnLocation = new Vector3(_randomX, 0.0f, _randomZ);

            GameObject obj = Instantiate(MonsterList[monsterNumber], spawnLocation, Quaternion.identity, plane);

            obj.transform.parent = null;
        }

        for (int bossIndex = 0; bossIndex < BossAmount; bossIndex++)
        {
            int bossNumber = Random.Range(0, BossList.Count);

            float _randomX = Random.Range(-_planeX / 2.0f, _planeX / 2.0f);
            float _randomZ = Random.Range(-_planeZ / 2.0f, _planeZ / 2.0f);

            Vector3 spawnLocation = new Vector3(_randomX, 0.0f, _randomZ);

            GameObject obj = Instantiate(BossList[bossNumber], spawnLocation, Quaternion.identity, plane);

            obj.transform.parent = null;
        }
    }

    private void SetCounters()
    {
        LivingCounterUiMonster.SetNumberOfMonsters(MonsterAmount);
        LivingCounterUiBoss.SetNumberOfMonsters(BossAmount);
    }
}
