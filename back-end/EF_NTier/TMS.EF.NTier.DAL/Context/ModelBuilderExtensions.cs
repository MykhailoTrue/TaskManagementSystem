using Bogus;
using Microsoft.EntityFrameworkCore;
using System.Text.Json;
using TMS.EF.NTier.Common.DTO.Users;
using TMS.EF.NTier.DAL.Entities;

namespace TMS.EF.NTier.DAL.Context
{
    public static class ModelBuilderExtensions
    {
        private static HttpClient _client = new HttpClient()
        {
            BaseAddress = new Uri("https://localhost:7171/"),
        };

        private static JsonSerializerOptions _jsonSerializerOptions = new JsonSerializerOptions
        {
            PropertyNameCaseInsensitive = true
        };

        private const int ENTITY_COUNT = 10;
        public static void Configure(this ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Issue>(entity =>
            {
                // can't configure without principal entity
                /*entity.HasOne(d => d.Asignee).WithMany(p => p.Issues)
                    .HasForeignKey(d => d.AsigneeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_To_Users_AsigneeId");*/

                entity.HasOne(d => d.IssueType).WithMany(p => p.Issues)
                    .HasForeignKey(d => d.IssueTypeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("FK_Issues_To_IssueTypes");

                entity.HasOne(d => d.ProjectColumn).WithMany(p => p.Issues)
                    .HasForeignKey(d => d.ProjectColumnId)
                    .HasConstraintName("FK_Issues_To_ProjectColumns");
            });

            modelBuilder.Entity<IssueType>(entity =>
            {
                entity.HasOne(d => d.Project).WithMany(p => p.IssueTypes)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_IssueTypes_To_Projects");
            });

            modelBuilder.Entity<Project>(entity =>
            {
                entity.HasKey(e => e.Id).HasName("PK_Project_Id");

                // can't configure without principal entity
                /*entity.HasOne(d => d.ProjectCategory).WithMany(p => p.Projects)
                    .HasForeignKey(d => d.ProjectCategoryId)
                    .HasConstraintName("FK_Projects_To_Categories");*/

                // can't configure without principal entity
                /*entity.HasOne(d => d.Workspace).WithMany(p => p.Projects)
                    .HasForeignKey(d => d.WorkspaceId)
                    .HasConstraintName("FK_Projects_To_Workspaces");*/
            });

            modelBuilder.Entity<ProjectColumn>(entity =>
            {
                entity.HasOne(d => d.Project).WithMany(p => p.ProjectColumns)
                    .HasForeignKey(d => d.ProjectId)
                    .HasConstraintName("FK_ProjectColumns_To_Projects");
            });
        }

        public static void Seed(this ModelBuilder modelBuilder)
        {
            Randomizer.Seed = new Random(876321);

            var users = GetExistingUsers();
            var projects = GetExistingProjects();
            var projectColumns = GenerateRandomProjectColumns(projects);
            var issueTypes = GenerateRandomIssueTypes(projects);
            var issues = GenerateRandomIssues(users, projectColumns, issueTypes);

            modelBuilder.Entity<ProjectColumn>()
                .HasData(projectColumns);

            modelBuilder.Entity<IssueType>()
                .HasData(issueTypes);

            modelBuilder.Entity<Issue>()
                .HasData(issues);
        }

        private static IEnumerable<Project> GetExistingProjects()
        {
            var response = _client.GetAsync("api/Projects").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(content);

            var projects = JsonSerializer.Deserialize<IEnumerable<Project>>(content, _jsonSerializerOptions);
            return projects;
        }

        private static IEnumerable<UserReadDTO> GetExistingUsers()
        {
            var response = _client.GetAsync("api/Users").Result;
            var content = response.Content.ReadAsStringAsync().Result;

            Console.WriteLine(content);

            var users = JsonSerializer.Deserialize<IEnumerable<UserReadDTO>>(content, _jsonSerializerOptions);
            return users;
        }

        private static IEnumerable<Issue> GenerateRandomIssues(IEnumerable<UserReadDTO> users, IEnumerable<ProjectColumn> projectColumns, IEnumerable<IssueType> issueTypes)
        {
            int issueId = 1;
            var issueFaker = new Faker<Issue>()
                .RuleFor(i => i.Id, _ =>  issueId++)
                .RuleFor(i => i.Name, f => f.Lorem.Word())
                .RuleFor(i => i.Descritption, f => f.Lorem.Sentence())
                .RuleFor(i => i.ProjectColumnId, f => f.PickRandom(projectColumns).Id)
                .RuleFor(i => i.IssueTypeId, f => f.PickRandom(issueTypes).Id)
                .RuleFor(i => i.AsigneeId, f => f.PickRandom(users).Id);

            return issueFaker.Generate(ENTITY_COUNT);

        }

        private static IEnumerable<ProjectColumn> GenerateRandomProjectColumns(IEnumerable<Project> projects)
        {
            int projectColumnId = 1;
            var projectColumnsFaker = new Faker<ProjectColumn>()
                .RuleFor(pc => pc.Id, _ => projectColumnId++)
                .RuleFor(pc => pc.Name, f => f.Lorem.Word())
                .RuleFor(pc => pc.ProjectId, f => f.PickRandom(projects).Id);

            return projectColumnsFaker.Generate(ENTITY_COUNT);
        }

        private static IEnumerable<IssueType> GenerateRandomIssueTypes(IEnumerable<Project> projects)
        {
            int issueType = 1;
            var issueTypeFaker = new Faker<IssueType>()
                .RuleFor(it => it.Id, _ => issueType++)
                .RuleFor(it => it.Name, f => f.Lorem.Word())
                .RuleFor(it => it.Description, f => f.Lorem.Paragraph())
                .RuleFor(it => it.ProjectId, f => f.PickRandom(projects).Id);

            return issueTypeFaker.Generate(ENTITY_COUNT);
        }
    }
}
