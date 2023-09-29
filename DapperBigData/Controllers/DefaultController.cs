using Dapper;
using DapperBigData.Models;
using DapperBigData.Models.Dtos;
using Microsoft.AspNetCore.Mvc;
using System.Data.SqlClient;

namespace DapperBigData.Controllers
{
    public class DefaultController : Controller
    {
        private readonly string _connectionString = "Data Source=DESKTOP-DAHMG8F;Initial Catalog=AirportDb;Integrated Security=True;Persist Security Info=False;Pooling=False;MultipleActiveResultSets=False;Encrypt=False;TrustServerCertificate=False;";

        public async Task<IActionResult> Index()
        {
            await using var connection = new SqlConnection(_connectionString);

            var countryMaxCount = (await connection.QueryAsync<CountryDto>("SELECT TOP 1 CountryName, COUNT(*) AS Count FROM dbo.Flights GROUP BY CountryName ORDER BY Count DESC")).FirstOrDefault();
            var countryMinCount = (await connection.QueryAsync<CountryDto>("SELECT TOP 1 CountryName, COUNT(*) AS Count FROM dbo.Flights GROUP BY CountryName ORDER BY Count ASC")).FirstOrDefault();

            var nationalityMaxCount = (await connection.QueryAsync<NationalityDto>("SELECT TOP 1 Nationality, COUNT(*) AS Count FROM dbo.Flights GROUP BY Nationality ORDER BY Count DESC")).FirstOrDefault();
            var nationalityMinCount = (await connection.QueryAsync<NationalityDto>("SELECT TOP 1 Nationality, COUNT(*) AS Count FROM dbo.Flights GROUP BY Nationality ORDER BY Count ASC")).FirstOrDefault();

            var continentsMaxCount = (await connection.QueryAsync<ContinentDto>("SELECT TOP 1 Continents, COUNT(*) AS Count FROM dbo.Flights GROUP BY Continents ORDER BY Count DESC")).FirstOrDefault();
            var continentsMinCount = (await connection.QueryAsync<ContinentDto>("SELECT TOP 1 Continents, COUNT(*) AS Count FROM dbo.Flights GROUP BY Continents ORDER BY Count ASC")).FirstOrDefault();


            ViewData["countryNameMax"] = countryMaxCount.CountryName;
            ViewData["countCountryNameMax"] = countryMaxCount.Count;

            ViewData["countryNameMin"] = countryMinCount.CountryName;
            ViewData["countCountryNameMin"] = countryMinCount.Count;

            ViewData["nationalityNameMax"] = nationalityMaxCount.Nationality;
            ViewData["countNationalityNameMax"] = nationalityMaxCount.Count;

            ViewData["nationalityNameMin"] = nationalityMinCount.Nationality;
            ViewData["countNationalityNameMin"] = nationalityMinCount.Count;

            ViewData["continentsNameMax"] = continentsMaxCount.Continents;
            ViewData["countContinentsNameMax"] = continentsMaxCount.Count;

            ViewData["continentsNameMin"] = continentsMinCount.Continents;
            ViewData["countContinentsNameMin"] = continentsMinCount.Count;

            return View();
        }
    }
}
