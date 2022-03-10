using System.Collections.Generic;

namespace Api.AppServices.DTOs
{
    public class FeriasDto
    {
        public int Id { get; set; }
        public int ColaboradorId { get; set; }
        public int AnoDeExercicio { get; set; }
        public ColaboradorDto Colaborador { get; set; }
        public List<PeriodoDeFeriasDto> PeriodosDeFerias { get; set; }
        public HomologacaoDeFeriasDto Homologacao { get; set; }
    }
}
