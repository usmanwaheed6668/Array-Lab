# Lab 1: Data Structures & Algorithms — Array Operations & Student Manager

A comprehensive .NET 9 solution implementing fundamental **1D Array Operations** and an applied **Student Management System** using low-level array manipulation (without relying on high-level dynamic collections for core logic).

The solution includes:
1. **`ArrayLab`**: An interactive Console Application providing step-by-step execution and terminal visualization for all lab tasks.
2. **`ArrayLabApi`**: A production-ready ASP.NET Core Web API with Swagger/OpenAPI documentation, demonstrating RESTful design, singleton state management, and generic response modeling (`ApiResponse<T>`).

---

## 📋 Table of Contents

- [Project Architecture](#-project-architecture)
- [Summary of Tasks](#-summary-of-tasks)
  - [Task 01: Traversal, Insertion at End & Beginning](#task-01-traversal-insertion-at-end--beginning)
  - [Task 02: Positional Insertion & Deletions](#task-02-positional-insertion--deletions)
  - [Task 03: Element Update & Linear Search](#task-03-element-update--linear-search)
  - [Task 04: Search All Occurrences & Deletion from Beginning](#task-04-search-all-occurrences--deletion-from-beginning)
  - [Task 05: Applied System — Student Management System](#task-05-applied-system--student-management-system)
- [Algorithm & Complexity Analysis](#-algorithm--complexity-analysis)
- [REST API Reference](#-rest-api-reference)
  - [Array Operations Endpoints](#array-operations-endpoints)
  - [Student Manager Endpoints](#student-manager-endpoints)
- [Standard API Response Format](#-standard-api-response-format)
- [How to Build and Run](#-how-to-build-and-run)
  - [Prerequisites](#prerequisites)
  - [Running the Console Application](#running-the-console-application)
  - [Running the Web API with Swagger](#running-the-web-api-with-swagger)
- [Demonstration & Sample Output](#-demonstration--sample-output)

---

## 🏗 Project Architecture

```
LAB 1 DSA/
├── ArrayLab.slnx                   # Solution file
├── README.md                       # Comprehensive project documentation
├── ArrayLab/                       # Console Application Project
│   ├── ArrayLab.csproj
│   └── Program.cs                  # Console driver demonstrating Tasks 01 to 05
└── ArrayLabApi/                    # ASP.NET Core Web API Project
    ├── ArrayLabApi.csproj
    ├── Program.cs                  # Web API configuration & dependency injection
    ├── Controllers/
    │   ├── ArrayController.cs      # Endpoints for Tasks 01 - 04
    │   └── StudentsController.cs   # Endpoints for Task 05
    ├── Models/
    │   └── Dtos.cs                 # Request DTOs and ApiResponse<T>
    └── Services/
        ├── ArrayOperations.cs      # Core array logic (Capacity: 15)
        └── StudentManager.cs       # Roster management logic (Capacity: 20)
```

---

## 📌 Summary of Tasks

### Task 01: Traversal, Insertion at End & Beginning

Focuses on initializing fixed-size arrays, snapshot extraction/traversal, and boundary-safe additions.

#### 1. Array Traversal & Snapshot
- **Class / Method:** `ArrayOperations.Snapshot()`
- **Concept:** Traverses the active elements of the underlying array `_arr` from index `0` up to `_size - 1` and creates an exact clone without exposing the internal private buffer.
- **Complexity:** Time $\mathcal{O}(N)$, Space $\mathcal{O}(N)$.

#### 2. Insert at End
- **Class / Method:** `ArrayOperations.InsertAtEnd(int value)`
- **Concept:**
  1. Checks if the array is full (`_size >= Capacity`). If full, returns `false` (Array Overflow).
  2. Places `value` at `_arr[_size]`.
  3. Increments `_size`.
- **Complexity:** Time $\mathcal{O}(1)$, Space $\mathcal{O}(1)$.

#### 3. Insert at Beginning
- **Class / Method:** `ArrayOperations.InsertAtBeginning(int value)`
- **Concept:**
  1. Checks for array overflow (`_size >= Capacity`).
  2. Shifts all existing elements one position to the right, iterating backward from index `_size` down to `1`:
     $$\_arr[i] = \_arr[i - 1]$$
  3. Places `value` at index `0`.
  4. Increments `_size`.
- **Complexity:** Time $\mathcal{O}(N)$ due to $N$ shift operations, Space $\mathcal{O}(1)$.

---

### Task 02: Positional Insertion & Deletions

Focuses on arbitrary index manipulations and right/left element shifting.

#### 1. Insert at Specific Position
- **Class / Method:** `ArrayOperations.InsertAtPosition(int pos, int value)`
- **Concept:**
  1. Checks overflow condition (`_size >= Capacity`).
  2. Validates that `0 <= pos <= _size`.
  3. Shifts elements from index `_size` down to `pos + 1` one step to the right:
     $$\_arr[i] = \_arr[i - 1]$$
  4. Inserts `value` at `_arr[pos]`.
  5. Increments `_size`.
- **Complexity:** Time $\mathcal{O}(N)$, Space $\mathcal{O}(1)$.

#### 2. Delete at Specific Position
- **Class / Method:** `ArrayOperations.DeleteAtPosition(int pos)`
- **Concept:**
  1. Checks for underflow (`_size == 0`).
  2. Validates index boundary (`0 <= pos < _size`).
  3. Shifts elements left from `pos` up to `_size - 2`:
     $$\_arr[i] = \_arr[i + 1]$$
  4. Decrements `_size`.
- **Complexity:** Time $\mathcal{O}(N)$, Space $\mathcal{O}(1)$.

#### 3. Delete from End
- **Class / Method:** `ArrayOperations.DeleteFromEnd()`
- **Concept:**
  1. Checks if array is empty (`_size == 0`).
  2. Decrements `_size` by 1, logically dropping the last element.
- **Complexity:** Time $\mathcal{O}(1)$, Space $\mathcal{O}(1)$.

---

### Task 03: Element Update & Linear Search

Focuses on index-based mutation and finding the first match of a search key.

#### 1. Update at Position
- **Class / Method:** `ArrayOperations.UpdateAtPosition(int pos, int newValue)`
- **Concept:**
  1. Validates `0 <= pos < _size`.
  2. Directly mutates `_arr[pos] = newValue`.
- **Complexity:** Time $\mathcal{O}(1)$, Space $\mathcal{O}(1)$.

#### 2. Linear Search (First Occurrence)
- **Class / Method:** `ArrayOperations.LinearSearch(int key)`
- **Concept:**
  1. Iterates from index `0` to `_size - 1`.
  2. Compares each element with `key`.
  3. Returns index immediately upon match.
  4. Returns `-1` if key is not found.
- **Complexity:** Best case $\mathcal{O}(1)$ (element is at index 0), Worst/Average case $\mathcal{O}(N)$.

---

### Task 04: Search All Occurrences & Deletion from Beginning

Focuses on multi-match scanning and head-of-array deletion.

#### 1. Linear Search All Occurrences
- **Class / Method:** `ArrayOperations.LinearSearchAll(int key)`
- **Concept:**
  1. Initializes a list for indices.
  2. Iterates from `0` to `_size - 1` through the entire active array.
  3. Adds matching indices to the result list.
  4. Returns list of all matching positions (or empty list if absent).
- **Complexity:** Time $\mathcal{O}(N)$, Space $\mathcal{O}(K)$ where $K$ is number of matches.

#### 2. Delete from Beginning
- **Class / Method:** `ArrayOperations.DeleteFromBeginning()`
- **Concept:**
  1. Validates underflow (`_size == 0`).
  2. Shifts all subsequent items left by one position from index `0` to `_size - 2`:
     $$\_arr[i] = \_arr[i + 1]$$
  3. Decrements `_size`.
- **Complexity:** Time $\mathcal{O}(N)$ due to full-length left shifts, Space $\mathcal{O}(1)$.

---

### Task 05: Applied System — Student Management System

Simulates a real-world student roster using fixed-size array operations in `StudentManager.cs` (default class capacity: 20 students).

#### Core Operations:
1. **Enroll Student (`Add`)**:
   - Validates class capacity (`_size < Capacity`).
   - Appends the roll number to the end of `_rolls`.
   - Returns confirmation or capacity overflow message.
2. **Find Student Position (`Find`)**:
   - Performs a linear search over active roll numbers.
   - Returns zero-based index or `-1` if not found.
3. **Drop Student (`Delete`)**:
   - Validates underflow (`_size == 0`).
   - Locates student using `Find(roll)`.
   - Shifts all subsequent roll numbers left to fill the gap.
   - Decrements `_size`.
4. **Update Student Roll (`Update`)**:
   - Locates old roll number via `Find(oldRoll)`.
   - Replaces with `newRoll`.
5. **Search Student (`Search`)**:
   - Returns `(bool found, int pos)` tuple indicating presence and current index.

#### Standard Demo Workflow:
Demonstrated in both `ArrayLab/Program.cs` and `POST /api/students/demo`:
- **Step 1:** Enrolls 10 students (Roll numbers: `1001` through `1010`).
- **Step 2:** Drops 2 students (`1003` and `1007`), verifying shift logic.
- **Step 3:** Updates 1 student roll (`1005` $\rightarrow$ `2005`).
- **Step 4:** Searches for 3 students:
  - `1001` $\rightarrow$ Found at position `0`.
  - `1003` $\rightarrow$ Not found (successfully dropped).
  - `2005` $\rightarrow$ Found at position `3`.

---

## ⏱ Algorithm & Complexity Analysis

| Operation | Method Name | Best Case Time | Worst Case Time | Space Complexity | Reason / Mechanism |
|---|---|---|---|---|---|
| **Traversal / Snapshot** | `Snapshot()` | $\mathcal{O}(N)$ | $\mathcal{O}(N)$ | $\mathcal{O}(N)$ | Copies $N$ active items into a new array |
| **Insert at End** | `InsertAtEnd()` | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | Direct index assignment at `_arr[_size]` |
| **Insert at Beginning** | `InsertAtBeginning()` | $\mathcal{O}(N)$ | $\mathcal{O}(N)$ | $\mathcal{O}(1)$ | Shifts all $N$ items right |
| **Insert at Position** | `InsertAtPosition()` | $\mathcal{O}(1)$ | $\mathcal{O}(N)$ | $\mathcal{O}(1)$ | Shifts $(N - \text{pos})$ items right |
| **Delete from End** | `DeleteFromEnd()` | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | Decrements `_size` pointer |
| **Delete from Beginning** | `DeleteFromBeginning()` | $\mathcal{O}(N)$ | $\mathcal{O}(N)$ | $\mathcal{O}(1)$ | Shifts $(N - 1)$ items left |
| **Delete at Position** | `DeleteAtPosition()` | $\mathcal{O}(1)$ | $\mathcal{O}(N)$ | $\mathcal{O}(1)$ | Shifts $(N - 1 - \text{pos})$ items left |
| **Update at Position** | `UpdateAtPosition()` | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | Direct array indexing `_arr[pos]` |
| **Linear Search** | `LinearSearch()` | $\mathcal{O}(1)$ | $\mathcal{O}(N)$ | $\mathcal{O}(1)$ | Scans sequentially until target is found |
| **Linear Search All** | `LinearSearchAll()` | $\mathcal{O}(N)$ | $\mathcal{O}(N)$ | $\mathcal{O}(K)$ | Must scan entire array of $N$ items |
| **Student Add** | `StudentManager.Add()` | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | $\mathcal{O}(1)$ | Append at end |
| **Student Drop** | `StudentManager.Delete()`| $\mathcal{O}(N)$ | $\mathcal{O}(N)$ | $\mathcal{O}(1)$ | Search $\mathcal{O}(N)$ + Left shift $\mathcal{O}(N)$ |
| **Student Update** | `StudentManager.Update()`| $\mathcal{O}(N)$ | $\mathcal{O}(N)$ | $\mathcal{O}(1)$ | Search $\mathcal{O}(N)$ + Direct update $\mathcal{O}(1)$ |

---

## 🌐 REST API Reference

The Web API runs on ASP.NET Core and exposes all capabilities over HTTP with JSON payloads.

### Array Operations Endpoints (`/api/array`)

| HTTP Method | Endpoint | Request Body | Description |
|---|---|---|---|
| `GET` | `/api/array/traverse` | _None_ | Retrieves snapshot of current array, size, and capacity |
| `POST` | `/api/array/insert-end` | `{"value": 60}` | Appends an element to the end of the array |
| `POST` | `/api/array/insert-beginning`| `{"value": 5}` | Prepends an element at index 0 (shifts all elements right) |
| `POST` | `/api/array/insert-position` | `{"position": 2, "value": 99}` | Inserts element at specified index (shifts right) |
| `DELETE`| `/api/array/delete-position` | `{"position": 3}` | Deletes element at specified index (shifts left) |
| `DELETE`| `/api/array/delete-end` | _None_ | Removes the last element from the array |
| `DELETE`| `/api/array/delete-beginning` | _None_ | Removes the first element (shifts all elements left) |
| `PUT` | `/api/array/update` | `{"position": 0, "newValue": 777}` | Updates element at given index |
| `POST` | `/api/array/search` | `{"key": 30}` | Performs linear search; returns first found index |
| `POST` | `/api/array/search-all` | `{"key": 20}` | Returns list of all indices matching the key |
| `POST` | `/api/array/reset` | _None_ | Resets array to initial seed: `[10, 20, 30, 40, 50]` |

### Student Manager Endpoints (`/api/students`)

| HTTP Method | Endpoint | Request Body | Description |
|---|---|---|---|
| `GET` | `/api/students` | _None_ | Gets list of all currently enrolled student roll numbers |
| `POST` | `/api/students` | `{"rollNumber": 1001}` | Enrolls a new student roll number |
| `DELETE`| `/api/students` | `{"rollNumber": 1003}` | Drops a student and closes the gap |
| `PUT` | `/api/students` | `{"oldRollNumber": 1005, "newRollNumber": 2005}` | Updates an existing student's roll number |
| `GET` | `/api/students/search/{roll}` | _None_ (Route param) | Searches for student roll number and returns position |
| `POST` | `/api/students/demo` | _None_ | Automatically runs the full lab scenario and returns execution log |

---

## 📦 Standard API Response Format

All API responses follow the consistent generic structure defined in `ApiResponse<T>`:

```json
{
  "success": true,
  "message": "Inserted 60 at end.",
  "data": [10, 20, 30, 40, 50, 60]
}
```

When an error occurs (such as array overflow or invalid index), the API returns appropriate HTTP status codes (`400 Bad Request`, `404 Not Found`) with:

```json
{
  "success": false,
  "message": "Overflow: array is full.",
  "data": null
}
```

---

## 🚀 How to Build and Run

### Prerequisites
- [.NET 9.0 SDK](https://dotnet.microsoft.com/download/dotnet/9.0) or higher installed.

### Running the Console Application

To run the interactive console demonstration executing all tasks:

```powershell
dotnet run --project ArrayLab
```

### Running the Web API with Swagger

To start the REST API server:

```powershell
dotnet run --project ArrayLabApi
```

Once running:
- Open your browser to the URL displayed in the console (e.g., `https://localhost:7145/swagger` or `http://localhost:5057/swagger`).
- Use the interactive **Swagger UI** to test every endpoint with custom inputs.

---

## 🖥 Demonstration & Sample Output

When running `dotnet run --project ArrayLab`, the console demonstrates the operations as follows:

```text
--- Tasks 01 to 04: ArrayOperations Demo ---
Initial Array: [10, 20, 30, 40, 50] (Size: 5, Capacity: 15)

[Task 01] Insert at End (60) and Beginning (5):
Array after Task 01: [5, 10, 20, 30, 40, 50, 60]

[Task 02] Insert at Position 2 (99), Delete at Position 3, Delete from End:
Insert pos 2: Inserted. -> [5, 10, 99, 20, 30, 40, 50, 60]
Delete pos 3: Deleted. -> [5, 10, 99, 30, 40, 50, 60]
Delete from end -> [5, 10, 99, 30, 40, 50]

[Task 03] Update index 0 to 777, Search for 30:
Updated index 0 to 777 -> [777, 10, 99, 30, 40, 50]
Linear Search for 30: found at index 3

[Task 04] Search all occurrences of 20, Delete from Beginning:
Occurrences of 20: found at indices []
After Delete from Beginning -> [10, 99, 30, 40, 50]


--- Task 05: StudentManager Demo ---
Adding 10 students (Rolls 1001-1010):
  Student 1001 enrolled.
  Student 1002 enrolled.
  Student 1003 enrolled.
  Student 1004 enrolled.
  Student 1005 enrolled.
  Student 1006 enrolled.
  Student 1007 enrolled.
  Student 1008 enrolled.
  Student 1009 enrolled.
  Student 1010 enrolled.
Enrolled Students: [1001, 1002, 1003, 1004, 1005, 1006, 1007, 1008, 1009, 1010]

Dropping students 1003 and 1007:
  Student 1003 removed (dropped).
  Student 1007 removed (dropped).
After drops: [1001, 1002, 1004, 1005, 1006, 1008, 1009, 1010]

Updating student roll 1005 to 2005:
  Roll number updated: 1005 -> 2005.
After update: [1001, 1002, 1004, 2005, 1006, 1008, 1009, 1010]

Searching for students:
  Roll 1001: Found at position 0
  Roll 1003: Not found
  Roll 2005: Found at position 3
```
