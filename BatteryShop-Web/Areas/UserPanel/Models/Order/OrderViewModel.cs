using System.ComponentModel.DataAnnotations;
using CoreLayer.DTOs.Order;

namespace BatteryShop_Web.Areas.UserPanel.Models.Order;

public class OrderViewModel
{
    public List<OrderFinallyDto> OrderFinallyDtos { get; set; }

}