using System;
using System.Globalization;

namespace Solution.Domain.Events.Proposal
{
    public class ProposalCreatedEvent : EventBase
    {
        public decimal LoanValue { get; set; }
        public int NumberOfInstallments { get; set; }
        public ProposalCreatedEvent(IProposalRepository repo, string[] messageData) : base(repo, messageData)
        {
            CultureInfo culture = new CultureInfo("en-US");
            LoanValue = Convert.ToDecimal(messageData[5], culture);
            NumberOfInstallments = Int32.Parse(messageData[6]);
        }

        public override void Run()
        {
            if (!IsValid())
                return;

            _repo.Save(new Solution.Domain.Proposal(this));
        }
        public override bool IsValid()
        {
            var currentProposal = _repo.GetById(Id);

            if (currentProposal == null)
                return true;
            return false;
        }
    }
}
