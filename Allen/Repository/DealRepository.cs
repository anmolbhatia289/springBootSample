using Allen.Models;
using System.Data.SqlTypes;

namespace Allen.Repository
{
    internal class DealRepository
    {
        public static DealRepository Instance { get; set; }

        private Dictionary<int, Deal> DealDatabase;

        private DealRepository()
        {
            DealDatabase = new Dictionary<int, Deal>();
        }

        public static DealRepository getInstance()
        {
            if (Instance == null)
            {
                Instance = new DealRepository();
            }

            return Instance;
        }

        public void AddDeal(Deal Deal)
        {
            DealDatabase[Deal.Id] = Deal;
        }

        public List<Deal> GetDealsByProductName(string productName)
        {
            var deals = new List<Deal>();
            foreach (var Deal in DealDatabase.Values)
            {
                // ignore stale deals.
                if (Deal.endTime <= DateTime.Now) continue;

                if (string.Equals(Deal.Product.Name, productName))
                {
                   deals.Add(Deal);
                }
            }

            return deals;
        }

        public Deal GetDealById(int DealId)
        {
            if (DealDatabase.ContainsKey(DealId))
            {
                return DealDatabase[DealId];
            }
            else
            {
                return null;
            }
        }

        public void ClaimDeal(Deal deal)
        {
            if (DealDatabase.ContainsKey(deal.Id))
            {
                var storedDeal = DealDatabase[deal.Id];
                if (storedDeal.endTime < DateTime.Now) { throw new Exception("Deal is expired");  }
                if (storedDeal.UsedQuantity == storedDeal.Quantity)
                {
                    throw new Exception("Deal already used");
                }
                else
                {
                    storedDeal.UsedQuantity += 1;
                    DealDatabase[storedDeal.Id] = storedDeal;
                }
            }
        }

        public int GetNextDealId()
        {
            return this.DealDatabase.Count + 1;
        }

        public void EndDeal(Deal deal)
        {
            if (DealDatabase.ContainsKey(deal.Id))
            {
                var storedDeal = DealDatabase[deal.Id];
                if (storedDeal.endTime < DateTime.Now) { throw new Exception("Deal is already expired"); }
                storedDeal.endTime = DateTime.Now;
                DealDatabase[storedDeal.Id] = storedDeal;
            }
        }

        public void UpdateDeal(Deal deal, DateTime endTime, int quantity)
        {
            if (DealDatabase.ContainsKey(deal.Id))
            {
                var storedDeal = DealDatabase[deal.Id];
                storedDeal.endTime = endTime;
                storedDeal.Quantity = quantity;
                DealDatabase[storedDeal.Id] = storedDeal;
            }
        }
    }
}
