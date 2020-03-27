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
}
