using System;
using System.Linq;
using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ServicePlace.Logic;
using ServicePlace.Model;
using Microsoft.AspNetCore.Identity;
using ServicePlace.Website.Models.OrderViewModels;
using System.Threading.Tasks;
using System.Collections.Generic;
using System.Threading;

namespace ServicePlace.Website.Controllers
{
    public class OrderController : BaseController
    {
        private IOrderService orderService;
        private UserManager<User> userManager;
        private readonly IMapper _mapper;

        public OrderController(UserManager<User> userManager, IOrderService orderService, IMapper mapper)
        {
            this.userManager = userManager;
            this.orderService = orderService;
            _mapper = mapper;
        }

        [HttpGet]
        public IActionResult Index()
        {
            //Mapper.Initialize(cfg => cfg.CreateMap<IEnumerable<ServicePlace.Model.Order>, IEnumerable<PreviewViewModel>>());
            //var model = Mapper.Map<IEnumerable<ServicePlace.Model.Order>, IEnumerable<PreviewViewModel>>(orderService.Orders.Result);
            //Mapper.Reset();
            var model = _mapper.Map<IEnumerable<PreviewViewModel>>(orderService.Orders.Result);
            return View(model);
        }

        [HttpGet("{id:int}")]
        public IActionResult Get(int id)
        {
            var order = orderService.FindByIdAsync(id).Result;

            Mapper.Initialize(cfg => cfg.CreateMap<ServicePlace.Model.Order, ShowViewModel>()
                .ForMember("CreatedAt", opt => opt.MapFrom(src => src.CreatedAt.ToString())));
            var model = Mapper.Map<ServicePlace.Model.Order, ShowViewModel>(order);
            Mapper.Reset();

            if (order == null) return NotFound(new
            {
                Error = $"Order #{id} has not been found"
            });

            return View(model);
        }

        [HttpGet]
        [Route("Create")]
        public IActionResult Create()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Post(CreateViewModel model)
        {
            if (ModelState.IsValid)
            {
                //Mapper.Initialize(cfg => cfg.CreateMap<CreateViewModel, ServicePlace.Model.Order>()
                //    .ForMember("Creator", opt => opt.UseValue(userManager.GetUserAsync(User))));
                //var order = Mapper.Map<CreateViewModel, ServicePlace.Model.Order>(model);
                //Mapper.Reset();
                var order = new Order
                {
                    Title = model.Title,
                    Body = model.Body,
                    Creator = userManager.GetUserAsync(User).Result
                };
                orderService.CreateAsync(order, new CancellationToken());
                return RedirectToLocal($"Order/{orderService.Orders.Result.Last().Id}");
            }

            return View("Get", orderService.Orders.Result.Last());
        }
    }
}