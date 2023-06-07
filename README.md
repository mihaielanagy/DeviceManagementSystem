# How it works
- An SQL server database will be created and populated using the 2 scripts
- The project DeviceManagementDB is used to connect to the SQL Server database
- Entity framework is used to connect to the database, using database first approach
- DeviceManagementWeb uses DeviceManagementDB to communicate to the database
- DeviceManagementWeb contains a set of controllers that exposes the database interactions
- In order to avoid returning the user password from the database in the GetAll / GetById methods, we have a DTO that maps the information
- Validations in the controllers contains basic validation, since mandatory fields are automatically validated before the code accesses the controller methods. If the fields are not populated, the request will fail.
- There are validations for the email format and password length


# How to set up the solution
- Open SQL Server Management and connect to a database server
- Run the CreateDB_Tables.sql script to create the database and the tables
- Run the Populate_Tables.sql script to populate the tables with dummy data
- Open the DeviceManagementContext.cs class located in DeviceManagementDB project and change the existing connection string with your connection string, should be similar to:
Server=.\\SQLExpress;Database=DeviceManagement;Trusted_Connection=true;TrustServerCertificate=True
- Rebuild and start the DeviceManagementWeb project
- Validate that the database connection works by using swagger to get data, like the data for Cities
