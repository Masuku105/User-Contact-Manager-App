using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Contact_Manager_DataAccess
{
    public class ContactDatabaseSettings
    {
        public string DatabaseName { get; set; } = null!;
        public string ContactCollection { get; set; } = null!;
        public string ConnectionString { get; set; } = null!;
    }
}
