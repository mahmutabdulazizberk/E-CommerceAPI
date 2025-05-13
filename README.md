
### English Version
```markdown
# E-Commerce API

## Overview

This project is a RESTful API for an e-commerce platform, built with .NET 8. It provides core functionalities for managing users, products, categories, shopping carts, and orders. The application is designed to be containerized using Docker, with the API and PostgreSQL database running in separate containers, orchestrated by Docker Compose for ease of development and deployment.

## Features

* **User Authentication & Management**: Secure registration and login for users.
* **Product Catalog Management**: CRUD operations for products and categories.
* **Shopping Cart Functionality**: Add, update, remove items, and view cart.
* **Order Processing**: Create and manage customer orders.
* **Role-Based Authorization**: (Implicit, common in e-commerce APIs)

## Technologies Used

* **.NET 8**
* **ASP.NET Core 8** (for building RESTful APIs)
* **Entity Framework Core 8** (for data access with PostgreSQL)
* **PostgreSQL** (as the relational database)
* **Docker & Docker Compose** (for containerization and orchestration)
* **PgAdmin4** (web-based PostgreSQL administration tool)
* **JWT (JSON Web Tokens)** for secure authentication (inferred from `AuthenticationManager.cs`)

## Project Structure

The solution is organized into the following projects:

* `ECommerceApi/`: The main ASP.NET Core Web API project (startup project). Contains `Dockerfile` for building the API image.
* `Repository/`: The data access layer, implementing the repository pattern using Entity Framework Core and containing database migrations.
* `Service/`: The business logic layer, containing services that orchestrate operations between controllers and repositories.
* `Presentation/`: Contains the API controllers that handle incoming HTTP requests.
* `Entity/`: Contains domain models (database entities) and Data Transfer Objects (DTOs).
* `docker-compose.yml`: Located in the solution root (`ECommerceSolution/`), this file defines and configures the multi-container Docker application (API, database, PgAdmin).

## Prerequisites

* **Docker Desktop** (or Docker Engine with Docker Compose CLI plugin) installed and running.
* **.NET 8 SDK** (required for running Entity Framework Core migration commands locally or for development outside of Docker).

## Getting Started

Follow these steps to get the project up and running:

### 1. Clone the Repository

```bash
git clone <your-repository-url>
```

### 2. Configuration

#### Docker Compose (`docker-compose.yml`)
This file handles most of the service configuration, including environment variables for database credentials and PgAdmin.

#### API Connection String

* **When running with Docker Compose:**
    The API service (`ecommerceapi`) is configured to connect to the PostgreSQL database using the service name `postgres`. The connection string should be:
    `Server=postgres;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1`

    Ensure this is set in `ECommerceApi/appsettings.json` or, preferably, override it using environment variables in `docker-compose.yml` for the `ecommerceapi` service:

    ```yaml
    # In docker-compose.yml, under services.ecommerceapi.environment:
    environment:
      ASPNETCORE_ENVIRONMENT: Development
      ConnectionStrings__DefaultConnection: "Server=postgres;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1"
      ASPNETCORE_URLS: http://+:8080
    ```
    *(Uncomment/add `ConnectionStrings__DefaultConnection` if not already present and correctly configured in your `docker-compose.yml`)*.

* **Local Development/Migration Connection String:**
    For running EF Core migrations from your host machine (outside Docker), your `ECommerceApi/appsettings.Development.json` should have a connection string pointing to `localhost`:

    ```json
    {
      "ConnectionStrings": {
        "DefaultConnection": "Server=localhost;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1"
      }
      // ... other settings
    }
    ```

### 3. Running with Docker Compose (Recommended)

This is the easiest way to start all services.

1.  Navigate to the directory containing the `docker-compose.yml` file (e.g., `ECommerceSolution/`).
2.  Build and start all services in detached mode:

    ```bash
    docker-compose up -d --build
    ```

    This command will:
    * Build the Docker image for the `ecommerceapi` service using `ECommerceApi/Dockerfile`.
    * Pull the `postgres` and `dpage/pgadmin4` images if not already present.
    * Create and start containers for the API, PostgreSQL database, and PgAdmin.
    * Create a dedicated network (e.g., `ecom_network` as likely defined in your `docker-compose.yml`) for the services.
    * Mount a volume (e.g., `postgres_data`) for PostgreSQL data persistence.

#### Accessing Services:

* **E-Commerce API**: `http://localhost:8080` (or the host port mapped to container port 8080 in `docker-compose.yml`)
* **Swagger UI (API Documentation)**: `http://localhost:8080/swagger`
* **PgAdmin4**: `http://localhost:5050`
    * Default Email: `admin@example.com`
    * Default Password: `123456seven`

    To connect to the database from PgAdmin:
    1.  Click "Add New Server".
    2.  **General tab**: Name it (e.g., `ECommerceDB_Docker`).
    3.  **Connection tab**:
        * Host name/address: `postgres` (this is the service name of the PostgreSQL container within the Docker network)
        * Port: `5432`
        * Maintenance database: `ECommerceApiDB` (or `postgres`)
        * Username: `ecomuser`
        * Password: `StrongPass1`
    4.  Save the connection.

