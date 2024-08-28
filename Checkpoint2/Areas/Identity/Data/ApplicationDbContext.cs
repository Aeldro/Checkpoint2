using Checkpoint2.Areas.Identity.Data;
using Checkpoint2.Models.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Checkpoint2.Data;

public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<Book> Books { get; set; }
    public DbSet<Author> Authors { get; set; }
    public DbSet<Category> Categories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        // Customize the ASP.NET Identity model and override the defaults if needed.
        // For example, you can rename the ASP.NET Identity table names and more.
        // Add your customizations after calling base.OnModelCreating(builder);

        builder.Entity<Category>().HasData(
        new Category()
        {
            Id = 1,
            Name = "Thriller"
        },
        new Category()
        {
            Id = 2,
            Name = "Fantaisie"
        },
        new Category()
        {
            Id = 3,
            Name = "Horreur"
        }
        );

        builder.Entity<Author>().HasData(
            new Author()
            {
                Id = 1,
                Name = "Patrick",
                Biography = "Il est mort avant d'être né."
            },
            new Author()
            {
                Id = 2,
                Name = "Cassandra",
                Biography = "Elle est devenue écrivaine à la suite d'une soirée bien arrosée."
            },
            new Author()
            {
                Id = 3,
                Name = "Philippe",
                Biography = "Il s'est pris de passion pour la littérature le jour où il a perdu son cheval."
            }
        );

        builder.Entity<Book>().HasData(
            new Book()
            {
                Id = 1,
                Title = "Tout est bon dans la corruption",
                AuthorId = 1,
                CategoryId = 1,
                Price = 19.99,
                Description = "Pour les amateurs de flics corrompus.",
                ISBN = "45-45-45-45"
            },
            new Book()
            {
                Id = 2,
                Title = "Avec un peu de chance ça passe",
                AuthorId = 2,
                CategoryId = 1,
                Price = 0.99,
                Description = "Huit clos franchement pas terrible. D'où son prix.",
                ISBN = "46-45-45-45"
            },
            new Book()
            {
                Id = 3,
                Title = "Le merveilleux voyage d'un sac poubelle",
                AuthorId = 1,
                CategoryId = 2,
                Price = 19.99,
                Description = "Vous aussi vous avez toujours rêvé de vous mettre dans la peau d'un sac poubelle? Vous êtes vraiment étrange...",
                ISBN = "45-45-45-158"
            },
            new Book()
            {
                Id = 4,
                Title = "Les moldus",
                AuthorId = 3,
                CategoryId = 2,
                Price = 19.99,
                Description = "Pour les fans d'Harry Potter qui n'aiment pas trop la magie.",
                ISBN = "45-459-45-45"
            },
            new Book()
            {
                Id = 5,
                Title = "Bouh !",
                AuthorId = 2,
                CategoryId = 3,
                Price = 19.99,
                Description = "Ahh !!",
                ISBN = "45-45-28-28"
            },
            new Book()
            {
                Id = 6,
                Title = "Maman j'ai peur",
                AuthorId = 3,
                CategoryId = 3,
                Price = 19.99,
                Description = "Une mère indigne demande à sa fille de ne pas venir la rejoindre dans sa chambre de peur que le monstre sous son lit ne la suive jusqu'à elle.",
                ISBN = "45-555-45"
            },
            new Book()
            {
                Id = 7,
                Title = "L'histoire d'un nouveau développeur",
                Price = 99.99,
                Description = "Oups, j'ai oublié l'auteur et la catégorie.",
                ISBN = "45-45-45-45"
            }
            );

    }
}
