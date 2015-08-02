namespace RoushTech.Async.Tests.Tasks
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class FinallyExtension
    {
        [Fact]
        public void ShouldRunAfterCatch()
        {
            bool caught = false, finallyrun = false;
            Task.Factory
                .StartNew(() => { throw new InvalidOperationException("test"); })
                .Catch((exception) =>
                {
                    Assert.Equal("test", exception.Message);
                    caught = true;
                })
                .Finally(() => { finallyrun = true; })
                .Wait();
            Assert.True(caught, "Caught flag false");
            Assert.True(finallyrun, "Finally flag false");
        }

        [Fact]
        public void ShouldRunAfterTask()
        {
            bool finallyrun = false;
            Task.Factory
                .StartNew(() => { })
                .Catch((exception) => { })
                .Finally(() => { finallyrun = true; })
                .Wait();
            Assert.True(finallyrun, "Finally flag false");
        }

        [Fact]
        public void ShouldClearFaulted()
        {
            bool faulted = true;
            Task.Factory
                .StartNew(() => { })
                .Catch((exception) => { })
                .Finally(() => { })
                .Then((t) => { faulted = t.IsFaulted; })
                .Wait();
            Assert.False(faulted, "Faulted flag true");
        }
    }
}
