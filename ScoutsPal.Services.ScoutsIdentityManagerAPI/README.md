This is a FAQ for our project :)

Bellow we'll have the steps we've covered to build this API:

1- added the following packages:
- Duende.Identity.Server.AspNetIdentity by Duende Software version 5.1.1
- Microsoft.EntityFrameworkCore.SqlServer by Microsoft version 6.11.0
- Microsoft.AspNetCore.Identity.EntityFrameworkCore by Microsoft version 6.11.0
- Microsoft.AspNetCore.Identity.UI by Microsoft version 6.11.0
- Microsoft.EntityFrameworkCore.Tools by Microsoft version 6.11.0

2- Added the folder DbContext
2.1- added the class ApplicationDbContext and inserted this code:
```
public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }
    }
```

3- In the Program.cs file, in the ConfigureServices method, let's add:
```
builder.Services.AddDbContext<ApplicationDbContext>(options => options.UseSqlServer(Configuration.GetConnectionString("DefaultConnection")));
```

4-Then let's go to appsettings.json and let's add the connection string:
```
"ConnectionStrings": {
    "DefaultConnection": "Server=Depends_On_Your_Local_Machine_Instance;Database=ScoutsPalScoutsIdentityServerAPI;Trusted_Connection=True;MultipleActiveResultSets=true"
  }
```

5- Then let's add the folder "Models"
5.1- Then we'll add some classes that we'll need to the folder
5.2- then let's add DbSets for those classes that we created to the ApplicationDbContext.cs file

6- After that, we'll open the package manager console, and let's add a migration:
6.1- in the command line, you'll add:
    - add-migration name_for_the_migration
    - update-database