
using Microsoft.AspNetCore.Identity;
using Workout_API.DBContexts;
using Workout_API.Models;

namespace Workout_API_Test_Suite.UnitTests.ModelTests
{
    public class StorageTests
    {
        DBContext DBContext { get; set; }

        public StorageTests(DBContext _context)
        {
            DBContext = _context;
        }

        [Fact]
        public void CreateStorageObject()
        {
            UserStore store = new UserStore(DBContext);
            Assert.NotNull(store);
        }
    }
}
