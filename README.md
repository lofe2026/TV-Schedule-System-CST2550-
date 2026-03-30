TV Program Scheduling System

Overview
The TV Program Scheduling System is a Windows Forms application developed using C# .NET. The purpose of the system is to manage and display TV program schedules efficiently. It allows users to add, remove, and view schedules while ensuring that no time conflicts occur within the same channel.

The system includes multiple user roles:

* Admin
* Manager
* Client
* Viewer (no login required, limited session access)



Features

Authentication
The system includes a role-based login mechanism. Admin and Manager accounts use predefined credentials, while Client users can register their own accounts. Viewer access does not require login but is restricted by a session timeout.

Schedule Management
Users can add schedules by specifying:

* Channel ID
* Program ID
* Start time
* Duration
* Image preview

The system prevents overlapping schedules on the same channel. Schedules can also be removed.

Search Functionality
The system supports efficient search operations, including:

* Retrieving schedules by channel
* Retrieving a specific schedule using channel and time

Viewer Mode
The Viewer role provides a read-only interface where users can view schedules without making changes. A session timeout of 30 minutes is implemented to automatically return the user to the login screen.

User Interface
The application uses Windows Forms and includes:

* A DataGridView to display schedules
* A preview section for program images
* A digital clock
* Navigation buttons to move between forms



Data Structure

A custom hash table with separate chaining is implemented to store schedules.

The key used in the hash table is a combination of Channel ID and Start Time.

The hash table was selected because it provides efficient access to data and improves the performance of search operations compared to linear structures.

Time Complexity:

* Insert: O(1) average
* Search: O(1) average
* Delete: O(1) average
* Worst case: O(n)



Database

The system uses SQL Server LocalDB to store data.

The main tables are:

* Users
* Schedule

A SQL script file (database.sql) is included to create and populate the database.



Testing

Unit testing is implemented using MSTest.

The following functionalities are tested:

* Adding schedules
* Detecting scheduling conflicts
* Removing schedules
* Searching for schedules
* Retrieving schedules by channel
* Handling invalid inputs and edge cases



How to Run

1. Open the solution in Visual Studio
2. Ensure SQL Server LocalDB is installed
3. Execute the database.sql script to create the database
4. Build the solution
5. Run the application



Default Access

Admin
Username: admin, 
Password: admin123

Manager
Username: manager, 
Password: manager123

Viewer
Accessible through the Viewer option without login
Session automatically expires after 30 minutes



Project Structure

Models: Contains data models such as Schedule, User, Channel, and TVProgram
DataStructures: Contains the custom hash table implementation
Services: Contains business logic
Repositories: Handles database operations
Forms: Contains all user interface forms
Tests: Contains MSTest unit tests


Notes

The system does not use any third-party libraries. All data structures and algorithms are implemented manually. The application follows a layered structure where the user interface communicates with the service layer, which interacts with the data structure and database.


Conclusion

The system demonstrates the use of custom data structures, efficient search algorithms, database integration, and unit testing. It provides a functional scheduling application with role-based access and a user-friendly interface.
