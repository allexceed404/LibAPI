// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;
using libraryAPI.EfCore;

#nullable disable

namespace libraryAPI.Migrations
{
    [DbContext(typeof(EF_DataContext))]
    partial class EF_DataContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "6.0.10")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.Property<int>("authorsid")
                        .HasColumnType("integer");

                    b.Property<int>("booksid")
                        .HasColumnType("integer");

                    b.HasKey("authorsid", "booksid");

                    b.HasIndex("booksid");

                    b.ToTable("AuthorBook");
                });

            modelBuilder.Entity("libraryAPI.EfCore.Author", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<string>("country")
                        .HasColumnType("text");

                    b.Property<DateTime?>("date_of_birth")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("author");
                });

            modelBuilder.Entity("libraryAPI.EfCore.Book", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<DateTime?>("date_of_first_publication")
                        .HasColumnType("timestamp with time zone");

                    b.Property<int>("edition")
                        .HasColumnType("integer");

                    b.Property<string>("isbn")
                        .HasColumnType("text");

                    b.Property<string>("name")
                        .HasColumnType("text");

                    b.Property<string>("original_language")
                        .HasColumnType("text");

                    b.Property<string>("publisher")
                        .HasColumnType("text");

                    b.HasKey("id");

                    b.ToTable("book");
                });

            modelBuilder.Entity("libraryAPI.EfCore.Relation", b =>
                {
                    b.Property<int>("id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("integer");

                    NpgsqlPropertyBuilderExtensions.UseIdentityByDefaultColumn(b.Property<int>("id"));

                    b.Property<int>("authorid")
                        .HasColumnType("integer");

                    b.Property<int>("bookid")
                        .HasColumnType("integer");

                    b.HasKey("id");

                    b.HasIndex("authorid");

                    b.HasIndex("bookid");

                    b.ToTable("relation");
                });

            modelBuilder.Entity("AuthorBook", b =>
                {
                    b.HasOne("libraryAPI.EfCore.Author", null)
                        .WithMany()
                        .HasForeignKey("authorsid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("libraryAPI.EfCore.Book", null)
                        .WithMany()
                        .HasForeignKey("booksid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();
                });

            modelBuilder.Entity("libraryAPI.EfCore.Relation", b =>
                {
                    b.HasOne("libraryAPI.EfCore.Author", "author")
                        .WithMany()
                        .HasForeignKey("authorid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("libraryAPI.EfCore.Book", "book")
                        .WithMany()
                        .HasForeignKey("bookid")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("author");

                    b.Navigation("book");
                });
#pragma warning restore 612, 618
        }
    }
}
