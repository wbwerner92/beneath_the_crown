using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TestManager : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()
    {
        Debug.Log("Begin Testing");
        StartCoroutine(WaitToStartTest());
    }

    IEnumerator WaitToStartTest()
    {
        while (CombatManager.instance == null || BodManager.instance == null || MainTextManager.instance == null)
        {
            yield return null;
        }

        CombatTest1();
    }

    public Party GetTestPlayerParty1()
    {
        Debug.Log("GetTestPlayerParty1");
        
        // Leader
        Bod lord = BodManager.instance.CreateNewLordBod("Manfred", "Greenleaf", BodManager.instance.GetRandomPersonalityType(), true);

        // Player Party
        Party p = new Party(lord);

        // Body Guard Retainer
        Bod guard1 = BodManager.instance.CreateNewGuard("Derrik", "Alcroft");
        p.AddRetainer(guard1);

        // 2 Fighting units
        Bod fl1 = BodManager.instance.CreateNewFreelancer("Jonas");
        p.AddMember(fl1);
        Bod fl2 = BodManager.instance.CreateNewFreelancer("Cal");
        p.AddMember(fl2);

        // 1 Non combatant
        Bod nc = BodManager.instance.CreateNewBod("Wendell");
        p.AddMember(nc);

        return p;
    }

    public Party GetTestBanditParty1()
    {
        Debug.Log("GetTestBanditParty1");

        // Bandit Chief
        Bod banditChief = BodManager.instance.CreateNewFreelancer("Bandit Chief");
        banditChief.SetFightingSkill(2);

        Party p = new Party(banditChief, "Bandit Party");

        // 3 Bandits
        Bod b1 = BodManager.instance.CreateNewFreelancer("Bandit 1");
        p.AddMember(b1);
        Bod b2 = BodManager.instance.CreateNewFreelancer("Bandit 2");
        p.AddMember(b2);
        Bod b3 = BodManager.instance.CreateNewFreelancer("Bandit 3");
        p.AddMember(b3);

        return p;
    }

    public void CombatTest1()
    {
        Debug.Log("Combat Test 1");
        MainTextManager.instance.ClearMainText();

        Party p1 = GetTestPlayerParty1();
        Party p2 = GetTestBanditParty1();

        CombatManager.instance.SetNewCombat(p1, p2);
    }
}