### 4. Database Migrations (Entity Framework Core)

Migrations are used to create and update the database schema.

* **Connection String for Migrations (when run from host machine):**
    As mentioned in the configuration section, ensure your `ECommerceApi/appsettings.Development.json` (or the configuration source your EF tools use) has:
    `Server=localhost;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1`

* **Applying Migrations:**
    1.  The services (especially `postgres`) must be running (e.g., via `docker-compose up -d`).
    2.  Open a terminal in the root of your solution (e.g., `ECommerceSolution/` directory, where the `.sln` file is, or a directory from which the projects can be resolved).
    3.  To **Add a New Migration** (if you've made changes to your EF Core models):

        ```bash
        dotnet ef migrations add YourMigrationName --project Repository --startup-project ECommerceApi --context AppDbContext
        ```
        Replace `YourMigrationName` with a descriptive name for the migration.

    4.  To **Apply Migrations to the Database**:

        ```bash
        dotnet ef database update --project Repository --startup-project ECommerceApi --context AppDbContext
        ```
        This command applies any pending migrations to the database schema.

    **Note**: Some applications are configured to apply migrations automatically on startup. Check your `ECommerceApi/Program.cs` for such logic. If so, manual database update might only be needed if that fails or for initial setup.

### 5. Running Services Manually with Docker (Alternative)

If you prefer not to use Docker Compose, you can run the containers individually.

1.  **Create a Docker Network** (recommended for service discovery by name):

    ```bash
    docker network create my-ecom-network
    ```

2.  **Start PostgreSQL Container**:

    ```bash
    docker run -d --name postgres --network my-ecom-network \
      -e POSTGRES_USER=ecomuser \
      -e POSTGRES_PASSWORD=StrongPass1 \
      -e POSTGRES_DB=ECommerceApiDB \
      -p 5432:5432 \
      -v postgres_custom_data:/var/lib/postgresql/data \
      postgres
    ```
    * `postgres_custom_data` is a named volume for data persistence.

3.  **Start PgAdmin4 Container**:

    ```bash
    docker run -d --name pgadmin4 --network my-ecom-network \
      -p 5050:80 \
      -e PGADMIN_DEFAULT_EMAIL=admin@example.com \
      -e PGADMIN_DEFAULT_PASSWORD=123456seven \
      dpage/pgadmin4
    ```
    Connect to `postgres` (hostname) from PgAdmin as described in the Docker Compose section.

4.  **Build and Run the E-Commerce API Container**:
    1.  Navigate to the `ECommerceApi` directory (containing the `Dockerfile`):
        ```bash
        cd ECommerceApi
        ```
    2.  Build the Docker image:
        ```bash
        docker build -t ecommerce-api-manual .
        ```
    3.  Run the API container:
        ```bash
        docker run -d --name ecommerceapi-manual --network my-ecom-network \
          -p 8080:8080 \
          -e ASPNETCORE_ENVIRONMENT=Development \
          -e ConnectionStrings__DefaultConnection="Server=postgres;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1" \
          -e ASPNETCORE_URLS="http://+:8080" \
          ecommerce-api-manual
        ```
        * The API will connect to the `postgres` container using its service name on `my-ecom-network`.
        * API will be accessible at `http://localhost:8080`.

#### Regarding User-Provided Manual Commands:

The commands you provided were:
```bash
# For PostgreSQL
docker run -d --name postgres --network bridge[INSPECT KOMUTU ÇALIŞTIRILARAK ALINACAK BİLGİ] -e POSTGRES_USER=ecomuser -e POSTGRES_PASSWORD=StrongPass1 -e POSTGRES_DB=ECommerceApiDB -p 5432:5432 postgres

# For PgAdmin4
docker run -d --name pgadmin4 --network bridge[INSPECT KOMUTU ÇALIŞTIRILARAK ALINACAK BİLGİ] -p 5050:80 -e PGADMIN_DEFAULT_EMAIL=admin@example.com -e PGADMIN_DEFAULT_PASSWORD=123456seven dpage/pgadmin4
```
The `bridge[INSPECT KOMUTU ÇALIŞTIRILARAK ALINACAK BİLGİ]` placeholder refers to the name of an existing Docker bridge network (literally, "information to be obtained by running the INSPECT COMMAND"). You can list networks with `docker network ls`. If you use the default `bridge` network, containers usually communicate via IP addresses, which can be unreliable as they might change. Using a custom user-defined bridge network (like `my-ecom-network` created above or the one Docker Compose creates) is generally preferred as it provides DNS resolution for container names.

---

## Connection Strings Summary

* **For EF Migrations (from host machine):**
    `Server=localhost;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1`
    *Use this in `appsettings.Development.json` or when prompted by EF tools if running commands from your local machine.*

* **For API running inside Docker (via Docker Compose or manual run on a custom network):**
    `Server=postgres;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1`
    *`postgres` is the service name of the PostgreSQL container. This should be set as an environment variable in `docker-compose.yml` or `docker run` command for the API service, or be the default in the API's `appsettings.json` that gets packaged into the image.*

* **User-provided "Normal Kullanımda" (Normal Usage) Connection String:**
    `Server=172.17.0.3;Port=5432;Database=ECommerceApiDB;Username=ecomuser;Password=StrongPass1`
    *This IP address (e.g., `172.17.0.3`) is likely the IP of the PostgreSQL container on a Docker bridge network (often the default bridge network) as seen from the host or another container on that same specific network. This IP can change if containers are restarted or recreated. Using service names with Docker Compose or custom bridge networks is more robust and recommended for inter-container communication.*

---

## API Endpoints Overview

The API exposes several endpoints for managing resources. Explore them using the Swagger UI:

* **Swagger UI**: `http://localhost:8080/swagger`

Key endpoint groups (based on controller names from your `Presentation/Controllers/` directory):

* `api/Authentication`: For user registration and login (e.g., `/api/Authentication/register`, `/api/Authentication/login`). (Controller: `AuthenticationController.cs`)
* `api/Users`: For user management. (Controller: `UsersController.cs`)
* `api/Categories`: For managing product categories. (Controller: `CategoiesController.cs` - *Note: The filename has a typo, "Categoies". The route might be `/api/Categories` if corrected in the controller's `[Route]` attribute.*)
* `api/Products`: For managing products. (Controller: `ProductsController.cs`)
* `api/Carts`: For shopping cart operations. (Controller: `CartsController.cs`)
* `api/Orders`: For managing orders. (Controller: `OrdersControllers.cs` - *Note: The filename is plural "OrdersControllers". The route might be `/api/Orders` if defined as such in the controller's `[Route]` attribute.*)

Refer to the Swagger documentation available at `http://localhost:8080/swagger` for detailed information on request/response models, parameters, and specific paths for each endpoint.

---

## Useful Docker Commands

* List running containers:
    ```bash
    docker ps
    ```
* List all containers (including stopped):
    ```bash
    docker ps -a
    ```
* View logs for a container:
    ```bash
    docker logs <container_name_or_id>
    ```
    Example: `docker logs ecommerceapi` (if using names from `docker-compose.yml`)
* Stop all services (if using Docker Compose, from the directory with `docker-compose.yml`):
    ```bash
    docker-compose down
    ```
* Stop and remove all services, including volumes (use with caution for `postgres_data` if you want to keep data):
    ```bash
    docker-compose down -v
    ```
* Stop a specific container:
    ```bash
    docker stop <container_name_or_id>
    ```
* Remove a specific container (must be stopped first):
    ```bash
    docker rm <container_name_or_id>
    ```
* List Docker images:
    ```bash
    docker images
    ```
* Remove a Docker image:
    ```bash
    docker rmi <image_id_or_name>
    ```
* List Docker networks:
    ```bash
    docker network ls
    ```
* Inspect container network settings:
    ```bash
    docker inspect <CONTAINER_NAME> --format '{{json .NetworkSettings.Networks}}'
    ```
    Example: `docker inspect postgres --format '{{json .NetworkSettings.Networks}}'`
* Access a running container's shell (for debugging):
    ```bash
    docker exec -it <container_name_or_id> /bin/bash  # (or /bin/sh if bash is not available)
    ```
    Example: `docker exec -it ecommerceapi /bin/sh`

---

## Useful System Commands

* Show processes listening on port 5432 (Windows):
    ```cmd
    netstat -aon | findstr :5432
    ```
    Then use `tasklist | findstr <PID>` (where `<PID>` is from the previous command) to find the process name.

* Show processes listening on port 5432 (Linux/macOS):
    ```bash
    sudo lsof -i :5432
    # or
    ss -tulnp | grep :5432
    ```

---

## Contributing

Pull requests are welcome. For major changes, please open an issue first to discuss what you would like to change.

Please make sure to:
* Write clear commit messages.
* Update documentation if you introduce new features or change existing ones.
* Add or update tests as appropriate (if tests are part of the project).
* Follow any existing coding style or conventions.

---

## License

[Specify License Here, e.g., MIT]
