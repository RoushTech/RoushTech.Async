namespace RoushTech.Async.Tests.Tasks
{
    using System.Threading.Tasks;
    using Xunit;

    public class ThenExtension
    {

        [Fact]
        public void ShouldChainMethods()
        {
            var i = 0;
            Task.Factory
                .StartNew(() => { i++; })
                .Then((t) => { i++; })
                .Wait();
            Assert.Equal(2, i);
        }

        [Fact]
        public void ThrownExceptionShouldSkipThen()
        {
            var executed = false;
            Task.Factory
                .StartNew(() => { throw new System.Exception("test"); })
                .Then((t) => { executed = true; })
                .Catch((t) => { /* Clearing IsFailed flag */ })
                .Wait();
            Assert.False(executed, "Executed flag true");
        }
    }
}
