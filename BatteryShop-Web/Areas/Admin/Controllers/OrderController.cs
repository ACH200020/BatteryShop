using BatteryShop_Web.Areas.Admin.Models.Order;
using CoreLayer.DTOs.Order;
using CoreLayer.DTOs.User;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;
using Microsoft.AspNetCore.Mvc;

namespace BatteryShop_Web.Areas.Admin.Controllers;

public class OrderController : AdminControllerBase
{

    #region Services

    private readonly IOrderService _orderService;

    public OrderController(IOrderService orderService)
    {
        _orderService = orderService;
    }

    #endregion


    
    public IActionResult Index()
    {
        ListOrderViewModel listOrderViewModel = new ListOrderViewModel();
        listOrderViewModel.OrderDtos = _orderService.OrdersThatPaymentFinaled();

        return View(listOrderViewModel);
    }

    [HttpPost]
    public IActionResult EditOrderStatus(EditOrderDto editDto, int orderId)
    {
        var result = _orderService.EditOrder(editDto, orderId);
        if (result.Status != OperationResultStatus.Success)
        {
            ErrorAlert(result.Message);

            return RedirectAndShowAlert(result, RedirectToAction("Index"));
        }
        return RedirectAndShowAlert(result, RedirectToAction("Index"));

    }

    public IActionResult ShowEditModal(int orderId)
    {
        var order = _orderService.GetOrder(orderId);
        return PartialView("_EditOrder", new EditOrderDto()
        {
            OrderStatus = order.OrderStatus
        });
    }
}