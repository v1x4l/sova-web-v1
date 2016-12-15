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
        public void AddSovaUsersToDatabase()
        {
            DateTime now = DateTime.Now;
            var sovaUser = new SovaUser {SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "AddSovausersToDatabase")
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
                .UseInMemoryDatabase(databaseName: "RemoveSovausersFromDatabase")
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
        public void UpdateSovausers() {
            DateTime now = DateTime.Now;
            var sovaUser = new SovaUser { SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "UpdateSovausers")
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

        [Fact]
        public void GetSingleSovauser()
        {
            DateTime now = DateTime.Now;
            SovaUser retrievedSovaUser;
            SovaUser sovaUser = new SovaUser { SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "GetSingleSovauser")
                .Options;

            using (var context = new SovaContext(options))
            {
                var service = new SovaUserDataService(context);
                service.Add(sovaUser);
                retrievedSovaUser = service.Get(1);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new SovaContext(options))
            {
                Assert.NotNull(retrievedSovaUser);
                Assert.Equal(sovaUser.SovaUserCreationDate, context.SovaUsers.Single().SovaUserCreationDate);
                
            }
        }

        [Fact]
        public void GetListOfSovaUsers()
        {
            DateTime now = DateTime.Now;
            int AmountOfRetrievedSovaUsers = 5;
            IList<SovaUser> retrievedSovaUser;
            SovaUser sovaUser = new SovaUser { SovaUserCreationDate = now };
            var options = new DbContextOptionsBuilder<SovaContext>()
                .UseInMemoryDatabase(databaseName: "GetListOfSovausers")
                .Options;

            using (var context = new SovaContext(options))
            {
                var service = new SovaUserDataService(context);
                service.Add(sovaUser);
                now = DateTime.Now;
                service.Add(sovaUser);
                now = DateTime.Now;
                service.Add(sovaUser);
                now = DateTime.Now;
                service.Add(sovaUser);
                now = DateTime.Now;
                service.Add(sovaUser);
                retrievedSovaUser = service.GetList(0, AmountOfRetrievedSovaUsers);
            }

            // Use a separate instance of the context to verify correct data was saved to database
            using (var context = new SovaContext(options))
            {
                Assert.NotNull(retrievedSovaUser);
                Assert.NotEmpty(retrievedSovaUser);
                Assert.IsType<List<SovaUser>>(retrievedSovaUser);
                Assert.Equal(AmountOfRetrievedSovaUsers, retrievedSovaUser.Count());
            }
        }
        
        [Fact]
        public void getListFromProcedure() {
            IList<SearchResult> searchResults;

            using (var context = new SovaContext())
            {
                var service = new SearchResultDataService(context);
                searchResults = service.GetProcedureList(0, 10, "Java", "java", "java", true);
            }

            using (var context = new SovaContext())
            {
                Assert.NotNull(searchResults);
                Assert.NotEmpty(searchResults);
                Assert.IsType<List<SearchResult>>(searchResults);
                Assert.Equal(489936, searchResults.First().PostId);
                Assert.Equal(14633709, searchResults.Last().PostId);
            }
            
          }
          
      
    }
}
