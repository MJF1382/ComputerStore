using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/store")]
    [ApiController]
    public class StoreController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;

        public StoreController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }
    }
}
