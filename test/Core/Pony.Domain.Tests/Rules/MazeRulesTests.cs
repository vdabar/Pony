using Pony.Domain.Mazes.Rules;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Xunit;

namespace Pony.Domain.Tests.Rules
{
    public class MazeRulesTests
    {
        [Fact]
        public void Should_return_true_if_pony_name_exists()
        {
            var ur = new MazeRules();
            var result = ur.IsPonyNameValidAsync("Scootaloo");
            Assert.Equal(true, result);
        }

        [Fact]
        public void Should_return_false_if_pony_name_doesnt_exists()
        {
            var ur = new MazeRules();
            var result = ur.IsPonyNameValidAsync("notexistingponyname");
            Assert.Equal(false, result);
        }
    }
}