# Flight Information System

This document provides instructions on how to set up the database and run the project.

## 🚀 How to Set Up `FlightsDb`

Follow these steps to create and populate the database using the provided `setup.sql` and `data.sql` scripts.

### 1. Connect to Your SQL Server Instance

*   Launch **SQL Server Management Studio (SSMS)** or any other SQL client.
*   In the "Connect to Server" window, connect to your local or remote SQL Server instance.

### 2. Execute SQL Scripts

> ⚠️ **Important:** Make sure that you don't already have a database named `FlightsDb` before proceeding.

#### Step 2.1: Execute `setup.sql` to create the database structure

This script will create the `FlightsDb` database, the `Flights` table, and all required stored procedures.

1.  Open the `Database/setup.sql` file in SSMS. You can do this by:
    *   **Drag-and-Drop:** Drag the file from your file explorer into the SSMS query window.
    *   **Menu:** Go to `File > Open > File...` (or press `Ctrl+O`) and select the script.

2.  Click the **Execute** button on the toolbar or press `F5`.

3.  After the script finishes, you should see the message:
    ```sql
    Commands completed successfully.
    ```

#### Step 2.2: Execute `data.sql` to populate the database

This script will insert initial test data into the `Flights` table.

1.  Open the `Database/data.sql` file in SSMS using the same method as above.
2.  Click the **Execute** button or press `F5`.
3.  After the script finishes, you should see the message:
    ```sql
    (10 rows affected)
    ```

### 3. Verify the Setup

After completing the steps above, please verify the following in SSMS:

*   ✅ A new database named `FlightsDb` has been created.
*   ✅ Inside `FlightsDb`, there is a table named `dbo.Flights`.
*   ✅ The `dbo.Flights` table contains the initial 10 rows of data.
*   ✅ Under `Programmability > Stored Procedures`, you can see all the created procedures (e.g., `GetFlightByNumber`, `AddFlight`, etc.).