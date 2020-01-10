using System;
using Solution.Domain.Events.Warranty;

namespace Solution.Domain
{
   public class Warranty
   {
      public Guid Id { get; set; }
      public decimal Value { get; set; }
      public string Province { get; set; }

      public Warranty(WarrantyAddedEvent ev){
         Id = ev.WarrantyId;
         Value = ev.Value;
         Province = ev.Province;
      }
   }
}