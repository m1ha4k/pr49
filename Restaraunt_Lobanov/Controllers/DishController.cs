using Microsoft.AspNetCore.Mvc;
using Restaraunt_Lobanov.Context;
using Restaraunt_Lobanov.Models;
using Restaraunt_Lobanov.Models.DTO;

namespace Restaraunt_Lobanov.Controllers

{
    [Route("api/dishes")]
    [ApiExplorerSettings(GroupName = "v2")]
    public class DishController : Controller
    {
        /// <summary>
        /// Получить список версий
        /// </summary>
        /// <remarks>Данный метод возвращает список доступных версий меню (завтрак, обед, ужин)</remarks>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Проблемы при запросе</response>
        [Route("version")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult GetVersions()
        {
            DishContext context = new DishContext();

            try
            {
                var versions = context.Dishes.Select(x => x.Version).ToList();

                return Ok(new { version = versions });
            }
            catch
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Получить список блюд
        /// </summary>
        /// <remarks>Данный метод возвращает список блюд</remarks>
        /// <response code="200">Успешное выполнение</response>
        /// <response code="400">Проблемы при запросе</response>
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        public ActionResult GetDishes([FromQuery] string version)
        {
            DishContext context = new DishContext();

            try
            {
                var dishes = context.Dishes
                    .Where(x => x.Version == version)
                    .ToList();
                if (dishes.Count == 0)
                {
                    return Ok(new { message = "Блюда с версией " + version + "не найдены" });
                }
                return Ok(dishes);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        /// <summary>
        /// Получить историю
        /// </summary>
        /// <remarks>Данный метод осуществляет получение истории</remarks>
        /// <response code="200">История получена</response>
        /// <response code="400">Проблемы при запросе</response>
        /// <response code="401">Неавторизированный доступ</response>
        [Route("histories")]
        [HttpGet]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult GetHistory([FromHeader] string token)
        {
            DishContext context = new DishContext();

            try
            {
                int? userId = JwtToken.GetUserIdFromToken(token);

                if (userId == null)
                {
                    return StatusCode(401);
                }

                var history = context.Orders.Where(x => x.UserId == userId)
                    .Select(x => new
                    {
                        x.Address,
                        x.Date,
                        Dishes = x.Dishes.Select(d => new
                        {
                            d.DishId,
                            d.Count
                        })
                    }).ToList();

                return Ok(history);
            }
            catch
            {
                return StatusCode(400);
            }
        }


        /// <summary>
        /// Отправить заказ
        /// </summary>
        /// <remarks>Данный метод осуществляет отправку заказа</remarks>
        /// <response code="200">Заказ принят</response>
        /// <response code="400">Проблемы при запросе</response>
        /// <response code="401">Неавторизированный доступ</response>
        [ApiExplorerSettings(GroupName = "v1")]
        [Route("order")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult CreateOrder([FromBody] OrderRequest request, [FromHeader] string token)
        {
            DishContext context = new DishContext();

            try
            {
                int? userId = JwtToken.GetUserIdFromToken(token);

                if (userId == null)
                {
                    return StatusCode(401);
                }

                var order = new Order
                {
                    UserId = userId.Value,
                    Address = request.Address,
                    Date = request.Date,
                    Dishes = request.Dishes.Select(d => new OrderDish
                    {
                        DishId = d.DishId,
                        Count = d.Count
                    }).ToList()
                };

                context.Orders.Add(order);
                context.SaveChanges();

                return Ok(new { message = "Заказ принят", orderId = order.OrderId });
            }
            catch (Exception ex)
            {
                return StatusCode(400, new { error = ex.Message });
            }
        }
    }

}