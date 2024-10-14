﻿using System;

namespace ShootEmUp.Model
{
    public interface IWallet
    {
        event Action onUpdateScore;

        int Score { get; } 

        void OnKill(Enemy enemy);
        void OnKill(Row row);
        void Reset();
    }
}