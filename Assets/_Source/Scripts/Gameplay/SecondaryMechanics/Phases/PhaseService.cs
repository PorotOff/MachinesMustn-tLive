    using System;
    using System.Collections.Generic;
    using UnityEngine;

    public class PhaseService
    {
        private CellField _cellField;
        private int _availableSteps;
        private PillarBar _pillarBar;
        private PillarSpawner _pillarSpawner;

        private List<CombatUnit> _warriors;
        private List<CombatUnit> _enemies;

        private IPhase _currentPhase;

        public event Action WarriorsDied;
        public event Action EnemiesDied;

        public PhaseService(CellField cellField, int availableSteps, PillarBar pillarBar, PillarSpawner pillarSpawner, List<CombatUnit> warriors, List<CombatUnit> enemies)
        {
            _cellField = cellField;
            _availableSteps = availableSteps;
            _pillarBar = pillarBar;
            _pillarSpawner = pillarSpawner;

            _warriors = warriors;
            _enemies = enemies;

            SetPhase(new PreparePhase(_cellField, _availableSteps, _pillarBar, _pillarSpawner));
        }

        public void Subscribe()
        {
            if (_currentPhase == null)
                return;

            _currentPhase.Over += OnPhaseOver;

            if (_currentPhase is BattlePhase battlePhase)
            {
                battlePhase.WarriorsDied += OnWarriorsDied;
                battlePhase.EnemiesDied += OnEnemiesDied;
            }
        }

        public void Unsubscribe()
        {
            if (_currentPhase == null)
                return;

            _currentPhase.Over -= OnPhaseOver;

            if (_currentPhase is BattlePhase battlePhase)
            {
                battlePhase.WarriorsDied -= OnWarriorsDied;
                battlePhase.EnemiesDied -= OnEnemiesDied;
            }
        }

        private void OnPhaseOver()
        {        
            if (_currentPhase is BattlePhase)
            {
                SetPhase(new PreparePhase(_cellField, _availableSteps, _pillarBar, _pillarSpawner));
            }
            else
            {
                SetPhase(new BattlePhase(_warriors, _enemies));
            }
        }

        private void SetPhase(IPhase phase)
        {
            Unsubscribe();
            
            if (_currentPhase != null)
            {
                _currentPhase.Exit();
            }

            _currentPhase = phase;
            Debug.Log($"Setted phase: {_currentPhase}");
            
            Subscribe();
            _currentPhase.Enter();
        }

        private void OnWarriorsDied()
        {
            WarriorsDied?.Invoke();
        }

        private void OnEnemiesDied()
        {
            EnemiesDied?.Invoke();
        }
    }