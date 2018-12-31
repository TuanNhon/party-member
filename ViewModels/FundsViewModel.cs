using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;
using WebApp1.Models;

namespace WebApp1.ViewModels
{
    public class FundsViewModel
    {
        public FundsViewModel()
        {
        }
        public FundsViewModel(Funds funds, TotalFunds totalFunds, string transactionUserName)
        {
            _funds = funds;
            _totalFunds = totalFunds;
            TransactionUserName = transactionUserName;
        }
        public Funds _funds { get; set; }
        public TotalFunds _totalFunds { get; set; }

        [Display(Name = "Người thực hiện")]
        public string TransactionUserName { get; set; }
    }
}
