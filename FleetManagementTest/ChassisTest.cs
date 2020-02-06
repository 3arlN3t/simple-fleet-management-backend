using FleetManagementAPI.Models;
using Newtonsoft.Json;
using System;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FleetManagementTest
{
    [Order(1)]
    public class ChassisTest
    {
        private static Chassis standardChassis = new Chassis()
        {
            Series = "1234TEST",
            Number = 134
        };

        [Fact]
        public async Task Should_get_all_chasseez()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/chassis");

                response.EnsureSuccessStatusCode();

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact, Order(1)]
        public async Task Should_save_a_chassis()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/chassis", new StringContent(
                    JsonConvert.SerializeObject(standardChassis)
                    , Encoding.UTF8, "application/json"));

                string responseChassis = await response.Content.ReadAsStringAsync();

                standardChassis = JsonConvert.DeserializeObject<Chassis>(responseChassis);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);                
            }
        }

        [Fact, Order(2)]
        public async Task Should_error_same_chassis()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/chassis", new StringContent(
                    JsonConvert.SerializeObject(standardChassis)
                    , Encoding.UTF8, "application/json"));                

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact, Order(3)]
        public async Task Should_delete_a_chassis()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/chassis/" + standardChassis.Id);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }
    }
}
