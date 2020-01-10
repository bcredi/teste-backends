using Solution.Domain.Events.Proponent;
using System;

namespace Solution.Domain
{
    public class Proponent
    {
        public Guid Id { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsMain { get; set; }

        public Proponent(ProponentAddedEvent ev)
        {
            Id = ev.ProponentId;
            Name = ev.Name;
            Age = ev.Age;
            MonthlyIncome = ev.MonthlyIncome;
            IsMain = ev.IsMain;
        }
    }
}