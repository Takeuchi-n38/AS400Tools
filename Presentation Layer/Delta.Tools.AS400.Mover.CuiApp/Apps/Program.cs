using Delta.AS400.Libraries;
using Delta.Honsha01;
using Delta.Tools.AS400.Configs;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Hosting;

namespace Delta.Tools.AS400.Mover.Apps
{
    //Install-Package Microsoft.EntityFrameworkCore.Tools
    //Scaffold-DbContext "Host=192.168.130.96;Database=koubai01;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "salelib
    //Scaffold-DbContext "Host=192.168.130.96;Database=koubai01;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "cds"
    //Scaffold-DbContext "Host=192.168.130.96;Database=koubai01;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "eigylib"

    //Scaffold-DbContext "Host=192.168.130.96;Database=honsha01;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "lfblib"
    //Scaffold-DbContext "Host=192.168.130.96;Database=honsha01;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "oplib"
    //Scaffold-DbContext "Host=192.168.130.96;Database=honsha01;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "prodlib"
    //Scaffold-DbContext "Host=192.168.130.96;Database=honsha01;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "seatlib"
    //Scaffold-DbContext "Host=192.168.130.96;Database=h1jj;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "prodlib"
    //Scaffold-DbContext "Host=192.168.130.96;Database=h1jj;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "f__lib"
    //Scaffold-DbContext "Host=192.168.130.96;Database=h1qkc;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "prodlib"
    //Scaffold-DbContext "Host=192.168.130.96;Database=h1iid;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "iidlib"

    //Scaffold-DbContext "Host=localhost;Port=5432;Database=h1iid;Username=postgres;Password=postgres" Npgsql.EntityFrameworkCore.PostgreSQL -Force -Schemas "iidlib"

    class Program
    {

        //static IToolConfig ToolConfig = Honsha01Jj_libConfig.Of();
        static IToolConfig ToolConfig = null;//Koubai01BaalibConfig.Of();

        static Library TargetLibrary = Honsha01LibraryList.Iiflib;

        public static void Main(string[] args)
        {
            CreateHostBuilder(args).Build().Run();
        }

        public static IHostBuilder CreateHostBuilder(string[] args) =>
            Host.CreateDefaultBuilder(args)
                .ConfigureServices((hostContext, services) =>
                {
                    services.AddHostedService(serviceProvider => new Mover(ToolConfig,TargetLibrary));
                });

    }
}
