using System;
using Ambev.DeveloperEvaluation.Domain.Events;

namespace Ambev.DeveloperEvaluation.Domain.Services
{
  
    public class ConsoleEventPublisherService : IEventPublisherService
    {
        public void Publish(object evt)
        {
            switch (evt)
            {
                case SaleCreatedEvent e:
                    Console.WriteLine($"[EVENT] SaleCreated: SaleId={e.SaleId}, Date={e.CreatedAt:O}");
                    break;
                case SaleModifiedEvent e:
                    Console.WriteLine($"[EVENT] SaleModified: SaleId={e.SaleId}, Date={e.ModifiedAt:O}");
                    break;
                case SaleCancelledEvent e:
                    Console.WriteLine($"[EVENT] SaleCancelled: SaleId={e.SaleId}, Date={e.CancelledAt:O}");
                    break;
                case ItemCancelledEvent e:
                    Console.WriteLine($"[EVENT] ItemCancelled: SaleId={e.SaleId}, ItemId={e.ItemId}, Date={e.CancelledAt:O}");
                    break;
            }
        }
    }
}
