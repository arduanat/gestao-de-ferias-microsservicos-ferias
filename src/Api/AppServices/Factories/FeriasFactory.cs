using Api.AppServices.DTOs;
using Dominio.Models;
using System;
using System.Collections.Generic;
using System.Linq;

namespace Api.AppServices.Factories
{
    public static class FeriasFactory
    {
        public static Ferias CriarFerias(int colaboradorId, List<PeriodoDeFeriasDto> periodosDto)
        {
            var periodos = CriarPeriodosDeFerias(periodosDto);
            var ferias = new Ferias(colaboradorId, DateTime.Now.Year);
            ferias.AdicionarPeriodos(periodos);
            return ferias;
        }

        public static List<FeriasDto> ListarFerias(List<Ferias> feriasDosColaboradores, List<ColaboradorDto> colaboradores)
        {
            return !feriasDosColaboradores.Any() ? new List<FeriasDto>() : feriasDosColaboradores.Select(x => new FeriasDto()
            {
                Id = x.Id,
                ColaboradorId = x.ColaboradorId,
                AnoDeExercicio = x.AnoDeExercicio,
                Colaborador = colaboradores.First(y => y.Id == x.ColaboradorId),
                PeriodosDeFerias = CriarPeriodosDeFeriasDto(x.PeriodosDeFerias),
                Homologacao = CriarHomologacaoDeFerias(x.Homologacao)
            })
            .ToList();
        }

        public static List<PeriodoDeFerias> CriarPeriodosDeFerias(List<PeriodoDeFeriasDto> periodos)
        {
            return periodos.Select(x => new PeriodoDeFerias(x.DataInicial, x.QuantidadeDeDias, x.TipoDePeriodoDeFerias)).ToList();
        }

        public static List<PeriodoDeFeriasDto> CriarPeriodosDeFeriasDto(List<PeriodoDeFerias> periodos)
        {
            return periodos.Select(x => new PeriodoDeFeriasDto()
            {
                Id = x.Id,
                FeriasId = x.FeriasId,
                DataInicial = x.DataInicial,
                DataFinal = x.DataFinal,
                TipoDePeriodoDeFerias = x.TipoDePeriodoDeFerias
            })
            .ToList();
        }

        public static HomologacaoDeFeriasDto CriarHomologacaoDeFerias(HomologacaoDeFerias homologacao)
        {
            return homologacao is null ? null : new HomologacaoDeFeriasDto()
            {
                Id = homologacao.Id,
                FeriasId = homologacao.FeriasId,
                SituacaoDasFerias = homologacao.SituacaoDasFerias.ToString()
            };
        }
    }
}
