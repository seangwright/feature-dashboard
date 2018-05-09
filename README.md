# Feature Dashboard

## Requirements
* Requires Visual Studio 2017 ~15.6

	https://blogs.msdn.microsoft.com/webdev/2018/02/27/asp-net-core-2-1-0-preview1-now-available/

* Running on `2.1.0-rc1-final` of ASP.NET Core

* Uses localdb connection for database
	
	https://docs.microsoft.com/en-us/aspnet/core/tutorials/razor-pages/sql

	https://docs.microsoft.com/en-us/sql/database-engine/configure-windows/sql-server-2016-express-localdb

* Uses Azure AD for security

	* Create folder at `%AppData%\Roaming\Microsoft\usersecrets\aspnet-featuredashboard.web-0FF74406-1E1E-494B-9C16-6895231CA071\`

	* Add `secrets.json` with the following body, filling in values appropriate for your application configuration in Azure AD

		{
		  "AzureAd": {
			"Instance": "",
			"Domain": "",
			"TenantId": "",
			"ClientId": "",
			"CallbackPath": ""
		  }
		}