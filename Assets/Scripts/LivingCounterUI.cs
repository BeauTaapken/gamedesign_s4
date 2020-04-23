using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;
[CreateAssetMenu]

public class LivingCounterUI : ScriptableObject
{
    private TextMeshProUGUI tmpMonsterText;
    private int numberOfRoundMonsters;
    private int numberOfLivingMonsters;
    private int monstersInField;

    public void SetTextMeshPro(TextMeshProUGUI tmpMonsterText)
    {
        this.tmpMonsterText = tmpMonsterText;
    }

    public void SetNumberOfMonsters(int monsterAmount)
    {
        numberOfRoundMonsters = monsterAmount;
        numberOfLivingMonsters = monsterAmount;
        setText();
    }

    public void RemoveMonster()
    {
        monstersInField--;
        numberOfLivingMonsters--;
        setText();
    }

    private void setText()
    {
        tmpMonsterText.text = numberOfLivingMonsters + "/" + numberOfRoundMonsters;
    }

    public int GetLivingMonsters()
    {
        return numberOfLivingMonsters;
    }

    public void UpMonstersInField()
    {
        monstersInField++;
    }

    public int GetMonstersInField()
    {
        return monstersInField;
    }

    public void SetMonstersInField(int newMonsterAmount)
    {
        monstersInField = newMonsterAmount;
    }
}
