using Dominio.ValueObjects.Enums;
using System;

namespace Api.AppServices.DTOs
{
    public class PeriodoDeFeriasDto
    {
        public int Id { get; set; }
        public int FeriasId { get; set; }
        public int QuantidadeDeDias { get; set; }
        public DateTime DataInicial { get; set; }
        public DateTime DataFinal { get; set; }
        public TipoDePeriodoDeFerias TipoDePeriodoDeFerias { get; set; }
    }
}
