using AutoMapper;
using Microsoft.AspNetCore.Mvc;
using ProjectT.Dtos.Parameter;
using ProjectT.Dtos.ViewModel;
using ProjectT.Infrastructure.Mappings;
using ProjectT.Infrastructure.Validators;
using ProjectT.Repository;
using ProjectT.Service.Dtos.Info;
using ProjectT.Service.Dtos.ResultModel;
using ProjectT.Service.Impletement;
using ProjectT.Service.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace ProjectT.Controllers
{
    /// <summary>
    /// 卡片管理
    /// </summary>
    /// <seealso cref="Microsoft.AspNetCore.Mvc.ControllerBase" />
    [ApiController]
    [Route("[controller]")]
    public class CardController : ControllerBase
    {
        private readonly IMapper _mapper;
        private readonly ICardService _cardService;

        /// <summary>
        /// 建構式
        /// </summary>
        public CardController(IMapper mapper, ICardService cardService)
        {
            this._mapper = mapper;
            this._cardService = cardService;
        }

        /// <summary>
        /// 查詢卡片列表
        /// </summary>
        /// <param name="parameter">卡片查詢條件</param>
        /// <returns></returns>
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(IEnumerable<CardViewModel>), 200)]
        public IEnumerable<CardViewModel> GetList(
            [FromQuery] CardSearchParameter parameter)
        {
            var info = this._mapper.Map<CardSearchParameter, CardSearchInfo>(parameter);
            var cards = this._cardService.GetList(info);
            var result = this._mapper.Map<IEnumerable<CardResultModel>, IEnumerable<CardViewModel>>(cards);

            return result;
        }

        /// <summary>
        /// 查詢卡片
        /// </summary>
        /// <remarks>我是附加說明</remarks>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>      
        [HttpGet]
        [Produces("application/json")]
        [ProducesResponseType(typeof(CardViewModel), 200)]
        [Route("{id}")]
        public CardViewModel Get(
            [FromRoute] int id)
        {
            var card = this._cardService.Get(id);
            var result = this._mapper.Map<CardResultModel, CardViewModel>(card);

            return result;
        }

        /// <summary>
        /// 新增卡片
        /// </summary>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        /// <response code="200">回傳ok</response>
        /// <response code="400">參數檢查失敗</response>
        /// <response code="500">新增失敗</response>    
        [HttpPost]
        public IActionResult Insert(
            [FromBody] CardParameter parameter)
        {
            //// 使用DI取代
            //// 這邊需要對參數做檢查
            //var validator = new CardParameterValidator();
            //var validationResult = validator.Validate(parameter);

            //// 如果沒有通過檢查，就把訊息串一串丟回去
            //if (validationResult.IsValid is false)
            //{
            //    var errorMessages = validationResult.Errors.Select(e => e.ErrorMessage);
            //    var resultMessage = string.Join(",", errorMessages);
            //    return BadRequest(resultMessage); // 直接回傳 400 + 錯誤訊息
            //}

            var info = this._mapper.Map<CardParameter, CardInfo>(parameter);

            var isInsertSuccess = this._cardService.Insert(info);
            if (isInsertSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// 更新卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <param name="parameter">卡片參數</param>
        /// <returns></returns>
        /// <response code="200">回傳ok</response>
        /// <response code="400">找不到卡片</response>
        /// <response code="500">更新失敗</response>   
        [HttpPut]
        [Route("{id}")]
        public IActionResult Update(
            [FromRoute] int id,
            [FromBody] CardParameter parameter)
        {
            var targetCard = this._cardService.Get(id);
            if (targetCard is null)
            {
                return NotFound();
            }

            var info = this._mapper.Map<CardParameter, CardInfo>(parameter);

            var isUpdateSuccess = this._cardService.Update(id, info);
            if (isUpdateSuccess)
            {
                return Ok();
            }
            return StatusCode(500);
        }

        /// <summary>
        /// 刪除卡片
        /// </summary>
        /// <param name="id">卡片編號</param>
        /// <returns></returns>
        [HttpDelete]
        [Route("{id}")]
        public IActionResult Delete(
            [FromRoute] int id)
        {
            this._cardService.Delete(id);
            return Ok();
        }
    }
}
