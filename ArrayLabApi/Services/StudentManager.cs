using System.Collections.Generic;

namespace ArrayLabApi.Services
{
    public class StudentManager
    {
        private const int Capacity = 20;

       
        private readonly int[] _rolls = new int[Capacity];

        
        private int _size = 0;

      
        public int Size => _size;

       
        public int[] Snapshot()
        {
            var copy = new int[_size]; 
            System.Array.Copy(_rolls, copy, _size); 
            return copy; 
        }

    
        public (bool ok, string msg) Add(int roll)
        {
            if (_size >= Capacity)
                return (false, "Class full! Cannot add more students.");

          
            _rolls[_size++] = roll;
           
            return (true, $"Student {roll} enrolled.");
        }
        public int Find(int roll)
        {
          
            for (int i = 0; i < _size; i++)
                if (_rolls[i] == roll) return i;
            return -1;
        }

        public (bool ok, string msg) Delete(int roll)
        {
            if (_size == 0)
                return (false, "No students to delete.");

            int pos = Find(roll);
            if (pos == -1)
                return (false, $"Roll number {roll} not found.");
            for (int i = pos; i < _size - 1; i++)
                _rolls[i] = _rolls[i + 1];
            _size--;
            return (true, $"Student {roll} removed (dropped).");
        }

        public (bool ok, string msg) Update(int oldRoll, int newRoll)
        {
            int pos = Find(oldRoll);
            if (pos == -1)
                return (false, $"Roll number {oldRoll} not found.");

            _rolls[pos] = newRoll;
            return (true, $"Roll number updated: {oldRoll} -> {newRoll}.");
        }
        public (bool found, int pos) Search(int roll)
        {
           
            int pos = Find(roll);
           
            return (pos != -1, pos);
        }
    }
}

// Namespace alias for DSALabsApi.Services
namespace DSALabsApi.Services
{
    public class StudentManager : ArrayLabApi.Services.StudentManager { }
}
