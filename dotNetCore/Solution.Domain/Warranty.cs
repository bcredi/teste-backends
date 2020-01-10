using System;

namespace Solution.Domain
{
   public class Warranty
   {
      public Guid Id { get; set; }
      public decimal Value { get; set; }
      public string Province { get; set; }
   }
}