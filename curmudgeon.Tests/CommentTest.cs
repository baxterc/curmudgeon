using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Xunit;
using curmudgeon.Models;

namespace curmudgeon.Tests
{
    public class CommentTest
    {
        [Fact]
        public void CreateCommentTest()
        {
            var comment = new Comment();
            comment.Content = "This is a comment";
            var result = comment.Content;
            Assert.Equal("This is a comment", result);
        }
    }
}
