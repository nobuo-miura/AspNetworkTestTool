using Microsoft.AspNetCore.Mvc;
using Yinyang.Utilities.MySql;
using Yinyang.Utilities.Npgsql;
using Yinyang.Utilities.Oracle.Core;
using Yinyang.Utilities.SqlServer;

namespace AspNetworkTestTool.Controllers;

public class DatabaseController : Controller
{
    public IActionResult Index()
    {
        ViewBag.DatabaseResult = "No Connect.";
        return View();
    }


    [HttpPost]
    public ActionResult Index(string connectionString, string database)
    {
        if (!string.IsNullOrEmpty(connectionString) && !string.IsNullOrEmpty(database))
        {
            ViewBag.ConnectionString = connectionString;
            ViewBag.Database = database;

            switch (database)
            {
                case "postgresql":
                    try
                    {
                        var db = new Pgsql(connectionString);
                        db.Open();
                        db.Close();
                        ViewBag.DatabaseResult = "Success.";
                        ViewBag.DatabaseError = "Success.";
                    }
                    catch (Exception err)
                    {
                        ViewBag.DatabaseResult = "Failed.";
                        ViewBag.DatabaseError = err;
                    }

                    return View();
                case "sqlserver":
                    try
                    {
                        var db = new SqlServer(connectionString);
                        db.Open();
                        db.Close();
                        ViewBag.DatabaseResult = "Success.";
                        ViewBag.DatabaseError = "Success.";
                    }
                    catch (Exception err)
                    {
                        ViewBag.DatabaseResult = "Failed.";
                        ViewBag.DatabaseError = err;
                    }

                    return View();
                case "mysql":
                    try
                    {
                        var db = new MySqlConnect(connectionString);
                        db.Open();
                        db.Close();
                        ViewBag.DatabaseResult = "Success.";
                        ViewBag.DatabaseError = "Success.";
                    }
                    catch (Exception err)
                    {
                        ViewBag.DatabaseResult = "Failed.";
                        ViewBag.DatabaseError = err;
                    }
                    return View();
                case "oracle":
                    try
                    {
                        var db = new OracleDatabase(connectionString);
                        db.Open();
                        db.Close();
                        ViewBag.DatabaseResult = "Success.";
                        ViewBag.DatabaseError = "Success.";
                    }
                    catch (Exception err)
                    {
                        ViewBag.DatabaseResult = "Failed.";
                        ViewBag.DatabaseError = err;
                    }
                    return View();
                default:
                    ViewBag.DatabaseResult = "No Connect.";
                    return View();
            }
        }

        ViewBag.DatabaseResult = "No Connect.";
        return View();
    }
}
