using System;
using System.IO;
using System.Linq;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using PeterKApplication.Shared.Data;
using PeterKApplication.Shared.Models;
using Xamarin.Forms;

namespace PeterKApplication.Data
{
    public class LocalDbContext : AppDbContext
    {
        private const string databaseName = "database.db";

        public LocalDbContext()
        {
        }

        public LocalDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            // Specify that we will use sqlite and the path of the database here
            optionsBuilder.UseSqlite($"Filename={GetDbPath()}");
        }

        public static string GetDbPath()
        {
            String databasePath = "";
            switch (Device.RuntimePlatform)
            {
                case Device.iOS:
                    SQLitePCL.Batteries_V2.Init();
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.MyDocuments), "..",
                        "Library", databaseName);
                    break;
                case Device.Android:
                    databasePath = Path.Combine(Environment.GetFolderPath(Environment.SpecialFolder.Personal),
                        databaseName);
                    break;
                default:
                    throw new NotImplementedException("Platform not supported");
            }

            System.Diagnostics.Debug.Write("databasePath : " + databasePath);

            return databasePath;
        }
    }
}