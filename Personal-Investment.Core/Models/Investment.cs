using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Runtime.CompilerServices;
using System.Text;
using System.Threading.Tasks;

namespace Personal_Investment.Core.Models;
    [Table("Investments")]
    public class Investment
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int TypeId { get; set; }
        public string Name { get; set; }
        public decimal NumberOfShares { get; set; }
        public DateTime DateOfInvestment { get; set; }
        [Range(0.01, 100)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal ExpectedReturnPercent { get; set; }
        [Range(-100, -0.01)]
        [Column(TypeName = "decimal(5,2)")]
        public decimal StopLossPercent { get; set; }
        public decimal BuyPrice { get; set; }
        public string? Notes { get; set; }
        public bool IsSold { get; set; }

        [NotMapped] // EF Core nie będzie próbował zapisać tego w bazie
        private decimal _currentPrice;
        public decimal CurrentPrice
        {
            get => _currentPrice == 0 ? BuyPrice : _currentPrice;
            set => _currentPrice = value;
        }
    [NotMapped]
        public decimal ProfitLoss => (CurrentPrice - BuyPrice) * NumberOfShares;
        [NotMapped]
        public decimal ProfitLossPercentage => BuyPrice != 0 ? (CurrentPrice - BuyPrice) / BuyPrice * 100 : 0;

        public User User { get; set; }
        public InvestmentType Type { get; set; }
        public ICollection<UserInvestment> UserInvestments { get; set; }
        public ICollection<ReturnsHistory> ReturnsHistories { get; set; }
    }
