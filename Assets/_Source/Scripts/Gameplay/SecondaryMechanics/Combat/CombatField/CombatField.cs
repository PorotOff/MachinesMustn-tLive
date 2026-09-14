using System;
using System.Collections.Generic;
using UnityEngine;

public class CombatField : MonoBehaviour
{
    [SerializeField] private List<CombatUnitPlace> _warriorPlaces;
    [SerializeField] private List<CombatUnitPlace> _enemyPlaces;

    public void TakeCombatUnits(List<CombatUnit> units)
    {
        if (units == null)
            throw new ArgumentNullException(nameof(units));
        
        if (units.Count == 0)
            throw new InvalidOperationException($"{nameof(units)} count is {units.Count}");

        CombatUnit unit = units[0];

        if (unit is WarriorCombatUnit)
        {
            PlaceUnits(units, _warriorPlaces);
        }
        else
        {
            PlaceUnits(units, _enemyPlaces);
        }
    }

    private void PlaceUnits(List<CombatUnit> units, List<CombatUnitPlace> places)
    {
        if (units.Count > places.Count)
            throw new InvalidOperationException($"{nameof(units)} count: {units.Count} > {nameof(places)} count: {places.Count}");

        List<CombatUnitPlace> cachedPlaces = new List<CombatUnitPlace>(places);
        int unitsCount = units.Count;

        foreach (var unit in units)
        {
            CombatUnitPlace place = cachedPlaces.PopRandom();
            unit.StandAtPosition(place.transform.position);
        }
    }
}