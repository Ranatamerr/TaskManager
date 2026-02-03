# Task Management API

A simple RESTful API for managing tasks, built with **ASP.NET Core** and **Entity Framework Core** using **SQLite**.

---

## Features

- List all tasks (`GET /api/tasks`) with optional status filtering
- Get a single task by ID (`GET /api/tasks/{id}`)
- Create a new task (`POST /api/tasks`)
- Update a task (`PUT /api/tasks/{id}`)
- Delete a task (`DELETE /api/tasks/{id}`)
- Input validation with meaningful error messages
- Seeded sample data on first run
- Swagger UI for interactive API testing

---

## Setup

1. **Clone the repository**

```bash
git clone https://github.com/Ranatamerr/TaskManager.git
cd TaskManager
```

2. **Restore packages**

```bash
dotnet restore
```

3. **Run the application**

```bash
dotnet run
```

The API will run at:
- `http://localhost:5117`
- `https://localhost:7117`

4. **Access Swagger UI**

Open:
```
https://localhost:7117/swagger
```
to explore and test the endpoints interactively.

---

## Database

- Uses **SQLite** (`tasks.db`) located in the project root.
- Database is automatically created and seeded on first run with example tasks.

**Seeded tasks include:**
- Finish assignment (High, Pending)
- Read documentation (Medium, InProgress)
- Test API (Low, Pending)

---

## Testing

A Postman collection is available for testing all endpoints with example requests and scenarios.

**[View Postman Collection](https://ranatamerr-6525919.postman.co/workspace/ranatamerr's-Workspace~c0a634c6-a83c-46a7-8d16-e23d09974072/collection/48953531-17e389fa-3ce1-4dec-a7d4-c1b14b9badfa?action=share&creator=48953531)**

You can also import the included `TaskManagementAPI.postman_collection.json` file directly into Postman.

---

## API Endpoints

| Method | Endpoint | Description |
|--------|----------|-------------|
| GET | `/api/tasks` | Get all tasks (optional status filter) |
| GET | `/api/tasks/{id}` | Get a task by ID |
| POST | `/api/tasks` | Create a new task |
| PUT | `/api/tasks/{id}` | Update a task by ID |
| DELETE | `/api/tasks/{id}` | Delete a task by ID |

---

## Notes

- All dates use UTC.
- Task Priority and Status are enums (Low/Medium/High, Pending/InProgress/Completed).
- Input validation returns clear messages for missing fields.