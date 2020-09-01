using System.Linq;
using AutoFixture;

namespace SakhaTyla.Core.UnitTests.Requests.Roles
{
    public static class FixtureGlobal
    {
        public static Fixture Get()
        {
            var fixture = new Fixture();
            fixture.Behaviors.OfType<ThrowingRecursionBehavior>()
                .ToList().ForEach(f => fixture.Behaviors.Remove(f));
            fixture.Behaviors.Add(new NullRecursionBehavior());
            return fixture;
        }
    }
}