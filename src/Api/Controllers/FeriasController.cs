using Api;
using Api.AppServices;
using Api.AppServices.DTOs;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeriasController : ControllerBase
    {
        private readonly FeriasAppService feriasAppService;

        public FeriasController(FeriasAppService contexto)
        {
            this.feriasAppService = contexto;
        }

        [HttpGet("ObterFerias")]
        public async Task<List<FeriasDto>> ObterFerias()
        {
            var ferias = await feriasAppService.ListarFerias();
            return ferias;
        }

        [HttpPost("MarcarFerias")]
        public async Task<Response> MarcarFerias([FromBody] List<PeriodoDeFeriasDto> periodos)
        {
            try
            {
                await feriasAppService.CadastrarFeriasParaTodosOsColaboradores(periodos);
                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);
            }
        }

        [HttpPost("AprovarFerias")]
        public async Task<Response> AprovarFerias()
        {
            try
            {
                await feriasAppService.AprovarTodasAsFeriasDosColaboradores();
                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);
            }
        }
        
        [HttpDelete("LimparBanco")]
        public async Task<Response> LimparBanco()
        {
            try
            {
                await feriasAppService.LimparBanco();
                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);
            }
        }
    }
}