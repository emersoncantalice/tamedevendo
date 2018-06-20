namespace BookService.Migrations
{
    using System;
    using System.Data.Entity;
    using System.Data.Entity.Migrations;
    using System.Linq;
    using BookService.Models;

    internal sealed class Configuration : DbMigrationsConfiguration<BookService.Models.BookServiceContext>
    {
        public Configuration()
        {
            AutomaticMigrationsEnabled = true;
        }

        protected override void Seed(BookService.Models.BookServiceContext context)
        {
            context.Clientes.AddOrUpdate(x => x.Id,
               new Cliente() { Id = 1, Nome = "Luan", Telefone = "83988219689" }
           );

            context.Clientes.AddOrUpdate(x => x.Id,
              new Cliente() { Id = 2, Nome = "Thayle", Telefone = "83988214639" }
          );


            context.Dividas.AddOrUpdate(x => x.Id,
                new Divida()
                {
                    Id = 1,
                    Descricao = "Bola do rocha",
                    Data = DateTime.Now,
                    ClienteId = 1,
                    Preco = 120,
                    Status = true
                },
                 new Divida()
                 {
                     Id = 2,
                     Descricao = "NetFlix",
                     Data = DateTime.Now,
                     ClienteId = 2,
                     Preco = 37,
                     Status = false
                 });
        }
    }
}
