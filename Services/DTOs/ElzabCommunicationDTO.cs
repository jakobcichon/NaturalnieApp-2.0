using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace NaturalnieApp2.Services.DTOs
{
    [Table("elzab_communication")]
    public class ElzabCommunicationDTO
    {
        public enum CommunicationStatus
        {
            Started,
            FinishSuccess,
            FinishFailed,
        }

        [Key]
        public int Id { get; set; }
        public DateTime DateOfCommunication { get; set; }
        public CommunicationStatus StatusOfCommunication { get; set; }
        public string ElzabCommandName { get; set; }
        public int ElzabCommandReportStatusCode { get; set; }
        public string ElzabCommandReportStatusText { get; set; }

    }
}
