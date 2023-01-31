
using Firebase.Database;
using Firebase.Database.Query;
using System;

namespace FirebaseSampleApp
{
    class Program
    {
        static void Main(string[] args)
        {
            var firebaseClient = new FirebaseClient("https://newsheadlines-945c4-default-rtdb.firebaseio.com/");


            var repository = new FirebaseRepository<SampleData>(firebaseClient);

            var sampleData = new SampleData
            {
                Name = "John Doe",
                Age = 35,
                Email = "johndoe@example.com"
            };

            var result = repository.Add(sampleData).Result;
            Console.WriteLine($"Data stored: (Key: {result.Key})");

            Console.ReadLine();
        }
    }

    public class FirebaseRepository<T> where T : class
    {
        private readonly FirebaseClient _firebaseClient;

        public FirebaseRepository(FirebaseClient firebaseClient)
        {
            _firebaseClient = firebaseClient;
        }

        public async System.Threading.Tasks.Task<FirebaseObject<T>> Add(T data)
        {
            return await _firebaseClient
                .Child(typeof(T).Name)
                .PostAsync(data);
        }
    }

    public class SampleData
    {
        public string Name { get; set; }
        public int Age { get; set; }
        public string Email { get; set; }
    }
}
