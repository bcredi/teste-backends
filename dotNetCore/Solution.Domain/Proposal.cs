using System;
using System.Collections.Generic;

namespace Solution.Domain
{
   public class Proposal
   {
      public decimal LoanValue { get; set; }
      public int NumberOfInstallments { get; set; }
      public IEnumerable<Proponent> Proponents { get; set; }
      public IEnumerable<Warranty> Warranties { get; set; }
   }
}