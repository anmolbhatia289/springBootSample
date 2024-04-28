using Allen.Models;
using Allen.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Allen.Controllers
{
    public class DealController
    {
        public static DealController Instance { get; set; }

        private DealRepository DealRepository { get; set; }

        private DealController()
        {
            DealRepository = DealRepository.getInstance();
        }

        public static DealController getInstance()
        {
            if (Instance == null)
            {
                Instance = new DealController();
            }

            return Instance;
        }

        public void AddDeal(Product product, int minutesFromNow, int quantity, float discountPercentage)
        {
            int id = DealRepository.GetNextDealId();
            var Deal =
                new Deal(
                    id,
                    DateTime.Now.AddMinutes(minutesFromNow),
                    product,
                    quantity,
                    discountPercentage);
            DealRepository.AddDeal(Deal);
        }

        public List<Deal> GetDeals(string productName)
        {
            return DealRepository.GetDealsByProductName(productName);
        }

        public void ClaimDeal(Deal deal)
        {
            if (deal == null) return;
            DealRepository.ClaimDeal(deal);
        }

        public void EndDeal(Deal deal)
        {
            if (deal == null) return;
            DealRepository.EndDeal(deal);
        }

        public void UpdateDeal(Deal deal, DateTime endTime, int quantity)
        {
            if (deal == null) return;
            DealRepository.UpdateDeal(deal, endTime, quantity);
        }
    }
}
