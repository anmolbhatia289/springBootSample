# Spring Boot Demo Project

A simple Spring Boot project with a REST controller and comprehensive tests.

## Project Structure

```
src/
├── main/
│   ├── java/com/example/springdemo/
│   │   ├── SpringDemoApplication.java    # Main application class
│   │   └── controller/
│   │       └── HelloController.java      # REST controller with API endpoints
│   └── resources/
│       └── application.properties        # Application configuration
└── test/
    └── java/com/example/springdemo/
        ├── SpringDemoApplicationTests.java   # Application context test
        └── controller/
            └── HelloControllerTest.java      # Controller unit tests
```

## API Endpoints

The `HelloController` provides the following endpoints:

### GET /api/hello
Returns a simple greeting message.

**Response:**
```json
{
  "message": "Hello, World!",
  "status": "success"
}
```

### GET /api/hello/{name}
Returns a personalized greeting message.

**Example:** `GET /api/hello/Spring`

**Response:**
```json
{
  "message": "Hello, Spring!",
  "status": "success"
}
```

### POST /api/echo
Echoes back the request body with a timestamp.

**Request Body:**
```json
{
  "name": "John",
  "age": 30,
  "city": "New York"
}
```

**Response:**
```json
{
  "echo": {
    "name": "John",
    "age": 30,
    "city": "New York"
  },
  "timestamp": 1692192000000,
  "status": "success"
}
```

### GET /api/health
Returns the health status of the application.

**Response:**
```json
{
  "status": "UP",
  "message": "Application is running successfully",
  "timestamp": "2025-08-06T22:52:03.464246900"
}
```

## Error Handling

The application includes comprehensive error handling for various scenarios:

### 404 - Not Found
When accessing invalid URLs, the application returns a structured error response:

**Example:** `GET /api/nonexistent`

**Response:**
```json
{
  "timestamp": "2025-08-06T22:52:12.463107600",
  "status": 404,
  "error": "Resource not found",
  "message": "The requested URL '/api/nonexistent' was not found on this server.",
  "path": "/api/nonexistent"
}
```

### 405 - Method Not Allowed
When using unsupported HTTP methods on endpoints:

**Example:** `POST /api/hello`

**Response:**
```json
{
  "timestamp": "2025-08-06T22:52:37.483724300",
  "status": 405,
  "error": "Method not allowed",
  "message": "Request method 'POST' not supported. Supported methods: [GET]"
}
```

### 400 - Bad Request
For malformed requests or validation errors:

**Response:**
```json
{
  "timestamp": "2025-08-06T17:13:47.168+00:00",
  "status": 400,
  "error": "Bad request",
  "message": "Request validation failed"
}
```

### 500 - Internal Server Error
For unexpected server errors:

**Response:**
```json
{
  "timestamp": "2025-08-06T17:13:47.168+00:00",
  "status": 500,
  "error": "Internal server error",
  "message": "An unexpected error occurred. Please try again later."
}
```

## Running the Application

### Prerequisites
- Java 17 or higher
- Maven 3.6 or higher

### Build and Run
1. **Compile the project:**
   ```bash
   mvn clean compile
   ```

2. **Run tests:**
   ```bash
   mvn test
   ```

3. **Start the application:**
   ```bash
   mvn spring-boot:run
   ```

   The application will start on `http://localhost:8080`

### VS Code Tasks
The project includes VS Code tasks for easy development:
- **Spring Boot: Run** - Starts the application
- **Maven: Test** - Runs all tests
- **Maven: Clean and Compile** - Cleans and compiles the project

## Testing

The project includes comprehensive tests:

- **SpringDemoApplicationTests**: Tests application context loading
- **HelloControllerTest**: Unit tests for all controller endpoints using MockMvc

### Test Coverage
- ✅ GET /api/hello
- ✅ GET /api/hello/{name}
- ✅ POST /api/echo

Run tests with: `mvn test`

## Development

To test the API manually, you can use curl or any HTTP client:

```bash
# Test the hello endpoint
curl http://localhost:8080/api/hello

# Test the personalized hello endpoint
curl http://localhost:8080/api/hello/YourName

# Test the echo endpoint
curl -X POST http://localhost:8080/api/echo \
  -H "Content-Type: application/json" \
  -d '{"name": "John", "age": 30}'
```
