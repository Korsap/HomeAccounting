﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Web.Mvc;
using System.Web.SessionState;
using DomainModels.Model;
using WebUI.Models;
using Services;
using Services.Exceptions;
using WebUI.Exceptions;
using WebUI.Infrastructure.Attributes;

namespace WebUI.Controllers
{
    [Authorize]
    [SessionState(SessionStateBehavior.ReadOnly)]
    public class OrderController : Controller
    {
        private readonly IOrderService _orderService;        

        public OrderController(IOrderService orderService)
        {
            _orderService = orderService;            
        }
        
        public async Task<ActionResult> Index(WebUser user)
        {
            List<Order> orders;
            try
            {
                orders = (await _orderService.GetList(user.Id)).Where(u => u.Active).ToList();
            }
            catch (ServiceException e)
            {
                throw new WebUiException($"Ошибка в контроллере {nameof(OrderController)} в методе {nameof(Index)}", e);
            }
            
            return PartialView("_Index",orders);
        }

        public async Task<ActionResult> OrderList(WebUser user)
        {
            List<Order> orders;
            try
            {
                orders = (await _orderService.GetList(user.Id)).Where(u => u.Active).ToList();
            }
            catch (ServiceException e)
            {
                throw new WebUiException($"Ошибка в контроллере {nameof(OrderController)} в методе {nameof(OrderList)}", e);
            }
            
            return PartialView("_OrderList", orders);
        }

        [HttpPost]
        public async Task<ActionResult> Delete(int id)
        {
            try
            {
                await _orderService.DeleteAsync(id);
            }
            catch (ServiceException e)
            {
                throw new WebUiException($"Ошибка в контроллере {nameof(OrderController)} в методе {nameof(Delete)}", e);
            }
            return RedirectToAction("OrderList");
        }

        public async Task<ActionResult> Edit(int id)
        {
            Order order;
            try
            {
                order = await _orderService.GetOrderAsync(id);
            }
            catch (ServiceException e)
            {
                throw new WebUiException($"Ошибка в контроллере {nameof(OrderController)} в методе {nameof(Edit)}", e);
            }
            
            return PartialView("_OrderDetailsList", order);
        }

        
        [HttpPost]
        [UserHasAnyCategories]
        public async Task<ActionResult> Add(WebUser user)
        {
            try
            {
                await _orderService.CreateOrderAsync(DateTime.Now, user.Id);
            }
            catch (ServiceException e)
            {
                throw new WebUiException($"Ошибка в контроллере {nameof(OrderController)} в методе {nameof(Add)}", e);
            }
            
            return RedirectToAction("OrderList");
        }

        [HttpPost]
        public async Task SendEmail(int id)
        {
            var mailTo = ((WebUser)Session["WebUser"]).Email;
            if (mailTo != null)
            {
                try
                {
                    await Task.Run(() => _orderService.SendByEmail(id, ((WebUser)Session["WebUser"]).Email));
                }
                catch (ServiceException e)
                {
                    throw new WebUiException($"Ошибка в контроллере {nameof(OrderController)} в методе {nameof(SendEmail)}", e);
                }
            }             
        }

        [HttpPost]
        public async Task<ActionResult> CloseOrder(int id)
        {
            try
            {
                await _orderService.CloseOrder(id);
            }
            catch (ServiceException e)
            {
                throw new WebUiException($"Ошибка в контроллере {nameof(OrderController)} в методе {nameof(CloseOrder)}", e);
            }
            
            return RedirectToAction("OrderList");
        }
    }
}