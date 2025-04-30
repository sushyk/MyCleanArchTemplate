using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MyCleanArchTemplate.Adapter.WebApi.Requests;

public record CreateCustomerRequest(string Name, string Email);
