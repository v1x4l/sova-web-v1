using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Moq;
using Xunit;
using DatabaseService;
using WebApi.Controllers;
using WebApi.JsonModels;
using DomainModel;
using Newtonsoft.Json;
using Microsoft.EntityFrameworkCore;

namespace Tests
{
    public class DataServiceTests
    {
        /*
        [Fact]
        public void Add_writes_to_database()
        {
            DateTime now = DateTime.Now;
            var sovaUser = new SovaUser { SovaUserId = 666, SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "Add_sovausers_to_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new SovaContext(options))
            {
                var service = new SovaUserDataService();
                service.Add(sovaUser);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new SovaContext(options))
            {
                Assert.Equal(1, context.SovaUsers.Count());
                Assert.Equal(sovaUser, context.SovaUsers.Single());
            }
        }
        */
    }
    
}
