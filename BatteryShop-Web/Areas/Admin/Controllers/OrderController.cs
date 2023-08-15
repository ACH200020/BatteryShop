using BatteryShop_Web.Areas.Admin.Models.Order;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.User;
using CoreLayer.Services.OrderDetails;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.Admin.Controllers;

public class OrderController : AdminControllerBase
{

    #region Services

    private readonly IOrderDetailService _orderService;
    private readonly IOrderService _order;
    public OrderController(IOrderDetailService orderService, IOrderService order)
    {
        _orderService = orderService;
        _order = order;
    }

    #endregion


    
    public IActionResult Index()
    {
        ListOrderDetailViewModel listOrderViewModel = new ListOrderDetailViewModel();
        var order = _order.OrdersThatPaymentFinaled();
        listOrderViewModel.OrderDtos = _orderService.GetOrderDetailByOrderId(order.Id);

        return View(listOrderViewModel);
    }

    [HttpPost]
    public IActionResult EditOrderStatus(EditOrderDto editDto)
    {
        var result = _order.EditOrder(editDto);
        if (result.Status != OperationResultStatus.Success)
        {
            ErrorAlert(result.Message);

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
        return RedirectAndShowAlert(result, RedirectToAction("Index"));

    }

    public IActionResult ShowEditModal(int orderId)
    {
        var order = _order.GetOrder(orderId);
        return PartialView("_EditOrder", new EditOrderDto()
        {
            OrderStatus = order.OrderStatus
        });
    }
}