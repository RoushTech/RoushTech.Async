namespace RoushTech.Async.Tests.Tasks
{
    using System;
    using System.Threading.Tasks;
    using Xunit;

    public class FinallyExtension
    {
        [Fact]
        public void CatchFinallyWithException()
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
    }
}
