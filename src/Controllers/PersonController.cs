using src.Persistance;
using src.Models;

using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Net;



namespace src.Controllers;

[ApiController]
[Route("[controller]")]
public class PersonController : ControllerBase
{
   private DatabaseContext _context { get; set; }

   public PersonController(DatabaseContext context)
   {
      this._context = context;
   }

   [HttpGet]
   public ActionResult<List<Pessoa>> Get()
   {
      /*Pessoa pessoa = new Pessoa("eliza", 22, "12345678");
      Contrato novoContrato = new Contrato("abc", 50.46);
      pessoa.contratos.Add(novoContrato);
      */

      var result = _context.Pessoas.Include(p => p.contratos).ToList();
      if (!result.Any())
         return NoContent();

      return Ok(result);
   }

   [HttpPost]
   public ActionResult<Pessoa> Post([FromBody] Pessoa pessoa)
   {

      try
      {
         _context.Pessoas.Add(pessoa);
         _context.SaveChanges();
      }
      catch (System.Exception)
      {
         return BadRequest(new
         {

         });
      }

      return Created("Criado", pessoa);
   }

   [HttpPut("{id}")]
   public ActionResult<Object> Update(
      [FromRoute] int id,
      [FromBody] Pessoa pessoa
      )
   {
      var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

      if (result is null)
      {
         return NotFound(new
         {
            msg = "Registro não encontrado",
            status = HttpStatusCode.NotFound
         });
      }


      try
      {
         _context.Pessoas.Update(pessoa);
         _context.SaveChanges();
      }
      catch (System.Exception)
      {
         return BadRequest(new
         {
            msg = "Houve erro ao enviar solicitação de atualização do Id "
             + id,
            status = HttpStatusCode.OK
         });
      }

      return Ok(new
      {
         msg = "Houve erro ao enviar solicitação de atualização do Id "
             + id,
         status = HttpStatusCode.OK
      });
   }


   [HttpDelete("{id}")]
   public ActionResult<Object> Delete([FromRoute] int id)
   {
      var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

      if (result is null)
      {
         return BadRequest(new
         {
            msg = "Conteúdo inexistente, solicitação inválida.",
            status = HttpStatusCode.BadRequest
         });
      }

      _context.Pessoas.Remove(result);
      _context.SaveChanges();

      return Ok(new
      {
         msg = "Deletando pessoa de Id " + id,
         status = HttpStatusCode.OK
      });
   }

}