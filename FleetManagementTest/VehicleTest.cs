using FleetManagementAPI.Models;
using FleetManagementAPI.Models.Custom;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Net;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;
using Xunit;
using Xunit.Extensions.Ordering;

namespace FleetManagementTest
{
    [Order(2)]
    public class VehicleTest
    {
        private static Vehicle standardVehicle = new Vehicle()
        {
            Type = "Car",
            NumberOfPassengers = Convert.ToByte(DefaultNumberOfPassengers.CAR),
            Color = "Black"
        };

        private static Chassis standardChassis = new Chassis()
        {
            Series = "1234TEST",
            Number = 134
        };

        [Fact]
        public async Task Should_get_all_vehicles()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.GetAsync("/api/vehicles");

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

                standardVehicle.ChassisId = standardChassis.Id;

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        [Fact, Order(2)]
        public async Task Should_save_a_vehicle()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/vehicles", new StringContent(
                    JsonConvert.SerializeObject(standardVehicle)
                    , Encoding.UTF8, "application/json"));

                string responseVehicle = await response.Content.ReadAsStringAsync();

                standardVehicle = JsonConvert.DeserializeObject<Vehicle>(responseVehicle);

                Assert.Equal(HttpStatusCode.Created, response.StatusCode);
            }
        }

        [Fact, Order(3)]
        public async Task Should_error_same_vehicle()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.PostAsync("/api/vehicles", new StringContent(
                    JsonConvert.SerializeObject(standardVehicle)
                    , Encoding.UTF8, "application/json"));

                Assert.Equal(HttpStatusCode.BadRequest, response.StatusCode);
            }
        }

        [Fact, Order(4)]
        public async Task Should_delete_a_vehicle()
        {
            using (var client = new TestClientProvider().Client)
            {
                var response = await client.DeleteAsync("/api/vehicles/" + standardVehicle.Id);

                Assert.Equal(HttpStatusCode.OK, response.StatusCode);
            }
        }

        [Fact, Order(5)]
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
