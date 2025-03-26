﻿// <auto-generated />
using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage.ValueConversion;
using Npgsql.EntityFrameworkCore.PostgreSQL.Metadata;

#nullable disable

namespace Hourly.Data.Migrations
{
    [DbContext(typeof(AppDbContext))]
    partial class AppDbContextModelSnapshot : ModelSnapshot
    {
        protected override void BuildModel(ModelBuilder modelBuilder)
        {
#pragma warning disable 612, 618
            modelBuilder
                .HasAnnotation("ProductVersion", "9.0.3")
                .HasAnnotation("Relational:MaxIdentifierLength", 63);

            NpgsqlModelBuilderExtensions.UseIdentityByDefaultColumns(modelBuilder);

            modelBuilder.Entity("Hourly.Shared.Models.Department", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ManagerId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("ManagerId");

                    b.ToTable("Departments");
                });

            modelBuilder.Entity("Hourly.Shared.Models.GitCommit", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("AuthorId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("ExtCommitId")
                        .HasColumnType("uuid");

                    b.Property<string>("ExtCommitShortId")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RepositoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Title")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WebUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.HasIndex("AuthorId");

                    b.HasIndex("RepositoryId");

                    b.ToTable("GitCommits");
                });

            modelBuilder.Entity("Hourly.Shared.Models.GitRepository", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<Guid>("ExtRepositoryId")
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Namespace")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("WebUrl")
                        .IsRequired()
                        .HasColumnType("text");

                    b.HasKey("Id");

                    b.ToTable("GitRepositories");
                });

            modelBuilder.Entity("Hourly.Shared.Models.Role", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Permissions")
                        .IsRequired()
                        .HasColumnType("jsonb");

                    b.HasKey("Id");

                    b.ToTable("Roles");
                });

            modelBuilder.Entity("Hourly.Shared.Models.User", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("CreatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.Property<Guid>("DepartmentId")
                        .HasColumnType("uuid");

                    b.Property<string>("Email")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GitAccessToken")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GitEmail")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("GitUsername")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<string>("Name")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("RoleId")
                        .HasColumnType("uuid");

                    b.Property<DateTime>("UpdatedAt")
                        .HasColumnType("timestamp with time zone");

                    b.HasKey("Id");

                    b.HasIndex("DepartmentId");

                    b.HasIndex("RoleId");

                    b.ToTable("Users");
                });

            modelBuilder.Entity("Hourly.Shared.Models.WorkSession", b =>
                {
                    b.Property<Guid>("Id")
                        .ValueGeneratedOnAdd()
                        .HasColumnType("uuid");

                    b.Property<DateTime>("EndTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<float>("Factor")
                        .HasColumnType("real");

                    b.Property<string>("OtherRemarks")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<DateTime>("StartTime")
                        .HasColumnType("timestamp with time zone");

                    b.Property<string>("TaskDescription")
                        .IsRequired()
                        .HasColumnType("text");

                    b.Property<Guid>("UserId")
                        .HasColumnType("uuid");

                    b.Property<bool>("WBSO")
                        .HasColumnType("boolean");

                    b.HasKey("Id");

                    b.HasIndex("UserId");

                    b.ToTable("WorkSessions");
                });

            modelBuilder.Entity("Hourly.Shared.Models.WorkSessionGitCommit", b =>
                {
                    b.Property<Guid>("WorkSessionId")
                        .HasColumnType("uuid");

                    b.Property<Guid>("GitCommitId")
                        .HasColumnType("uuid");

                    b.HasKey("WorkSessionId", "GitCommitId");

                    b.HasIndex("GitCommitId");

                    b.ToTable("WorkSessionGitCommits");
                });

            modelBuilder.Entity("Hourly.Shared.Models.Department", b =>
                {
                    b.HasOne("Hourly.Shared.Models.User", "Manager")
                        .WithMany()
                        .HasForeignKey("ManagerId")
                        .OnDelete(DeleteBehavior.Restrict)
                        .IsRequired();

                    b.Navigation("Manager");
                });

            modelBuilder.Entity("Hourly.Shared.Models.GitCommit", b =>
                {
                    b.HasOne("Hourly.Shared.Models.User", "Author")
                        .WithMany("GitCommits")
                        .HasForeignKey("AuthorId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hourly.Shared.Models.GitRepository", "Repository")
                        .WithMany("GitCommits")
                        .HasForeignKey("RepositoryId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Author");

                    b.Navigation("Repository");
                });

            modelBuilder.Entity("Hourly.Shared.Models.User", b =>
                {
                    b.HasOne("Hourly.Shared.Models.Department", "Department")
                        .WithMany("Users")
                        .HasForeignKey("DepartmentId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hourly.Shared.Models.Role", "Role")
                        .WithMany("Users")
                        .HasForeignKey("RoleId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("Department");

                    b.Navigation("Role");
                });

            modelBuilder.Entity("Hourly.Shared.Models.WorkSession", b =>
                {
                    b.HasOne("Hourly.Shared.Models.User", "User")
                        .WithMany("WorkSessions")
                        .HasForeignKey("UserId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("User");
                });

            modelBuilder.Entity("Hourly.Shared.Models.WorkSessionGitCommit", b =>
                {
                    b.HasOne("Hourly.Shared.Models.GitCommit", "GitCommit")
                        .WithMany("WorkSessionGitCommits")
                        .HasForeignKey("GitCommitId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.HasOne("Hourly.Shared.Models.WorkSession", "WorkSession")
                        .WithMany("WorkSessionGitCommits")
                        .HasForeignKey("WorkSessionId")
                        .OnDelete(DeleteBehavior.Cascade)
                        .IsRequired();

                    b.Navigation("GitCommit");

                    b.Navigation("WorkSession");
                });

            modelBuilder.Entity("Hourly.Shared.Models.Department", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Hourly.Shared.Models.GitCommit", b =>
                {
                    b.Navigation("WorkSessionGitCommits");
                });

            modelBuilder.Entity("Hourly.Shared.Models.GitRepository", b =>
                {
                    b.Navigation("GitCommits");
                });

            modelBuilder.Entity("Hourly.Shared.Models.Role", b =>
                {
                    b.Navigation("Users");
                });

            modelBuilder.Entity("Hourly.Shared.Models.User", b =>
                {
                    b.Navigation("GitCommits");

                    b.Navigation("WorkSessions");
                });

            modelBuilder.Entity("Hourly.Shared.Models.WorkSession", b =>
                {
                    b.Navigation("WorkSessionGitCommits");
                });
#pragma warning restore 612, 618
        }
    }
}
