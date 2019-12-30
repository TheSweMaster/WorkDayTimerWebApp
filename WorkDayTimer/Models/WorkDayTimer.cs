using Microsoft.AspNetCore.Identity;
using System;
using System.ComponentModel.DataAnnotations;

namespace WorkDayTimerWebApp.Models
{
    public class WorkDayTimer
    {
        [Key]
        public int Id { get; set; }
        public string UserId { get; set; }
        public IdentityUser User { get; set; }

        [Required]
        [DataType(DataType.Text)]
        public string Name { get; set; }

        [Required]
        [DataType(DataType.Date)]
        [DisplayFormat(ApplyFormatInEditMode = true, DataFormatString = "{0:yyyy-MM-dd}")]
        public DateTime Date { get; set; }

        [Range(0, 24)]
        public int WorkDayHours { get; set; }

        [DataType(DataType.Duration)]
        public DateTime TimerTime { get; set; }

        public bool IsRunning { get; set; }

        [Range(0, 24)]
        public int MaxTimerCount { get; set; }

    }
}
