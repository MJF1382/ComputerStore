﻿using ComputerStore.Classes;
using ComputerStore.Database.Entities;
using ComputerStore.Database.Repositories;
using ComputerStore.Database.UnitOfWork;
using ComputerStore.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace ComputerStore.Controllers
{
    [Route("api/brands")]
    [ApiController]
    public class BrandController : ControllerBase
    {
        private readonly IUnitOfWork _unitOfWork;
        private readonly IRepositoryBase<Brand> _brandRepository;

        public BrandController(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _brandRepository = _unitOfWork.RepositoryBase<Brand>();
        }
    }
}
