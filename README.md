# CrudMvcNoEF

A simple .Net Crud App not using the entity framework. 
You can create, edit and delete products on your local database.
To run this project you need to:

1- Create a database on your local machine (Using SQL Server)

2- Create table called "Product" whith these fields : ProductId, ProductName, Price & Count.

3- Create a Connection.config file into your project folder. You would need to define the connection string to your local database.
That would be somthing like this:

```xml
<?xml version="1.0" encoding="utf-8"?>
<connectionStrings>
    <add name="connectionString"
        providerName="System.Data.ProviderName"
        connectionString="Data Source= YOUR LOCAL MACHINE;Initial Catalog="DB NAME" Integrated Security=true;" />
</connectionStrings>
```

4-Reference the connection.config in your web.config file. That should look like this: 

```xml
<configuration> 
  <connectionStrings configSource="Connection.config"/> 
</configuration>
```

That's all. Ready to go!!
