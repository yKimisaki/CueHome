using CueHome.Data;

namespace CueHome.Models
{
    /// <summary>
    /// 声優を表します。
    /// </summary>
    public class Character
    {
        public string Name { get; }
        public int CoinAmount { get; private set; }
        public int PendingCoinAmount { get; private set; }
        public int LatestCommittedCoinAmount { get; private set; }

        public bool IsRetired { get; private set; } = false;
        public int RetiredYear;
        public int RetiredMonth;

        private Earning earning;

        public Character(string name)
        {
            Name = name;
            CoinAmount = 100;

            earning = Earning.ByName[Name];
        }

        /// <summary>
        /// 声優の稼ぎを取得します。
        /// </summary>
        /// <param name="year"></param>
        /// <param name="month"></param>
        /// <param name="week"></param>
        /// <returns></returns>
        public int GetEarnings(int year, int month, int week) => earning.Get(year, month, week);

        /// <summary>
        /// このターンに取得するコインを一時的にためます。
        /// </summary>
        /// <param name="coinAmount"></param>
        public void AddPendingCoinAmount(int coinAmount)
        {
            PendingCoinAmount += coinAmount;
        }

        /// <summary>
        /// 溜まっているコインを声優の財布に計上します。
        /// </summary>
        public void CommitPendingCoinAmount()
        {
            CoinAmount += PendingCoinAmount;
            LatestCommittedCoinAmount = PendingCoinAmount;
            PendingCoinAmount = 0;
        }

        /// <summary>
        /// 家賃を払います。
        /// </summary>
        /// <param name="model"></param>
        /// <param name="isForce"></param>
        /// <returns></returns>
        public bool TryPayCoinAmount(Main model, bool isForce = false)
        {
            if (IsRetired)
                return false;

            if (CoinAmount >= model.CurrentPaymentAmount)
            {
                CoinAmount -= model.CurrentPaymentAmount;
                return true;
            }
            else if (isForce)
            {
                Retire(model.Year, model.Month);
                return false;
            }

            return false;
        }

        private void Retire(int year, int month)
        {
            IsRetired = true;
            RetiredYear = year;
            RetiredMonth = month;
        }
    }
}
