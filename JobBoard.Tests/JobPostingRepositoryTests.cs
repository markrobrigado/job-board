using JobBoard.Data;
using JobBoard.Models;
using JobBoard.Repositories;
using Microsoft.EntityFrameworkCore;

namespace JobBoard.Tests
{
    public class JobPostingRepositoryTests
    {
        private readonly DbContextOptions<ApplicationDbContext> _options;

        public JobPostingRepositoryTests()
        {
            _options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase("JobPostingDb").Options;
        }

        private ApplicationDbContext CreateDbContext() => new ApplicationDbContext(_options);

        [Fact]
        public async Task AddAsync_ShouldAddJobPosting()
        {
            // Create Db context instance
            var context = CreateDbContext();
            var repository = new JobPostingRepository(context);

            // Create a new JobPosting instance with test data
            var jobPosting = new JobPosting
            {
                Name = "Test",
                Description = "Test Description",
                CreatedDate = DateTime.UtcNow,
                Company = "Test Company",
                Location = "Test Location",
                UserId = "Test User ID"
            };

            // Add the job posting to the repository asynchronously
            await repository.AddAsync(jobPosting);

            // Verify that the job posting was added successfully
            var result = context.JobPostings.SingleOrDefault(x => x.Name == jobPosting.Name);

            Assert.NotNull(result);
            Assert.Equal(jobPosting.Name, result.Name);
        }
    }
}
