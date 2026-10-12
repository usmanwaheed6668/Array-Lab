using ArrayLabApi.Services;
// ---------- TASKS 01 - 04: ArrayOperations Demo ----------
Console.WriteLine("\n--- Tasks 01 to 04: ArrayOperations Demo ---");
var arrayOps = new ArrayOperations(15);
arrayOps.Seed(10, 20, 30, 40, 50);

Console.WriteLine($"Initial Array: [{string.Join(", ", arrayOps.Snapshot())}] (Size: {arrayOps.Size}, Capacity: {arrayOps.Capacity})");

// Task 01: Insert at End & Beginning
Console.WriteLine("\n[Task 01] Insert at End (60) and Beginning (5):");
arrayOps.InsertAtEnd(60);
arrayOps.InsertAtBeginning(5);
Console.WriteLine($"Array after Task 01: [{string.Join(", ", arrayOps.Snapshot())}]");

// Task 02: Insert at Position, Delete at Position, Delete from End
Console.WriteLine("\n[Task 02] Insert at Position 2 (99), Delete at Position 3, Delete from End:");
var (insOk, insMsg) = arrayOps.InsertAtPosition(2, 99);
Console.WriteLine($"Insert pos 2: {insMsg} -> [{string.Join(", ", arrayOps.Snapshot())}]");

var (delOk, delMsg) = arrayOps.DeleteAtPosition(3);
Console.WriteLine($"Delete pos 3: {delMsg} -> [{string.Join(", ", arrayOps.Snapshot())}]");

arrayOps.DeleteFromEnd();
Console.WriteLine($"Delete from end -> [{string.Join(", ", arrayOps.Snapshot())}]");

// Task 03: Update at Position & Linear Search
Console.WriteLine("\n[Task 03] Update index 0 to 777, Search for 30:");
arrayOps.UpdateAtPosition(0, 777);
Console.WriteLine($"Updated index 0 to 777 -> [{string.Join(", ", arrayOps.Snapshot())}]");
int searchIdx = arrayOps.LinearSearch(30);
Console.WriteLine($"Linear Search for 30: found at index {searchIdx}");

// Task 04: Linear Search All & Delete from Beginning
Console.WriteLine("\n[Task 04] Search all occurrences of 20, Delete from Beginning:");
var occurrences = arrayOps.LinearSearchAll(20);
Console.WriteLine($"Occurrences of 20: found at indices [{string.Join(", ", occurrences)}]");
arrayOps.DeleteFromBeginning();
Console.WriteLine($"After Delete from Beginning -> [{string.Join(", ", arrayOps.Snapshot())}]");

// ---------- TASK 05: StudentManager Demo ----------
Console.WriteLine("\n\n--- Task 05: StudentManager Demo ---");
var studentMgr = new StudentManager();

Console.WriteLine("Adding 10 students (Rolls 1001-1010):");
for (int i = 0; i < 10; i++)
{
    var (_, msg) = studentMgr.Add(1001 + i);
    Console.WriteLine($"  {msg}");
}
Console.WriteLine($"Enrolled Students: [{string.Join(", ", studentMgr.Snapshot())}]");

Console.WriteLine("\nDropping students 1003 and 1007:");
Console.WriteLine($"  {studentMgr.Delete(1003).msg}");
Console.WriteLine($"  {studentMgr.Delete(1007).msg}");
Console.WriteLine($"After drops: [{string.Join(", ", studentMgr.Snapshot())}]");

Console.WriteLine("\nUpdating student roll 1005 to 2005:");
Console.WriteLine($"  {studentMgr.Update(1005, 2005).msg}");
Console.WriteLine($"After update: [{string.Join(", ", studentMgr.Snapshot())}]");

Console.WriteLine("\nSearching for students:");
var (f1001, pos1001) = studentMgr.Search(1001);
Console.WriteLine($"  Roll 1001: {(f1001 ? $"Found at position {pos1001}" : "Not found")}");

var (f1003, pos1003) = studentMgr.Search(1003);
Console.WriteLine($"  Roll 1003: {(f1003 ? $"Found at position {pos1003}" : "Not found")}");

var (f2005, pos2005) = studentMgr.Search(2005);
Console.WriteLine($"  Roll 2005: {(f2005 ? $"Found at position {pos2005}" : "Not found")}");

