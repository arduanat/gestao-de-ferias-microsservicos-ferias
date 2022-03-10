
using Dominio.ValueObjects.Enums;

namespace Dominio.Models
{
    public class HomologacaoDeFerias
    {
        public HomologacaoDeFerias(int feriasId, SituacaoDasFerias situacaoDasFerias, int id = 0)
        {
            Id = id;
            FeriasId = feriasId;
            SituacaoDasFerias = situacaoDasFerias;
        }

        public HomologacaoDeFerias()
        {
        }

        public int Id { get; set; }
        public int FeriasId { get; set; }
        public SituacaoDasFerias SituacaoDasFerias { get; set; }
        public Ferias Ferias { get; set; }
    }
}