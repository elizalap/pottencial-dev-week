using Microsoft.EntityFrameworkCore;
using src.Models;

namespace src.Persistance;

public class DatabaseContext : DbContext
{
   public DatabaseContext
   (DbContextOptions<DatabaseContext> options
   ) : base(options)
   {

   }
   public DbSet<Pessoa> Pessoas { get; set; }
   public DbSet<Contrato> Contratos { get; set; }

   //Adiciona configurações quando cria os modelos para passar para o banco de dados, 
   protected override void OnModelCreating(ModelBuilder builder)
   {
      //transforma a classe em uma entidade no banco de dados
      builder.Entity<Pessoa>(tabela =>
      {
         tabela.HasKey(e => e.Id);
         tabela
         .HasMany(e => e.contratos)
         .WithOne()
         .HasForeignKey(c => c.PessoaId);
      });

      builder.Entity<Contrato>(tabela =>
      {
         tabela.HasKey(e => e.Id);
      });
   }

}