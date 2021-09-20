using Dominio.Models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Dominio.Context.Mapping
{
    public class FeriasMap : IEntityTypeConfiguration<Ferias>
    {
        public void Configure(EntityTypeBuilder<Ferias> builder)
        {
            builder.HasKey(x => x.Id);
            builder.Property(x => x.ColaboradorId).IsRequired();
            builder.Property(x => x.AnoDeExercicio).IsRequired();
        }
    }
}