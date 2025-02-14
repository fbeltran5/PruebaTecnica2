using System;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace TaskManager.Models
{
    public class Tarea
    {
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Range(1, 5)]
        public int Priority { get; set; }

        public bool IsCompleted { get; set; }
        public DateTime? DueDate { get; set; }

        public int CategoryId { get; set; }

        public Category Category { get; set; }
        public SelectList CategoriesList { get; set; }
    }
}
