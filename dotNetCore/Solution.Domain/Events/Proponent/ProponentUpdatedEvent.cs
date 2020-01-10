using System;
using System.Globalization;
using System.Linq;

namespace Solution.Domain.Events.Proponent
{
    public class ProponentUpdatedEvent : EventBase
    {
        public Guid ProponentId { get; set; }
        public string Name { get; set; }
        public int Age { get; set; }
        public decimal MonthlyIncome { get; set; }
        public bool IsMain { get; set; }
        public ProponentUpdatedEvent(IProposalRepository repo, string[] messageData) : base(repo, messageData)
        {
            ProponentId = Guid.Parse(messageData[5]);
            Name = messageData[6].Trim();
            Age = Int32.Parse(messageData[7]);
            CultureInfo culture = new CultureInfo("en-US");
            MonthlyIncome = Convert.ToDecimal(messageData[8], culture);

            IsMain = bool.Parse(messageData[9]);
        }

        public override void Run()
        {
            var currentProposal = _repo.GetById(this.ProposalId);
            var proponent = currentProposal.Proponents.Single(w => w.Id == this.ProponentId);
            proponent.Name = this.Name;
            proponent.Age = this.Age;
            proponent.MonthlyIncome = this.MonthlyIncome;
            proponent.IsMain = this.IsMain;
            _repo.Update(currentProposal);
        }
    }
}
