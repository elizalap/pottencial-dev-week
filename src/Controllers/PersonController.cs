using Microsoft.AspNetCore.Mvc;
using src.Models;

using Microsoft.EntityFrameworkCore;
using src.Persistance;

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
   public Pessoa Post([FromBody] Pessoa pessoa)
   {
      _context.Pessoas.Add(pessoa);
      _context.SaveChanges();
      return pessoa;
   }

   [HttpPut("{id}")]
   public string Update([FromRoute] int id, [FromBody] Pessoa pessoa)
   {
      _context.Pessoas.Update(pessoa);
      _context.SaveChanges();

      return "Dados do id " + id + " atualizados";
   }

   [HttpDelete("{id}")]
   // public string Delete([FromRoute] int id)
   // {
   //    var result = _context.Pessoas.SingleOrDefault(e => e.Id == id);

   //    _context.Pessoas.Remove(result);
   //    _context.SaveChanges();

   //    return "Deletando pessoa de id " + id;
   // }

   public IActionResult Delete([FromRoute] int id)
   {
      //IActionResult - permite retorna status HTTP de forma mais agigável
      return NotFound();
   }

}