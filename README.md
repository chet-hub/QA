# QA
A question and answer website

# Install packages

```shell
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore --version 5.0.3
dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.3
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.3
dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 5.0.3
```

# Init database

```
dotnet ef database update Initial -v
```
