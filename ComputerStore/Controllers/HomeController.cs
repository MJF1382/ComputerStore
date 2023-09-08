using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;
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
        private readonly IRepositoryBase<Comment> _commentRepository;
        private readonly IRepositoryBase<Feedback> _feedbackRepository;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _satisfactionRepository = _unitOfWork.RepositoryBase<Satisfaction>();
            _articleRepository = _unitOfWork.RepositoryBase<Article>();
            _commentRepository = _unitOfWork.RepositoryBase<Comment>();
            _feedbackRepository = _unitOfWork.RepositoryBase<Feedback>();
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
                List<CommentModel>? comments = productModel.Comments;
                List<ProductModel> relatedPosts = (await _unitOfWork.ProductRepository.FindByConditionAsync(
                    p => p.CategoryId == productModel.CategoryId && p.Id != productModel.Id,
                    null,
                    null,
                    true,
                    0,
                    10))
                    .Select<Product, ProductModel>(product => product)
                    .ToList();

                productModel.Comments = null;

                return new ApiResult(Status.Ok, new
                {
                    Product = productModel,
                    Comments = comments,
                    RelatedPosts = relatedPosts
                });
            }

            return new ApiResult(Status.NotFound);
        }

        [HttpPost("product/{productId}/comments")]
        public async Task<ApiResult> SendCommentToProduct([FromRoute] Guid productId, [FromBody] CommentModel commentModel)
        {
            if (await _unitOfWork.ProductRepository.FindByIdAsync(productId) is Product)
            {
                if (commentModel.Type == "Product")
                {
                    commentModel.Id = Guid.NewGuid();
                    commentModel.ProductId = productId;
                    commentModel.PublishDateTime = DateTime.Now;
                    commentModel.Status = "در حال بررسی";

                    await _commentRepository.AddAsync(commentModel);
                    bool result = await _unitOfWork.Save();

                    if (result)
                    {
                        return new ApiResult(Status.Created, commentModel);
                    }

                    return new ApiResult(Status.InternalServerError);
                }

                return new ApiResult(Status.BadRequest, null, new List<string>()
                {
                    "نوع کامنت ارسال شده محصول نیست."
                });
            }

            return new ApiResult(Status.NotFound);
        }

        [HttpPost("contact-us")]
        public async Task<ApiResult> SendFeedback([FromBody] FeedbackModel feedbackModel)
        {
            feedbackModel.Id = Guid.NewGuid();

            await _feedbackRepository.AddAsync(feedbackModel);
            bool result = await _unitOfWork.Save();

            if (result)
            {
                return new ApiResult(Status.Created, feedbackModel);
            }

            return new ApiResult(Status.InternalServerError);
        }

        [HttpGet("blog")]
        public async Task<ApiResult> GetBlogData()
        {
            List<ArticleModel> articleModels = (await _articleRepository.FindByConditionAsync(
                null,
                new Expression<Func<Article, object>>[]
                {
                    p => p.Category,
                    p => p.User,
                    p => p.ArticleTags,
                    p => p.Comments
                }))
                .Select<Article, ArticleModel>(article => article)
                .ToList();

            return new ApiResult(Status.Ok, articleModels);
        }

        [HttpGet("blog/{id}")]
        public async Task<ApiResult> GetArticlesData([FromRoute] Guid id)
        {
            Article? article = (await _articleRepository.FindByConditionAsync(
                p => p.Id == id,
                new Expression<Func<Article, object>>[]
                {
                    p => p.Category,
                    p => p.User,
                    p => p.ArticleTags,
                    p => p.Comments
                }))
                .FirstOrDefault();

            if (article != null)
            {
                ArticleModel articleModel = article;
                List<CommentModel> comments = articleModel.Comments;
                List<ArticleModel>? relatedArticles = (await _articleRepository.FindByConditionAsync(
                    p => p.CategoryId == articleModel.CategoryId && p.Id != articleModel.Id))
                    .Select<Article, ArticleModel>(article => article)
                    .ToList();

                articleModel.Comments = null;

                return new ApiResult(Status.Ok, new
                {
                    Article = articleModel,
                    Comments = comments,
                    RelatedArticles = relatedArticles
                });
            }

            return new ApiResult(Status.NotFound);
        }
    }
}
