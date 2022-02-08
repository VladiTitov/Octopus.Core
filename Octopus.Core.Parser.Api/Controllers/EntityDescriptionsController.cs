using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using Octopus.Core.Common.Constants;
using Octopus.Core.Common.DynamicObject.Models;
using Octopus.Core.Loader.WebApi.Infrastructure.MongoDb.Interfaces;
using Swashbuckle.AspNetCore.Annotations;
using System;
using System.Collections.Generic;
using System.Net;
using System.Threading.Tasks;

namespace Octopus.Core.Parser.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EntityDescriptionsController : ControllerBase
    {
        private readonly ILogger<EntityDescriptionsController> _logger;
        private readonly IMongoRepository _mongoRepository;

        public EntityDescriptionsController(ILogger<EntityDescriptionsController> logger, 
            IMongoRepository mongoRepository)
        {
            _logger = logger;
            _mongoRepository = mongoRepository;
        }

        /// <summary>
        /// Возвращает список всех моделей в базе
        /// </summary>
        /// <returns></returns>
        /// <exception cref="NotImplementedException"></exception>
        [HttpGet]
        [SwaggerResponse((int)HttpStatusCode.OK, "Список моделей", typeof(List<DynamicEntityWithProperties>))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Произошла внутренняя ошибка сервера в процессе извлечения списка моделей", typeof(HttpStatusCode))]
        public async Task<IActionResult> GetEntities()
        {
            throw new NotImplementedException();
        }

        /// <summary>
        /// Добавление новой модели
        /// </summary>
        /// <param name="entity"> Имя модели, список динамических свойств </param>
        /// <returns></returns>
        [HttpPost]
        [SwaggerResponse((int)HttpStatusCode.OK, "Добавление новой модели прошло успешно", typeof(HttpStatusCode))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Произошла внутренняя ошибка сервера в процессе добавления новой модели", typeof(HttpStatusCode))]
        public async Task<IActionResult> CreateEntity(DynamicEntityWithProperties entity)
        {
            _logger.LogInformation($"Entity added at: {DateTime.Now}");
            await _mongoRepository.AddEntity(entity);

            return Ok(ApiStatusMessages.DynamicEntityAddedMessage);
        }

        /// <summary>
        /// Поиск модели в базе по имени
        /// </summary>
        /// <param name="name"> Имя модели </param>
        /// <returns></returns>
        [HttpGet("{name}", Name = "GetEntityById")]
        [SwaggerResponse((int)HttpStatusCode.OK, "Модель найдена", typeof(DynamicEntityWithProperties))]
        [SwaggerResponse((int)HttpStatusCode.NotFound, "Модель не найдена", typeof(HttpStatusCode))]
        [SwaggerResponse((int)HttpStatusCode.InternalServerError, "Произошла внутренняя ошибка сервера в процессе поиска модели", typeof(HttpStatusCode))]
        public async Task<IActionResult> GetEntityByName(string name)
        {
            var entity = await _mongoRepository.GetEntity(name);

            if (entity == null)
            {
                return NotFound();
            }

            return Ok(entity);
        }
    }
}
