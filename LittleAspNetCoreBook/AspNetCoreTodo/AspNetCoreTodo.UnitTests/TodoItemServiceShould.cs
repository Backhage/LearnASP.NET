using AspNetCoreTodo.Data;
using AspNetCoreTodo.Models;
using AspNetCoreTodo.Services;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Threading.Tasks;
using Xunit;

namespace AspNetCoreTode.UnitTests
{
    public class TodoItemServiceShould
    {
        [Fact]
        public async Task AddNewItemAsIncompleteWithDueDate()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>().UseInMemoryDatabase(databaseName: "Test_AddNewItem").Options;
            using (var context = new ApplicationDbContext(options))
            {
                var service = new TodoItemService(context);
                var fakeUser = new IdentityUser
                {
                    Id = "fake-000",
                    UserName = "fake@example.com"
                };

                await service.AddItemAsync(new TodoItem
                {
                    Title = "Testing?"
                }, fakeUser);
            }

            using (var context = new ApplicationDbContext(options))
            {
                var itemsInDatabase = await context.Items.CountAsync();
                Assert.Equal(1, itemsInDatabase);
                var item = await context.Items.FirstAsync();
                Assert.Equal("Testing?", item.Title);
                Assert.False(item.IsDone);

                var difference = DateTimeOffset.Now.AddDays(3) - item.DueAt;
                Assert.True(difference < TimeSpan.FromSeconds(10)); 
            }
        }
    }
}
