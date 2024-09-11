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
            var result = context.JobPostings.Find(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal(jobPosting.Name, result.Name);
        }

        [Fact]
        public async Task DeleteAsync_ShouldDeleteJobPosting()
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

            // Add the job posting to the in-memory database asynchronously
            await context.JobPostings.AddAsync(jobPosting);
            await context.SaveChangesAsync();

            // Remove the job posting
            await repository.DeleteAsync(jobPosting.Id);

            // Verify that the job posting was deleted successfully
            var result = context.JobPostings.Find(jobPosting.Id);

            Assert.Null(result);
        }

        [Fact]
        public async Task GetAllAsync_ShouldReturnAllJobPostings()
        {
            // Create Db context instance
            var context = CreateDbContext();
            var repository = new JobPostingRepository(context);

            // Create two JobPosting instance with test data
            var jobPosting1 = new JobPosting
            {
                Name = "Test 1",
                Description = "Test Description 1",
                CreatedDate = DateTime.UtcNow,
                Company = "Test Company 1",
                Location = "Test Location 1",
                UserId = "Test User ID 1"
            };

            var jobPosting2 = new JobPosting
            {
                Name = "Test 2",
                Description = "Test Description 2",
                CreatedDate = DateTime.UtcNow,
                Company = "Test Company 2",
                Location = "Test Location 2",
                UserId = "Test User ID 2"
            };

            // Add the job postings to the in-memory database asynchronously
            await context.JobPostings.AddRangeAsync(jobPosting1, jobPosting2);
            await context.SaveChangesAsync();

            // Verify that the result is not null and that the count of job postings returned is equal to 2
            var result = await repository.GetAllAsync();
            Assert.NotNull(result);
            Assert.True(result.Count() >= 2);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldReturnJobPosting()
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

            // Add the job posting to the in-memory database asynchronously
            await context.JobPostings.AddAsync(jobPosting);
            await context.SaveChangesAsync();

            // Verify that the job posting was added successfully
            var result = await repository.GetByIdAsync(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal(jobPosting.Name, result.Name);
        }

        [Fact]
        public async Task GetByIdAsync_ShouldThrowKeyNotFoundException()
        {
            // Create Db context instance
            var context = CreateDbContext();
            var repository = new JobPostingRepository(context);

            // Verify that calling GetByIdAsync with a non - existent ID throws a KeyNotFoundException
            await Assert.ThrowsAsync<KeyNotFoundException>(() => repository.GetByIdAsync(999));
        }

        [Fact]
        public async Task UpdateAsync_ShouldUpdateJobPosting()
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

            // Add the job postings to the in-memory database asynchronously
            await context.JobPostings.AddAsync(jobPosting);
            await context.SaveChangesAsync();

            // Update description for testing
            jobPosting.Description = "Update Description";
            await repository.UpdateAsync(jobPosting);

            // Verify that the job posting was updated successfully
            var result = context.JobPostings.Find(jobPosting.Id);

            Assert.NotNull(result);
            Assert.Equal(jobPosting.Description, result.Description);
        }
    }
}
