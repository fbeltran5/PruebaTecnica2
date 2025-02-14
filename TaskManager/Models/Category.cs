using System.Collections.Generic;

namespace TaskManager.Models
{
    public class Category
    {
        public int Id { get; set; }
        public string Name { get; set; }

        public List<Tarea> Tasks { get; set; }
    }
}
