using ComputerStore.Classes;
using ComputerStore.Database;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq.Expressions;

namespace ComputerStore.Controllers
{
    [Route("api")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Satisfaction> _satisfactionRepository;
        private readonly IRepositoryBase<Article> _articleRepository;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _satisfactionRepository = _unitOfWork.RepositoryBase<Satisfaction>();
            _articleRepository = _unitOfWork.RepositoryBase<Article>();
        }

        [HttpGet("index")]
        public async Task<ApiResult> GetIndexData()
        {
            int satisfactedPeopleCount = (await _satisfactionRepository.GetAllAsync()).Count();
            int experienceYears = DateTime.Now.Year - 2020;
            int successPurchase = (await _unitOfWork.PurchaseRepository.GetAllAsync()).DistinctBy(p => p.UserId).Count();
            int usersCount = (await _unitOfWork.RepositoryBase<AppUser>().GetAllAsync()).Count();

            var bestSellingProducts = await _unitOfWork.ProductRepository.GetBestSellingProductsAsync(10);
            var latestCustomersSatisfactions = (await _satisfactionRepository.FindByConditionAsync(null, null, p => p.PublishDate, false, 0, 10))
                .Select<Satisfaction, SatisfactionModel>(satisfaction => satisfaction)
                .ToList();
            var webSiteStatistics = new
            {
                SatisfactedPeopleCount = satisfactedPeopleCount,
                ExperienceYears = experienceYears,
                SuccessPurchase = successPurchase,
                UsersCount = usersCount
            };
            var latestArticles = (await _articleRepository.FindByConditionAsync(
                null,
                new Expression<Func<Article, object>>[] { p => p.Category, p => p.User, p => p.Comments },
                p => p.PublishDateTime,
                false,
                0,
                3)).Select<Article, ArticleModel>(article => article).ToList();

            return new ApiResult(Status.Ok, new
            {
                BestSellingProducts = bestSellingProducts,
                LatestCustomersSatisfactions = latestCustomersSatisfactions,
                WebSiteStatistics = webSiteStatistics,
                LatestArticles = latestArticles
            });
        }

        [HttpGet("store")]
        public async Task<ApiResult> GetStoreData()
        {
            var mostPopularProducts = await _unitOfWork.ProductRepository.GetBestSellingProductsAsync();
            var mostExpensiveProducts = (await _unitOfWork.ProductRepository.FindByConditionAsync(null, null, p => p.Price, false))
                .Select<Product, ProductModel>(product => product)
                .ToList();
            var cheapestProducts = (await _unitOfWork.ProductRepository.FindByConditionAsync(null, null, p => p.Price))
                .Select<Product, ProductModel>(product => product)
                .ToList();
            var categories = (await _unitOfWork.RepositoryBase<Category>().FindByConditionAsync(null, null, p => p.Name))
                .Select<Category, CategoryModel>(category => category)
                .ToList();
            var brands = (await _unitOfWork.RepositoryBase<Brand>().FindByConditionAsync(null, null, p => p.Name))
                .Select<Brand, BrandModel>(brand => brand)
                .ToList();

            return new ApiResult(Status.Ok, new
            {
                MostPopularProducts = mostPopularProducts,
                MostExpensiveProducts = mostExpensiveProducts,
                CheapestProducts = cheapestProducts,
                Categories = categories,
                Brands = brands
            });
        }

        [HttpGet("product/{id}")]
        public async Task<ApiResult> GetProductData([FromRoute] Guid id)
        {
            Product? product = (await _unitOfWork.ProductRepository.FindByConditionAsync(
                p => p.Id == id,
                new Expression<Func<Product, object>>[] {
                    p => p.Brand,
                    p => p.Category,
                    p => p.Comments,
                    p => p.ExtraDetails
                }))
                .FirstOrDefault();

            if (product != null)
            {
                ProductModel productModel = product;

                return new ApiResult(Status.Ok, new
                {
                    Product = productModel
                });
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
