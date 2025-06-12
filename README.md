# 🚀 How to Run the Project Using Docker

This guide explains how to run the entire Flight Information System (database, backend, and frontend) using Docker Compose.

### ✅ Prerequisites
*   Before you begin, make sure you have **[Docker Desktop](https://www.docker.com/products/docker-desktop/)** installed and running on your machine.

---

### Step 1: Build and Run the Docker Containers
First, you need to build the Docker images for the applications and start all the services.

1.  Open a terminal (like PowerShell or Command Prompt) in the root directory of the project, which is the folder that contains the `docker-compose.yml` file.

2.  Run the following command:
    ```bash
    docker-compose up --build
    ```
3.  This command will build the images and start three containers: one for the **MS SQL Server** database, one for the backend **API**, and one for the frontend **UI**. You will see a lot of log output in the terminal. ***Do not close this window***, as it keeps the containers running.

### Step 2: Set Up the Database
When the containers start for the first time, the database is empty. You need to connect to it and run the SQL scripts to create the tables and add data.

1.  **Wait for about 1 minute** to give the SQL Server container enough time to fully start.
2.  Next, open **SQL Server Management Studio (SSMS)**.
3.  Use the following details to connect to the database running in the Docker container:
    *   **Server name:** `localhost,1434`
    *   **Authentication:** `SQL Server Authentication`
    *   **Login:** `sa`
    *   **Password:** `Your_Strong_Password123!` *(or the password you set in the `docker-compose.yml` file)*
4.  When you try to connect, you will get a certificate error. To fix this, click the **Options >>** button in the connection window. Go to the **Connection Properties** tab and check the box for **"Trust server certificate"**. If you don't see that checkbox, go to the **Additional Connection Parameters** tab and type: `TrustServerCertificate=True`. Then click **Connect**.
5.  Once you are connected to the database, open the `setup.sql` file from the `Database` folder in your project. Execute this script by pressing `F5` or clicking the **Execute** button. This will create the `FlightsDb` database, the `Flights` table, and all the necessary stored procedures.
6.  After that, open the `data.sql` file from the same `Database` folder and execute it. This will insert the initial 10 flight records into the table.

### Step 3: Access the Running Applications
Now that the database is set up, the entire system is ready to use.

Open your web browser and go to the following addresses:
*   **Frontend UI:** ➡️ **[http://localhost:5001](http://localhost:5001)**
*   **Backend API (Swagger):** ➡️ **[http://localhost:5000/swagger](http://localhost:5000/swagger)**

### Step 4: Stopping the Application
When you are finished, you can stop all the running containers.

1.  Go back to the terminal window where the logs are running and press `Ctrl + C`.
2.  To completely remove the containers and free up system resources, run the following command in the same terminal:
    ```bash
    docker-compose down
    ```