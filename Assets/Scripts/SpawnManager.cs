using System.Collections;
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

    public Spawner spawner;

    // Start is called before the first frame update
    void Start()
    {
        Collider planeMesh = plane.GetComponent<Collider>();

        spawner.Setup(plane, MonsterAmount, BossAmount, planeMesh.bounds.size.x, planeMesh.bounds.size.z, tmpMonstersSlain, tmpBossesSlain);

        spawner.spawn(SpawnableMonsters, SpawnableBosses);
    }

    void Update()
    {
        if (spawner.GetLivingMonsters() == 0 && spawner.GetLivingBosses() == 0)
        {
            spawner.UpMonsterAmount(3);
            spawner.spawn(SpawnableMonsters, SpawnableBosses);
        }
    }
}
