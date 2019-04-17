using Microsoft.Azure.Documents.Client;
using PsHelloAzure.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace PsHelloAzure.Services
{
    public class CourseStore
    {
        private DocumentClient client;
        private Uri coursesLink;

        public CourseStore()
        {
            var uri = new Uri("https://cosmosdemodb.documents.azure.com:443//");
            var key = "7Sm44zcbFt9RyKhBfpauZXrZBYxC5xyS9cmlHOWQ4XKh2g2LvDEv9a2ZLIk8hxyKbfiS65pRB9XObBHWTIksRw==";
            client = new DocumentClient(uri, key);
            coursesLink = UriFactory.CreateDocumentCollectionUri("cosmosdbdemo", "courses");
        }

        public async Task InsertCourses(IEnumerable<Course> courses)
        {
            foreach(var course in courses)
            {
                await client.CreateDocumentAsync(coursesLink, course);
            }
        }

        public IEnumerable<Course> GetAllCourses()
        {
            var option = new FeedOptions { EnableCrossPartitionQuery = true };
            var courses = client.CreateDocumentQuery<Course>(coursesLink, option)
                                .OrderBy(c => c.Title);

            return courses;
        }
    }
}
