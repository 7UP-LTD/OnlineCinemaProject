using Microsoft.EntityFrameworkCore;
using OnlineCinema.Data.Entities;

namespace OnlineCinema.Data
{
    /// <summary>
    /// Класс контекста базы данных
    /// </summary>
    public class ApplicationDbContext : DbContext
    {
        /// <summary>
        /// Конструктор класса ApplicationDbContext, который принимает настройки контекста базы данных.
        /// </summary>
        /// <param name="options">Настройки контекста базы данных.</param>
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<UserEntity> Users { get; set; }
        public DbSet<UserFavoriteMovieEntity> UserFavoriteMovies { get; set; }
        public DbSet<UserFutureMovieEntity> UserFutureMovies { get; set; }
        public DbSet<UserMovieLikeEntity> UserMovieLikes { get; set; }
        public DbSet<UserMovieViewedEntity> UserMovieViewed { get; set; }

        public DbSet<MovieEntity> Movies { get; set; }
        public DbSet<MovieActorEntity> MovieActors { get; set; }
        public DbSet<MovieDirectorEntity> MovieDirectors { get; set; }
        public DbSet<MovieWriterEntity> MovieWriters { get; set; }
        public DbSet<MovieCommentEntity> MovieComments { get; set; }
        public DbSet<MovieGenreEntity> MovieGenres { get; set; }
        public DbSet<MovieSeasonEntity> MovieSeasons { get; set; }
        public DbSet<MovieTagEntity> MovieTags { get; set; }

        public DbSet<MovieEpisodeEntity> MovieEpisodes { get; set; }
        public DbSet<EpisodeCommentEntity> EpisodeComments { get; set; }

        public DbSet<NotificationEntity> Notifications { get; set; }

        public DbSet<DicActorEntity> DicActors { get; set; }
        public DbSet<DicDirectorEntity> DicDirectors { get; set; }
        public DbSet<DicGenreEntity> DicGenres { get; set; }
        public DbSet<DicTagEntity> DicTags { get; set; }
        public DbSet<DicWriterEntity> DicWriters { get; set; }

        public DbSet<PersonEntity> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<UserEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserEntity>()
                .Property(x => x.Email)
                .IsRequired();
            modelBuilder.Entity<UserEntity>()
                .Property(x => x.Password)
                .IsRequired();
            
            modelBuilder.Entity<UserFavoriteMovieEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserFutureMovieEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserMovieLikeEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<UserMovieViewedEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieActorEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieDirectorEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieWriterEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieCommentEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieGenreEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieSeasonEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieTagEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<MovieEpisodeEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<EpisodeCommentEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<NotificationEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<DicActorEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<DicDirectorEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<DicGenreEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<DicTagEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<DicWriterEntity>()
                .HasKey(x => x.Id);
            modelBuilder.Entity<PersonEntity>()
                .HasKey(x => x.Id);
         
            modelBuilder.Entity<MovieEntity>()
                .HasMany(x => x.Seasons)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MovieEntity>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MovieEntity>()
                .HasMany(x => x.Genres)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MovieEntity>()
                .HasMany(x => x.Tags)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MovieEntity>()
                .HasMany(x => x.Actors)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);

            modelBuilder.Entity<MovieEntity>()
                .HasMany(x => x.Directors)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MovieEntity>()
                .HasMany(x => x.Writers)
                .WithOne(x => x.Movie)
                .HasForeignKey(x => x.MovieId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MovieSeasonEntity>()
                .HasMany(x => x.Episodes)
                .WithOne(x => x.MovieSeason)
                .HasForeignKey(x => x.SeasonId)
                .OnDelete(DeleteBehavior.Cascade);
            
            modelBuilder.Entity<MovieEpisodeEntity>()
                .HasMany(x => x.Comments)
                .WithOne(x => x.Episode)
                .HasForeignKey(x => x.EpisodeId)
                .OnDelete(DeleteBehavior.Cascade);
     
        }
    }
}