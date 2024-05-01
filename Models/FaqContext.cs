using Microsoft.EntityFrameworkCore;

namespace MyFAQEnhanced.Models
{
    public class FaqContext : DbContext
    {
        public FaqContext(DbContextOptions<FaqContext> options)
            : base(options)
        { }
        public DbSet<FAQ> FAQs { get; set; }
        public DbSet<Topic> Topics { get; set; }
        public DbSet<Category> Categories { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Topic>().HasData(
                new Topic
                {
                    TopicId = "cs",
                    Name = "C#"
                },
                new Topic
                {
                    TopicId = "js",
                    Name = "JavaScript"
                },
                new Topic
                {
                    TopicId = "boot",
                    Name = "Bootstrap"
                });

            modelBuilder.Entity<Category>().HasData(
                new Category
                {
                    CategoryId = "hist",
                    Name = "History"
                },
                new Category
                {
                    CategoryId = "gen",
                    Name = "General"
                });

            modelBuilder.Entity<FAQ>().HasData(
                new FAQ
                {
                    FAQId = 1,
                    Question = "What is C#?",
                    Answer = "A general purpose object oriented language\r\nthat uses a concise, Java-like syntax.",
                    TopicId = "cs",
                    CategoryId = "gen"
                },
                new FAQ
                {
                    FAQId = 2,
                    Question = "When was C# first\r\nreleased?",
                    Answer = "In 2002.",
                    TopicId = "cs",
                    CategoryId = "hist"
                },
                new FAQ
                {
                    FAQId = 3,
                    Question = "What is JavaScript?",
                    Answer = "A general purpose scripting language that\r\nexecutes in a web browser.",
                    TopicId = "js",
                    CategoryId = "gen"
                },
                new FAQ
                {
                    FAQId = 4,
                    Question = "When was JavaScript\r\nfirst released?",
                    Answer = "In 1995.",
                    TopicId = "js",
                    CategoryId = "hist"
                },
                new FAQ
                {
                    FAQId = 5,
                    Question = "What is Bootstrap?",
                    Answer = "A CSS framework for creating responsive\r\nweb apps for multiple screen sizes.",
                    TopicId = "boot",
                    CategoryId = "gen"
                },
                new FAQ
                {
                    FAQId = 6,
                    Question = "When was Bootstrap\r\nfirst released?",
                    Answer = "In 2011.",
                    TopicId = "boot",
                    CategoryId = "hist"
                });
        }
    }
}
