using Microsoft.AspNetCore.Mvc;
using Restaraunt_Lobanov.Context;
using Restaraunt_Lobanov.Models;

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
        public ActionResult GetDishes()
        {
            DishContext context = new DishContext();

            try
            {
                var dishes = context.Dishes.ToList();

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
                        x.Dishes
                    }).ToList();

                return Ok(history);
            }
            catch
            {
                return StatusCode(400);
            }
        }

        [ApiExplorerSettings(GroupName = "v1")]
        /// <summary>
        /// Отправить заказ
        /// </summary>
        /// <remarks>Данный метод осуществляет отправку заказа</remarks>
        /// <response code="200">Заказ принят</response>
        /// <response code="400">Проблемы при запросе</response>
        /// <response code="401">Неавторизированный доступ</response>
        [Route("order")]
        [HttpPost]
        [ProducesResponseType(200)]
        [ProducesResponseType(400)]
        [ProducesResponseType(401)]
        public ActionResult CreateOrder([FromHeader] string token, [FromBody] Order order)
        {
            DishContext context = new DishContext();

            try
            {
                int? userId = JwtToken.GetUserIdFromToken(token);

                if (userId == null)
                {
                    return StatusCode(401);
                }

                order.UserId = userId.Value;

                context.Orders.Add(order);
                context.SaveChanges();

                return StatusCode(200);
            }
            catch
            {
                return StatusCode(400);
            }
        }
    }
    
}