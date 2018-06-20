using BookService.Models;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Description;

namespace DividaService.Controllers
{
    public class DividasController : ApiController
    {
        private BookServiceContext db = new BookServiceContext();

        // GET: api/Dividas
        public IQueryable<DividaDTO> GetDividas()
        {
            var Dividas = from b in db.Dividas
                          select new DividaDTO()
                          {
                              Id = b.Id,
                              Descricao = b.Descricao,
                              ClienteNome = b.Cliente.Nome
                          };

            return Dividas;
        }


        [Route("api/dividas/total")]
        public decimal GetDividasTotal()
        {
            decimal total = db.Dividas.Where(d => d.Status == true).Sum(d => d.Preco);
            return total;
        }

        // GET: api/Dividas/5
        [ResponseType(typeof(DividaDetailDTO))]
        public async Task<IHttpActionResult> GetDivida(int id)
        {
            var Divida = await db.Dividas.Include(b => b.Cliente).Select(b =>
                new DividaDetailDTO()
                {
                    Id = b.Id,
                    Descricao = b.Descricao,
                    Data = b.Data,
                    Preco = b.Preco,
                    ClienteNome = b.Cliente.Nome,
                    Status = b.Status
                }).SingleOrDefaultAsync(b => b.Id == id);
            if (Divida == null)
            {
                return NotFound();
            }

            return Ok(Divida);
        }

        // PUT: api/Dividas/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> PutDivida(int id, Divida Divida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != Divida.Id)
            {
                return BadRequest();
            }

            db.Entry(Divida).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!DividaExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Dividas
        [ResponseType(typeof(Divida))]
        public async Task<IHttpActionResult> PostDivida(Divida Divida)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Dividas.Add(Divida);
            await db.SaveChangesAsync();

            // Load author name
            db.Entry(Divida).Reference(x => x.Cliente).Load();

            var dto = new DividaDTO()
            {
                Id = Divida.Id,
                Descricao = Divida.Descricao,
                ClienteNome = Divida.Cliente.Nome
            };

            return CreatedAtRoute("DefaultApi", new { id = Divida.Id }, dto);
        }

        // DELETE: api/Dividas/5
        [ResponseType(typeof(Divida))]
        public async Task<IHttpActionResult> DeleteDivida(int id)
        {
            Divida Divida = await db.Dividas.FindAsync(id);
            if (Divida == null)
            {
                return NotFound();
            }

            db.Dividas.Remove(Divida);
            await db.SaveChangesAsync();

            return Ok(Divida);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool DividaExists(int id)
        {
            return db.Dividas.Count(e => e.Id == id) > 0;
        }
    }
}