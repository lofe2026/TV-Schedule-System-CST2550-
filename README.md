# TV Program Scheduling System

## Overview

The TV Program Scheduling System is a Windows Forms application developed using C# .NET. It is designed to manage and display TV program schedules efficiently. The system allows users to add, remove, and view schedules while ensuring that no time conflicts occur within the same channel.

The system includes multiple user roles:

* Admin
* Manager
* Client
* Viewer (no login required, limited session access)

## Features

### Authentication

The system includes a role-based login mechanism. Admin and Manager accounts use predefined credentials, while Client users can register their own accounts. Viewer access does not require login but is restricted by a session timeout.

### Schedule Management

Users can add schedules by specifying:

* Channel ID
* Program ID
* Start time
* Duration
* Image preview

The system prevents overlapping schedules on the same channel. Schedules can also be removed.

### Search Functionality

The system supports efficient search operations, including:

* Retrieving schedules by channel
* Retrieving a specific schedule using channel and time

### AI Assistant

The system includes an AI assistant integrated into the Admin interface to support scheduling decisions. The AI is available through a chat panel in the main scheduling form.

The AI assistant provides:

* Conflict explanation
* Next available slot suggestion
* Channel recommendation
* Natural language interaction based on current schedule data

The AI is implemented through an `AIService` class. It combines the user’s message with real-time scheduling data, such as selected channel, selected start time, duration, and existing schedules, before sending the request to the AI model. The response is then displayed in the chat panel inside the application.

The AI assistant is designed as a decision-support tool only. It helps the user understand scheduling options, but it does not automatically add, remove, or change schedules.

The Manager form does not include the AI chat panel. It provides the same scheduling functions as the Admin side, except for the AI feature.

### Viewer Mode

The Viewer role provides a read-only interface where users can view schedules without making changes. A session timeout of 30 minutes is implemented to automatically return the user to the login screen.

### User Interface

The application uses Windows Forms and includes:

* A DataGridView to display schedules
* A preview section for program images
* A digital clock
* Navigation buttons between forms
* A chat-based AI panel for scheduling assistance

## Data Structure

A custom hash table with separate chaining is implemented to store schedules.

The key is a combination of Channel ID and Start Time.

This structure was chosen because it provides efficient access and improves performance compared to linear structures.

### Time Complexity

* Insert: O(1) average
* Search: O(1) average
* Delete: O(1) average
* Worst case: O(n)

## Database

The system uses SQL Server LocalDB to store data.

Main tables:

* Users
* Schedule

A SQL script file (database.sql) is included to create and populate the database.

## Testing

Unit testing is implemented using MSTest.

Tested functionalities include:

* Adding schedules
* Detecting scheduling conflicts
* Removing schedules
* Searching schedules
* Retrieving schedules by channel
* Handling invalid inputs and edge cases

## How to Run

1. Open the solution in Visual Studio
2. Ensure SQL Server LocalDB is installed
3. Execute the database.sql script
4. Build the solution
5. Run the application

## Default Access

Admin
Username: admin
Password: admin123

Manager
Username: manager
Password: manager123

Viewer
Accessible without login (30-minute session limit)

## Project Structure

* Models – Data models such as Schedule, User, Channel, and Program
* DataStructures – Custom hash table implementation
* Services – Business logic and AI service
* Repositories – Database operations
* Forms – User interface
* Tests – MSTest unit tests

## Notes

The system does not use any third-party libraries. All data structures and algorithms are implemented manually. The application follows a layered architecture where the user interface communicates with the service layer, which interacts with the data structure and database.

The AI assistant enhances usability by providing intelligent scheduling support while keeping the core scheduling logic unchanged.

## Conclusion

The system demonstrates the use of custom data structures, efficient scheduling algorithms, database integration, and unit testing. The addition of an AI assistant improves user interaction and supports better scheduling decisions, making the application more practical and user friendly.
