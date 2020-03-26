using System.Collections;
using System.Collections.Generic;
using System.Reflection.Emit;
using TMPro;
using UnityEngine;

public class SpawnManager : MonoBehaviour
{
    public Transform Plane;
    public List<GameObject> SpawnableMonsters;
    public List<GameObject> SpawnableBosses;
    public int MonsterAmount = 3;
    public int BossAmount = 0;
    public TextMeshProUGUI tmpMonstersSlain;
    public TextMeshProUGUI tmpBossesSlain;

    private float _planeX;
    private float _planeZ;

    // Start is called before the first frame update
    void Start()
    {
        Mesh planeMesh = Plane.GetComponent<MeshFilter>().mesh;
        _planeX = planeMesh.bounds.size.x;
        _planeZ = planeMesh.bounds.size.z;
        spawn(SpawnableMonsters);
        //spawn(SpawnableBosses);
        tmpMonstersSlain.text = MonsterAmount + "/" + MonsterAmount;
    }

    private void spawn(List<GameObject> list)
    {
        for (int monsterIndex = 0; monsterIndex < MonsterAmount; monsterIndex++)
        {
            int monsterNumber = Random.Range(0, list.Count);
            
            float _randomX = Random.Range(-_planeX / 2.0f, _planeX / 2.0f);
            float _randomZ = Random.Range(-_planeZ / 2.0f, _planeZ / 2.0f);

            Vector3 spawnLocation = new Vector3(_randomX, 0.0f, _randomZ);

            GameObject obj = Instantiate(list[monsterNumber], spawnLocation, Quaternion.identity, Plane);

            obj.transform.parent = null;
        }
    }
}
