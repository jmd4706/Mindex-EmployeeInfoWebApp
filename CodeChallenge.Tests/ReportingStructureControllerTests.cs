
using System.Diagnostics;
using System.Net;
using System.Net.Http;
using System.Text;

using CodeChallenge.Models;

using CodeCodeChallenge.Tests.Integration.Extensions;
using CodeCodeChallenge.Tests.Integration.Helpers;

using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace CodeCodeChallenge.Tests.Integration
{
    [TestClass]
    public class ReportingStructureControllerTests
    {
        private static HttpClient _httpClient;
        private static TestServer _testServer;

        [ClassInitialize]
        // Attribute ClassInitialize requires this signature
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Style", "IDE0060:Remove unused parameter", Justification = "<Pending>")]
        public static void InitializeClass(TestContext context)
        {
            _testServer = new TestServer();
            _httpClient = _testServer.NewClient();
        }

        [ClassCleanup]
        public static void CleanUpTest()
        {
            _httpClient.Dispose();
            _testServer.Dispose();
        }

        [TestMethod]
        public void CreateReportingStructure_w_DirectReports_Returns_Created()
        {
            // Arrange employee
            var employeeId = "16a596ae-edd3-4847-99fe-c4518e82c86f";
            var expectedFirstName = "John";
            var expectedLastName = "Lennon";

            // Execute get employee
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}");
            var response = getRequestTask.Result;

            // Assert employee exists
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<Employee>();
            Assert.AreEqual(expectedFirstName, employee.FirstName);
            Assert.AreEqual(expectedLastName, employee.LastName);
            
            // Arrange reportingStructure
            var expectedNumOfReports = 4;
            
            // Execute create reportingStructure
            getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            response = getRequestTask.Result;
            
            // Assert reportingStructure is created and is correct
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(employee.EmployeeId, reportingStructure.Employee.EmployeeId);
            Assert.AreEqual(expectedNumOfReports, reportingStructure.NumberOfReports);
        }
        
        [TestMethod]
        public void CreateReportingStructure_w_o_DirectReports_Returns_Created()
        {
            // Arrange employee
            var employeeId = "b7839309-3348-463b-a7e3-5de1c168beb3";
            var expectedFirstName = "Paul";
            var expectedLastName = "McCartney";

            // Execute get employee
            var getRequestTask = _httpClient.GetAsync($"api/employee/{employeeId}");
            var response = getRequestTask.Result;

            // Assert employee exists
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var employee = response.DeserializeContent<Employee>();
            Assert.AreEqual(expectedFirstName, employee.FirstName);
            Assert.AreEqual(expectedLastName, employee.LastName);
            
            // Arrange reportingStructure
            var expectedNumOfReports = 0;
            
            // Execute create reportingStructure
            getRequestTask = _httpClient.GetAsync($"api/reportingStructure/{employeeId}");
            response = getRequestTask.Result;
            
            // Assert reportingStructure is created and is correct
            Assert.AreEqual(HttpStatusCode.OK, response.StatusCode);
            var reportingStructure = response.DeserializeContent<ReportingStructure>();
            Assert.AreEqual(employee.EmployeeId, reportingStructure.Employee.EmployeeId);
            Assert.AreEqual(expectedNumOfReports, reportingStructure.NumberOfReports);
        }
    }
}
