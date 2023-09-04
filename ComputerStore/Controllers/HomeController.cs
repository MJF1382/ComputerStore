using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/home")]
    [ApiController]
    public class HomeController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Satisfaction> _satisfactionRepository;

        public HomeController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _satisfactionRepository = _unitOfWork.RepositoryBase<Satisfaction>();
        }

        [HttpGet("best-selling-products")]
        public async Task<ApiResult> GetBestSellingProducts()
        {
            return new ApiResult(Status.Ok, await _unitOfWork.ProductRepository.GetBestSellingProductsAsync(1));
        }

        [HttpGet("latest-customers-satisfactions")]
        public async Task<ApiResult> LatestCustomersSatisfactions()
        {
            return new ApiResult(Status.Ok, await _satisfactionRepository.FindByConditionAsync(null, null, p => p.PublishDate, false, 0, 10));
        }

        [HttpGet("website-statistics")]
        public async Task<ApiResult> GetWebSiteStatistics()
        {
            int satisfactedPeopleCount = (await _satisfactionRepository.GetAllAsync()).Count();
            int experienceYears = DateTime.Now.Year - 2020;
            int successPurchase = (await _unitOfWork.PurchaseRepository.GetAllAsync()).DistinctBy(p => p.UserId).Count();
            int usersCount = (await _unitOfWork.RepositoryBase<AppUser>().GetAllAsync()).Count();

            return new ApiResult(Status.Ok, new
            {
                SatisfactedPeopleCount = satisfactedPeopleCount,
                ExperienceYears = experienceYears,
                SuccessPurchase = successPurchase,
                UsersCount = usersCount
            });
        }
    }
}
