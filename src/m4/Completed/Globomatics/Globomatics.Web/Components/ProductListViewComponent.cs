using Globomantics.Domain.Models;
using Globomatics.Infrastructure.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace Globomatics.Web.Components;

public class ProductListViewComponent : ViewComponent
{
    private readonly IRepository<Product> productRepository;
    private readonly ILogger<ProductListViewComponent> logger;

    public ProductListViewComponent(IRepository<Product> productRepository,
        ILogger<ProductListViewComponent> logger)
    {
        this.productRepository = productRepository;
        this.logger = logger;
    }

    public Task<IViewComponentResult> InvokeAsync()
    {
        var products = productRepository.All();

        logger.LogInformation($"Found a total of {products.Count()} products");

        return Task.FromResult<IViewComponentResult>(View(products));
    }
}
