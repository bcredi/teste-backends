using System;
using System.Globalization;

namespace Solution.Domain.Events.Proposal
{
    public class ProposalUpdatedEvent : EventBase
    {
        public decimal LoanValue { get; set; }
        public int NumberOfInstallments { get; set; }
        public ProposalUpdatedEvent(IProposalRepository repo, string[] messageData) : base(repo, messageData)
        {
            CultureInfo culture = new CultureInfo("en-US");
            LoanValue = Convert.ToDecimal(messageData[5], culture);
            NumberOfInstallments = Int32.Parse(messageData[6]);
        }

        public override void Run()
        {
            if (!IsValid())
                return;

            var currentProposal = _repo.GetById(Id);
            currentProposal.LoanValue = LoanValue;
            currentProposal.NumberOfInstallments = NumberOfInstallments;

            _repo.Update(currentProposal);
        }
    }
}
