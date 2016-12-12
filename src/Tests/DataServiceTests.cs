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
        [Fact]
        public void Add_sovausers_to_database()
        {
            DateTime now = DateTime.Now;
            var sovaUser = new SovaUser {SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "Add_sovausers_to_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new SovaContext(options))
            {
                var service = new SovaUserDataService(context);
                service.Add(sovaUser);
                service.Add(sovaUser);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new SovaContext(options))
            {
                Assert.Equal(2, context.SovaUsers.Count());
                Assert.Equal(1, context.SovaUsers.First().SovaUserId);
                Assert.Equal(2, context.SovaUsers.Last().SovaUserId);
            }
        }

        
        [Fact]
        public void Remove_sovausers_from_database()
        {
            DateTime now = DateTime.Now;
            var sovaUser = new SovaUser { SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "Remove_sovausers_to_database")
                .Options;

            // Run the test against one instance of the context
            using (var context = new SovaContext(options))
            {
                var service = new SovaUserDataService(context);
                service.Add(sovaUser);
                service.Add(sovaUser);
                service.Delete(1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new SovaContext(options))
            {
                Assert.Equal(2, context.SovaUsers.Single().SovaUserId);
                Assert.Equal(1, context.SovaUsers.Count()); 
            }
        }

        [Fact]
        public void Update_sovausers() {
            DateTime now = DateTime.Now;
            var sovaUser = new SovaUser { SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "update_sova")
                .Options;

            using (var context = new SovaContext(options))
            {
                var service = new SovaUserDataService(context);
                service.Add(sovaUser);
                now = DateTime.Now;
                sovaUser.SovaUserCreationDate = DateTime.Now;
                
                service.Update(sovaUser);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new SovaContext(options))
            {
                Assert.Equal(now, context.SovaUsers.Single().SovaUserCreationDate);
            }
        }

    }
    
}
