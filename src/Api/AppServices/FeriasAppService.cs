using Api.AppServices.DTOs;
using Api.AppServices.Factories;
using App.Services;
using Dominio.Context;
using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Api.AppServices
{
    public class FeriasAppService
    {
        private readonly Contexto contexto;
        private readonly ColaboradorService colaboradorService;

        public FeriasAppService(Contexto contexto, ColaboradorService colaboradorService)
        {
            this.contexto = contexto;
            this.colaboradorService = colaboradorService;
        }

        public async Task<List<FeriasDto>> ListarFerias()
        {
            var colaboradores = await colaboradorService.Buscar();
            var idDosColaboradores = colaboradores.Select(x => x.Id).ToList();
            var ferias = await contexto.Ferias.Include(x => x.PeriodosDeFerias).Include(x => x.Homologacao).Where(x => idDosColaboradores.Contains(x.ColaboradorId)).ToListAsync();
            return FeriasFactory.ListarFerias(ferias, colaboradores);
        }

        public async Task CadastrarFeriasParaTodosOsColaboradores(List<PeriodoDeFeriasDto> periodosDto)
        {
            var feriasDosColaboradores = new List<Ferias>();
            var colaboradores = await colaboradorService.Buscar();

            foreach (var colaborador in colaboradores)
            {
                var colaboradorPossuiFeriasCadastradas = await contexto.Ferias.AnyAsync(x => x.ColaboradorId == colaborador.Id && x.AnoDeExercicio == DateTime.Now.Year);

                if (!colaboradorPossuiFeriasCadastradas)
                {
                    var ferias = FeriasFactory.CriarFerias(colaborador.Id, periodosDto);
                    feriasDosColaboradores.Add(ferias);
                }
            };

            contexto.AddRange(feriasDosColaboradores);
            await contexto.SaveChangesAsync();
        }

        public async Task AprovarTodasAsFeriasDosColaboradores()
        {
            var colaboradores = await colaboradorService.Buscar();
            var idDosColaboradores = colaboradores.Select(x => x.Id).ToList();

            var feriasDosColaboradores = await contexto.Ferias
                                                       .Include(x => x.PeriodosDeFerias)
                                                       .Include(x => x.Homologacao)
                                                       .Where(x => idDosColaboradores.Contains(x.ColaboradorId))
                                                       .ToListAsync();

            foreach (var ferias in feriasDosColaboradores)
            {
                if (ferias.Homologacao is null)
                    ferias.Aprovar();
            };

            contexto.UpdateRange(feriasDosColaboradores);
            await contexto.SaveChangesAsync();
        }
        
        public async Task LimparBanco()
        {
            var ferias = await contexto.Ferias
                                       .Include(x => x.PeriodosDeFerias)
                                       .Include(x => x.Homologacao)
                                       .ToListAsync();

            contexto.RemoveRange(ferias);
            await contexto.SaveChangesAsync();
        }
    }
}
