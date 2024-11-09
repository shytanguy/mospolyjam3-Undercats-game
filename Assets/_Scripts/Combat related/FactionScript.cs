using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FactionScript : MonoBehaviour
{
    
    public enum Faction
    {
        player,
        enemy
    }
   [field:SerializeField] public Faction userFaction { get; private set; }

    public void ChangeFaction(Faction faction)
    {
        userFaction = faction;
    }
}
