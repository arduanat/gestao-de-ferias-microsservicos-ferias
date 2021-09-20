using Api;
using Dominio.Context;
using Dominio.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace App.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class FeriasController : ControllerBase
    {
        private readonly Contexto contexto;

        public FeriasController(Contexto contexto)
        {
            this.contexto = contexto;
        }

        [HttpGet]
        public async Task<List<Ferias>> Get(int colaboradorId)
        {
            var consulta = contexto.Ferias
                                   .Include(x => x.PeriodosDeFerias)
                                   .AsQueryable();

            if(colaboradorId > 0)
            {
                consulta.Where(x => x.ColaboradorId == colaboradorId);
            }

            var ferias = await consulta.ToListAsync();

            return ferias;
        }

        [HttpPost]
        public async Task<Response> Post(int colaboradorId, List<PeriodoDeFerias> periodos)
        {
            try
            {
                var ferias = new Ferias(colaboradorId, DateTime.Now.Year);
                ferias.AdicionarPeriodos(periodos);
                contexto.Add(ferias);
                await contexto.SaveChangesAsync();
                return RequestResponse.Success();
            }
            catch (Exception exception)
            {
                return RequestResponse.Error(exception.Message);
            }
        }
    }
}