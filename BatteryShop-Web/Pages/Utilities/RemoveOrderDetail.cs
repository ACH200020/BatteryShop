using CoreLayer.DTOs.OrderDetails;
using CoreLayer.Services.OrderDetails;
using CoreLayer.Services.Orders;
using CoreLayer.Utilities;

namespace BatteryShop_Web.Pages.Utilities;

public class RemoveOrderDetail
{
    private readonly IOrderService _order;
    private readonly IOrderDetailService _orderDetail;

    public RemoveOrderDetail(IOrderService order, IOrderDetailService orderDetail)
    {
        _order = order;
        _orderDetail = orderDetail;
    }

    public OperationResult EditOrder(int id, int userId)
    {
        var orderDetail = _orderDetail.GetOrderDetail(id);
        var result = _orderDetail.RemoveOrderDetail(id);
        if (result.Status == OperationResultStatus.Success)
        {
            var order = _order.GetOrderByUserId(userId); 
            _order.EditPrice(orderDetail.Price, order.Id);
        }
        return result;
    }


}