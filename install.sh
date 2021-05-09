#!/bin/bash
dotnet new react
dotnet tool install --global dotnet-ef
dotnet add package Microsoft.EntityFrameworkCore --version 5.0.3
dotnet add package Microsoft.EntityFrameworkCore.Design --version 5.0.3
dotnet add package Microsoft.EntityFrameworkCore.SqlServer --version 5.0.3

dotnet add package Microsoft.AspNetCore.Mvc.NewtonsoftJson --version 5.0.3


dotnet ef migrations add Initial -v
dotnet ef database update Initial -v



