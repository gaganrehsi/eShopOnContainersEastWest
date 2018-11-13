namespace Microsoft.eShopOnContainers.Services.Catalog.API.IntegrationEvents.EventHandling
{
    using BuildingBlocks.EventBus.Abstractions;
    using System.Threading.Tasks;
    using BuildingBlocks.EventBus.Events;
    using Infrastructure;
    using System.Collections.Generic;
    using System.Linq;
    using global::Catalog.API.IntegrationEvents;
    using IntegrationEvents.Events;

    public class ProductPriceChangedIntegrationEventHandler :
        IIntegrationEventHandler<ProductPriceChangedIntegrationEvent>
    {
        private readonly CatalogContext _catalogContext;
        private readonly ICatalogIntegrationEventService _catalogIntegrationEventService;

        public ProductPriceChangedIntegrationEventHandler(CatalogContext catalogContext,
            ICatalogIntegrationEventService catalogIntegrationEventService)
        {
            _catalogContext = catalogContext;
            _catalogIntegrationEventService = catalogIntegrationEventService;
        }


        public async Task Handle(ProductPriceChangedIntegrationEvent command)
        {
            var catalogItem = _catalogContext.CatalogItems.Find(command.ProductId);
            //Only save change and throw new event if we dont have this price already
            if (catalogItem.Price != command.NewPrice)
            {
                // Update current product
                var oldPrice = catalogItem.Price;

                catalogItem.Price = command.NewPrice;

                _catalogContext.CatalogItems.Update(catalogItem);

                // Save product's data and publish integration event through the Event Bus if price has changed

                //Create Integration Event to be published through the Event Bus
                var priceChangedEvent = new ProductPriceChangedIntegrationEvent(catalogItem.Id, command.NewPrice, oldPrice);

                // Achieving atomicity between original Catalog database operation and the IntegrationEventLog thanks to a local transaction
                await _catalogIntegrationEventService.SaveEventAndCatalogContextChangesAsync(priceChangedEvent);

                // Publish through the Event Bus and mark the saved event as published
                await _catalogIntegrationEventService.PublishThroughEventBusAsync(priceChangedEvent);
            }
        }
    }
}
