using System;

namespace ShootEmUp.Model
{
    [Serializable]
    public class SingelPlayerData : ISingelService
    {
        private readonly IWallet _wallet;

        public SingelPlayerData(IWallet wallet)
        {
            _wallet = wallet;
        }

        public int AccamulatedScore => _wallet.Score;
    }
}