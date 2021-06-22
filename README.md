## IssueList              :detective: :bug:

The IssueList project aims on providing a simple example of a web application using websockets to refresh client data.

* Frontend application is developed using Vue.js 
* Backend application is developed using .Net MVC (C#).
* Database for this application is SQL Server 2019. Has a initial EF migration on backend to generate database


### Needed
* **Visual Studio** to compile backend with .NET Framework 4.7
* **Node.js** (for npm package manager)
* **Sql server**(or sql express)

### Development technology

* **Backend:** .Net MVC with Repository + UoW +  DTO + WebApi + OWIN + JWT + swagger
* **Data storage:** SQL Server + EF migrations
* **Frontend:** Vue.js 

## Getting Started 

* Download this code :shrug:

### Backend: 

* Open IssueListMVC.sln with Visual studio

* Restore Nuget packages if needed.
```sh
Update-Package -reinstall
```

* Change IssueListMVC project connection string in web.config to your Sql server (DefaultConnection)
```sh
<connectionStrings>
    <add name="DefaultConnection" connectionString="Server=ANGBAND;Database=IssueList_DB;Integrated Security=True;" providerName="System.Data.SqlClient" />
  </connectionStrings>
```

* Use "update-database" on package management console  (select project IssueList.Domain)
```sh
update-database
```

* Run project ***IssueListMVC*** from visual studio

* Now you can access to WebApi endpoint in ***/swagger*** path

### Frontend:
* Enter in directory issuelist_client from console
```sh
cd IssueListMVC\issuelist_client
```

* Install all dependencies with npm
```sh
npm install
```

* Start client
```sh
npm run serve
```

* Login with credential 
```sh
username=user 
password=pass
```
