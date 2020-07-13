using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum PersonalityType
{
    NONE,
    RESOLUTE,
    RECKLESS,
    QUIRKY,
    OBSERVANT
}

public enum BodClass
{
    NONE,
    LORD,
    FREELANCER,
    GUARD,
    SCOUT,
    STEWARD
}

public class BodManager : MonoBehaviour
{
    public static BodManager instance;

    // Start is called before the first frame update
    void Start()
    {
       instance = this; 
    }

    public Bod CreateNewBod(string name, PersonalityType pt = PersonalityType.NONE, BodClass bc = BodClass.NONE)
    {
        if (pt == PersonalityType.NONE)
        {
            pt = GetRandomPersonalityType();
        }

        Bod bod = new Bod(name, "", pt, bc);
        bod.SetStrength(1);
        bod.SetAgility(1);
        bod.SetMind(1);
        return bod;
    }
    public Bod CreateNewBod(string name, string houseName, PersonalityType pt = PersonalityType.NONE, BodClass bc = BodClass.NONE)
    {
        if (pt == PersonalityType.NONE)
        {
            pt = GetRandomPersonalityType();
        }

        Bod bod = new Bod(name, houseName, pt, bc);
        bod.SetStrength(1);
        bod.SetAgility(1);
        bod.SetMind(1);

        return bod;
    }
    public Bod CreateNewLordBod(string name, string houseName, PersonalityType pt = PersonalityType.NONE, bool isCombatant = false)
    {
        Bod bod = CreateNewBod(name, houseName, pt, BodClass.LORD);
        bod.SetStrength(2);
        bod.SetAgility(2);
        bod.SetMind(2);
        if (isCombatant)
        {
            bod.SetFightingSkill(1);
        }
        return bod;
    }
    public Bod CreateNewGuard(string name, string houseName, PersonalityType pt = PersonalityType.NONE)
    {
        Bod bod = CreateNewBod(name, houseName, pt, BodClass.GUARD);
        bod.SetStrength(2);
        bod.SetFightingSkill(2);
        return bod;
    }
    public Bod CreateNewFreelancer(string name, PersonalityType pt = PersonalityType.NONE)
    {
        Bod bod = CreateNewBod(name, pt, BodClass.FREELANCER);
        bod.SetFightingSkill(1);
        return bod;
    }

    public PersonalityType GetRandomPersonalityType()
    {
        int val = RVCalc.GetVal(1, 4);
        switch (val)
        {
            case 1:
                return PersonalityType.RESOLUTE;
            case 2:
                return PersonalityType.RECKLESS;
            case 3:
                return PersonalityType.QUIRKY;
            case 4:
                return PersonalityType.OBSERVANT;
            default:
                return PersonalityType.NONE;
        }
    }

    /// <summary>
    /// Gets the Advantage/Disadvantage value for two personality types.
    /// Returns 1 if P1 is advantaged, -1 if P1 is disadvantaged, and 0 if P1 and P2 are equally advantaged
    /// </summary>
    /// <param name="pt1">Personality 1</param>
    /// <param name="pt2">Personality 2</param>
    /// <returns>1 if P1 is advantaged -1 if P1 is disadvantaged 0 if P1 and P2 are equally advantaged</returns>
    public int GetPersonalityTypeAdvantageValue(PersonalityType pt1, PersonalityType pt2)
    {
        // Advantaged
        if ((pt1 == PersonalityType.RESOLUTE && pt2 == PersonalityType.QUIRKY) ||
            (pt1 == PersonalityType.RECKLESS && pt2 == PersonalityType.RESOLUTE) ||
            (pt1 == PersonalityType.QUIRKY && pt2 == PersonalityType.OBSERVANT) ||
            (pt1 == PersonalityType.OBSERVANT && pt2 == PersonalityType.RECKLESS))
        {
            return 1;
        }
        // Disadvantaged
        else if ((pt2 == PersonalityType.RESOLUTE && pt1 == PersonalityType.QUIRKY) ||
            (pt2 == PersonalityType.RECKLESS && pt1 == PersonalityType.RESOLUTE) ||
            (pt2 == PersonalityType.QUIRKY && pt1 == PersonalityType.OBSERVANT) ||
            (pt2 == PersonalityType.OBSERVANT && pt1 == PersonalityType.RECKLESS))
        {
            return -1;
        }
        // Equal
        else
        {
            return 0;
        }
    }
    public bool IsPersonalityAdvantaged(Bod bod1, Bod bod2)
    {
        return GetPersonalityTypeAdvantageValue(bod1.personalityType, bod2.personalityType) == 1;
    }

    public bool IsCombatant(Bod bod)
    {
        int combat = bod.fighting;

        return combat > 0;
    }
}
