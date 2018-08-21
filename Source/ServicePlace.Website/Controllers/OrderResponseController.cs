using System.Linq;
using System.Web.Mvc;
using Microsoft.AspNet.Identity;
using Newtonsoft.Json;
using ServicePlace.Logic.Interfaces.Mappers;
using ServicePlace.Logic.Interfaces.Services;
using ServicePlace.Model.ViewModels.OrderResponseViewModels;

namespace ServicePlace.Website.Controllers
{
    public class OrderResponseController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IUserService _userService;
        private readonly IOrderResponseMapper _orderResponseMapper;

        public OrderResponseController(
            IUserService userService, 
            IOrderService orderService,
            IOrderResponseMapper orderResponseMapper)
        {
            _orderService = orderService;
            _userService = userService;
            _orderResponseMapper = orderResponseMapper;
        }

        public ActionResult Create(int orderId)
        {
            var viewModel = _orderResponseMapper.GetCreateOrderResponseViewModel(User.Identity.GetUserId(), orderId);
            return View("_OrderResponsePartial", viewModel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(CreateOrderResponseViewModel model)
        {
            var orderResponse = _orderResponseMapper.MapToOrderResponseModel(model, _userService.FindByUserName(User.Identity.GetUserName()));
            _orderService.CreateResponse(orderResponse);
            return PartialView("Partials/_OrderResponse",_orderResponseMapper.MapToOrderResponseViewModel(orderResponse));
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Delete(int id)
        {
            var responseToDelete = _orderService.GetAllOrderResponses().SingleOrDefault(x => x.Id == id);
            var orderId = responseToDelete?.Order.Id;
            _orderService.DeleteResponse(responseToDelete);
            return RedirectToAction("Show", "Order", new {id = orderId});
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Complete(int orderId, int orderResponseId)
        {
            _orderService.CompleteOrder(orderId, orderResponseId);
            return RedirectToAction("Show", "Order", new {id = orderId});
        }

        public ActionResult Index(int orderId)
        {
            var orderResponses = 
                _orderResponseMapper.MapToIndexOrderResponseViewModel(
                    _orderService.GetOrderResponses(orderId));

            return View("_OrderResponseIndexPartial", orderResponses);
        }
    }
}