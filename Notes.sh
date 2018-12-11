#Download SQLSERVER
docker pull microsoft/mssql-server-linux
#RUN SQLSERVER
docker run -e 'ACCEPT_EULA=Y' -e 'MSSQL_SA_PASSWORD=CitSaPw!' -p 1433:1433 --name sql1 -d microsoft/mssql-server-linux

#EXEC CONTAINER
sudo docker exec -it sql1 /bin/bash

#DOCKER STATUS
docker ps -a 

#BACKUP DATABASE
sqlcmd -S localhost -U SA -P CitSaPw! -Q "BACKUP DATABASE [master] TO DISK = N'/var/opt/mssql/data/JeremiSwannSpyStore.bak' WITH NOFORMAT, NOINIT, NAME = 'demodb-full', SKIP, NOREWIND, NOUNLOAD, STATS = 10"

#RESTORE DATABASE
sqlcmd -S localhost -U SA -P CitSaPw! -Q "RESTORE DATABASE [master] FROM DISK = N'/var/opt/mssql/data/JeremiSwannSpyStore.bak' WITH FILE = 1, NOUNLOAD, REPLACE, NORECOVERY, STATS = 5"

#CREATE EF MIGRATION
dotnet ef migrations add Initial -o EF/Migrations -c SMP.DAL.EF.Context
dotnet ef migrations add Rss -o EF/Migrations -c SMP.DAL.EF.Context
dotnet ef migrations add Message -o EF/Migrations -c SMP.DAL.EF.Context
dotnet ef migrations add Bio -o EF/Migrations -c SMP.DAL.EF.Context
dotnet ef migrations add Picture -o EF/Migrations -c SMP.DAL.EF.Context
dotnet ef migrations add Bio2 -o EF/Migrations -c SMP.DAL.EF.Context

#APPLY EF MIGRATION
dotnet ef database update Initial -c SMP.DAL.EF.Context
dotnet ef database update Rss -c SMP.DAL.EF.Context
dotnet ef database update Message -c SMP.DAL.EF.Context
dotnet ef database update Bio -c SMP.DAL.EF.Context
dotnet ef database update Picture -c SMP.DAL.EF.Context
dotnet ef database update Bio2 -c SMP.DAL.EF.Context

dotnet ef database update 0
dotnet ef migrations remove


#ERRORS
#A connection was successfully established with the server, but then an error occurred during the pre-login handshake. (provider: TCP Provider, error: 35 - An internal exception was caught)
brew update
brew install openssl
echo 'export PATH="/usr/local/opt/openssl/bin:$PATH"' >> ~/.bash_profile
source ~/.bash_profile


<environmentVariables>
    <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Development" />
    <environmentVariable name="CONFIG_DIR" value="f:\application_config" />
  </environmentVariables>


docker publishes the file
docker publish
docker compose might not work so trouble shoot
environment = development

docker compose file has alias in network, network settings "wvupsmapi"


notepad c:\windows\system32\drivers\etc\hosts


.UseWebListener( options =>
                {
                    options.ListenerSettings.Authentication.Schemes =
                        AuthenticationSchemes.Negotiate | AuthenticationSchemes.NTLM;
                    options.ListenerSettings.Authentication.AllowAnonymous = true;

                })

<aspNetCore processPath="dotnet" arguments=".\SMP.MVC.dll" stdoutLogEnabled="false" stdoutLogFile=".\logs\stdout">
      <environmentVariables>
        <environmentVariable name="ASPNETCORE_ENVIRONMENT" value="Development"/>
        <environmentVariable name="CONFIG_DIR" value="C:\inetpub\wwwroot"/>
      </environmentVariables>
    </aspNetCore>


app.Run(async (context) =>
            {
                try
                {
                    var user = (WindowsIdentity)context.User.Identity;
                    await context.Response
                            .WriteAsync($"User: {user.Name}\tState: {user.ImpersonationLevel}\n");
                    WindowsIdentity.RunImpersonated(user.AccessToken, () =>
                    {
                        var impersonatedUser = WindowsIdentity.GetCurrent();
                        var message =
                            $"User: {impersonatedUser.Name}\tState: {impersonatedUser.ImpersonationLevel}";

                        var bytes = Encoding.UTF8.GetBytes(message);
                        context.Response.Body.Write(bytes, 0, bytes.Length);
                    });
                }
                catch (Exception e)
                {
                    await context.Response.WriteAsync(e.ToString());
                }
            });

USE master;
GO
GRANT SELECT ON dbo.AspNetUsers TO jeremi;
GO