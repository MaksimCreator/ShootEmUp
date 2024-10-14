using Zenject;

namespace ShootEmUp.Model
{
    public class SingelPlayerData
    {
        private readonly IWallet _wallet;

        [Inject]
        public SingelPlayerData(IWallet wallet)
        {
            _wallet = wallet;
        }

        public int AccamulatedScore => _wallet.Score;
    }
}