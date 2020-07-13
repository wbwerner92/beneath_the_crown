using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CombatManager : MonoBehaviour
{
    public static CombatManager instance;

    public Party m_party1;
    public Party m_party2;

    // Start is called before the first frame update
    void Start()
    {
        instance = this;
    }

    public void PrintPreCombatAssessment()
    {
        if (m_party1 == null || m_party2 == null)
        {
            return;
        }

        string finalText = m_party1.partyName + ": " + m_party1.GetCombatants().Count + " members.\n" + m_party2.partyName + ": " + m_party2.GetCombatants().Count + " members.";
        MainTextManager.instance.AppendMainText(finalText);
    }
    public void PrintPostCombatAssessment(int p1Orig, int p2Orig)
    {
        if (m_party1 == null || m_party2 == null)
        {
            return;
        }

        int numP1 = m_party1.GetCombatants().Count;
        int numP2 = m_party2.GetCombatants().Count;

        string finalText = "";

        // Check 1: Party dead
        if (numP1 == 0)
        {
            finalText += "Your party has been defeated. All lie dead.";
        }
        else if (numP2 == 0)
        {
            finalText += "Your enemies lie dead at your feet. You are victorious.";
        }

        // Check 2: Toll on party
        if (numP1 > 0)
        {
            if (finalText != "")
            {
                finalText += "\n";
            }

            int numDead = p1Orig - numP1;
            float perDead = ((float)numDead/(float)p1Orig) * 100;

            // 0 Units lost.
            if (perDead == 0)
            {
                finalText += "All of your " + numP1 + " fighters remain alive.";
            }
            // Less than half dead
            else if (perDead < 50)
            {
                finalText += "You have sutained casualties. " + numDead + " of your units perished. " + numP1 + " remain able to fight.";
            }
            // More than half dead
            else
            {
                finalText += "You have taken heavy casualties. " + numDead + " lie dead. Only " + numP1 + " remain standing.";
            }

            finalText += "\n" + numP2 + " enemies remain.";
        }


        MainTextManager.instance.AppendMainText(finalText);
    }

    public void SetNewCombat(Party p1, Party p2)
    {
        MainTextManager.instance.AppendMainText("Battle! " + p1.partyName + " vs. " + p2.partyName);

        m_party1 = p1;
        m_party2 = p2;
        ResolveCombat();
    }

    void ResolveCombat()
    {
        while (m_party1.GetCombatants().Count > 0 && m_party2.GetCombatants().Count > 0)
        {
            ResolveCombatRound();
        }
    }

    void ResolveCombatRound()
    {
        MainTextManager.instance.AppendMainText("Combat ensues!");

        PrintPreCombatAssessment();

        List<Bod> p1Combatants = m_party1.GetCombatants();
        int numP1Orig = p1Combatants.Count;
        List<Bod> p2Combatants = m_party2.GetCombatants();
        int numP2Orig = p2Combatants.Count;

        int index1 = 0;
        int index2 = 0;

        // While there is another combatant on either team, continue the battle
        while (index1 < p1Combatants.Count || index2 < p2Combatants.Count)
        {
            Bod b1 = null;
            Bod b2 = null;

            if (index1 < p1Combatants.Count)
            {
                b1 = p1Combatants[index1];
            }
            if (index2 < p2Combatants.Count)
            {
                b2 = p2Combatants[index2];
            }

            if (b1 == null || b2 == null)
            {
                break;
            }
            else
            {
                BodCombat(b1, b2);
            }

            index1 ++;
            index2 ++;
        }

        PrintPostCombatAssessment(numP1Orig, numP2Orig);
    }

    void BodCombat(Bod b1, Bod b2)
    {
        string finalText = b1.fullName + " vs. " + b2.fullName;

        // Determine Advantage/Disadvantage
        int advVal = BodManager.instance.GetPersonalityTypeAdvantageValue(b1.personalityType, b2.personalityType);
        if (advVal != 0)
        {
            finalText += "\nAdvantage Value: " + advVal;
        }

        // Resolve turn order based on agility
        Bod first, second;
        finalText += "\n" + ResolveSpeedComparison(b1, b2, out first, out second);

        // Deal damage
        int atk1Bonus = 0;
        int atk2Bonus = 0;
        int atk1 = first.fighting + first.strength + atk1Bonus;
        int atk2 = second.fighting + second.strength + atk2Bonus;
        // Check for dead first
        if (!first.isDead && !second.isDead)
        {
            finalText += "\n" + ResolveAttack(first, second, atk1);
            if (second.isDead)
            {
                finalText += "\n" + second.name + " is killed.";
            }
        }
        // Check for dead secondary
        if (!first.isDead && !second.isDead)
        {
            finalText += "\n" + ResolveAttack(second, first, atk2);
            if (first.isDead)
            {
                finalText += "\n" + first.name + " is killed.";
            }
        }

        MainTextManager.instance.AppendMainText(finalText);
    }

    string ResolveSpeedComparison(Bod b1, Bod b2, out Bod first, out Bod second, int spBonus1 = 0, int spBonus2 = 0)
    {
        int sp1 = RVCalc.RollD20(b1.agility + b1.fighting + spBonus1);
        int sp2 = RVCalc.RollD20(b2.agility + b2.fighting + spBonus2);

        string finalText = b1.name + "'s speed: " + sp1 + " vs. " + b2.name + "'s speed: " + sp2;

        if (sp1 > sp2)
        {
            first = b1;
            second = b2;
        }
        else if (sp2 > sp1)
        {
            first = b2;
            second = b1;
        }
        else // sp1 == sp2
        {
            if (RVCalc.GetVal(1, 2) == 1)
            {
                finalText += "\nTie break: " + b1.name;
                first = b1;
                second = b2;
            }
            else
            {
                finalText += "\nTie break: " + b2.name;
                first = b2;
                second = b1;
            }
        }

        return finalText;
    }

    string ResolveAttack(Bod atk, Bod def, int atkPow)
    {
        string finalText = atk.name + " attacks " + def.name + " for " + atkPow + " dmg.";

        def.SetInjury(def.injury + atkPow);

        return finalText;
    }
}
