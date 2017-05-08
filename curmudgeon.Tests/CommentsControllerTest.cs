using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using curmudgeon.Models;
using curmudgeon.Controllers;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.AspNetCore.Builder;
using Microsoft.AspNetCore.Http;

namespace curmudgeon.Tests
{
    public class CommentsControllerTest
    {
        Mock<ICurmudgeonRepository> mock = new Mock<ICurmudgeonRepository>();
        TestDbContext db = new TestDbContext();
        private UserManager<ApplicationUser> userManager { get; set; }
        private SignInManager<ApplicationUser> signInManager { get; set; }
        private ApplicationUser testUser { get; set; }
        private Post testPost { get; set; }
        private HttpContext httpContext { get; set; }
        private ISession session { get; set; }

        /*
        private void DbSetup()
        {
            var services = new ServiceCollection();
            
            services.AddEntityFramework()
                .AddDbContext<TestDbContext>();

            services.AddIdentity<ApplicationUser, IdentityRole>()
                .AddEntityFrameworkStores<TestDbContext>()
                .AddDefaultTokenProviders();

            var serviceProvider = services.BuildServiceProvider();

            userManager = serviceProvider.GetRequiredService<UserManager<ApplicationUser>>();
            signInManager = serviceProvider.GetRequiredService<SignInManager<ApplicationUser>>();

            testPost = db.Posts.FirstOrDefault();
            if (testPost == null)
            {
                testPost = new Post("test", "test content", DateTime.Today);
                db.Posts.Add(testPost);
                db.SaveChanges();
            }
            testUser = db.Users.FirstOrDefault();
            if (testUser == null)
            {
                ApplicationUser testUser = new ApplicationUser { UserName = "test", Email = "test@test.com", Nickname = "test" };
                userManager.CreateAsync(testUser, "Test1234!");
            }
        }
        
        [Fact]
        public void Index_IsAViewResult()
        {
            
            CommentsController controller = new CommentsController(userManager, signInManager, db);
            var result = controller.Index();
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void Write_IsAViewResult()
        {
            DbSetup();
            CommentsController controller = new CommentsController(userManager, signInManager, db);
            var result = controller.Write(1);
            Assert.IsType<ViewResult>(result);
        }

        [Fact]
        public void CommentControllerWithRepoParam_IsAController()
        {
            CommentsController controller = new CommentsController(userManager, signInManager, db);

            Assert.IsType<CommentsController>(controller);
        }

        [Fact]
        public async void Write_AddsComment()
        {
            DbSetup();
            CommentsController controller = new CommentsController(userManager, signInManager, db);
            Comment testComment = new Comment();
            testComment.Content = "Test";
            testComment.Title = "Test Title";
            testComment.CommentId = 1;
            testComment.User = testUser;
            testComment.Post = testPost;
            var controllerResult = controller.WriteComment(testComment);
            var result = await controller.Index();
            //Assert.Contains(testComment, collection);
        }
        */
    }
}
