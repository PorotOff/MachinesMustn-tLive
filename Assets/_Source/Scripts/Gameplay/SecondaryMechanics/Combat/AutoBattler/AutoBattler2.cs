

using System.Collections.Generic;

public class AutoBattler2
{
    private Dictionary<CombatSquad, CombatSquad> _attackersOpponents = new Dictionary<CombatSquad, CombatSquad>();

    public void AddBattle(CombatSquad attackers, CombatSquad opponents)
    {
        _attackersOpponents.Add(attackers, opponents);
    }

    public void StartBattle()
    {
        foreach (var attackers in _attackersOpponents.Keys)
        {
            CombatSquad opponents = _attackersOpponents[attackers];

            // todo Я остановился на том, что не смог понять, как сталкивать юнитов: через атаку в скваде или путём вытягивания воинов
            // в списке во внешнем коде и там уже определять текущего атакующего и так далее.
        }
    }
}