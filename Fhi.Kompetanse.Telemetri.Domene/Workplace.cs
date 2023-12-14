using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fhi.Kompetanse.Telemetri.Domene;

public class Workplace
{
    public int Id { get; set; }
    public int PersonId { get; set; } 
    public string? CompanyName { get; set; }
    public string? Street { get; set; }
    public string? City { get; set; }
}
