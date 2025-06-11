# FlightInformationSystem
How to setup FlightsDb (setup.sql AND data.sql):
1)Connect to Server
    Launch SQL Server Management Studio (SSMS).
    In the "Connect to Server" window, connect to your local or remote SQL Server instance.
2)Opening a SQL script (Make sure that you don't have such a database created.)
    1. Open the .sql file in SSMS. You can do this in one of the following ways:
        a)Method A (Drag-and-Drop): Drag and drop the script file from your file manager (such as Windows Explorer) directly into the SSMS window.
        b)Method B (Menu-based): From the SSMS menu, choose File -> Open -> File... (or press Ctrl+O) and browse to your .sql file.
    2.Click the Execute button on the toolbar or press the F5 key.
After the execution is complete look at the bottom, you must see the messages:
    "Commands completed successfully" (setup.sql) 
    "10 rows affected"(data.sql). 
Make sure that you have created Database, table inside it and stored procedures in programmability folder, added initial data to Flights table.
