using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Serialization;
using System.Threading.Tasks;

namespace FakeStoreAPI;

record Product(
    int Id, 
    string Title,
    double Price, 
    string Description,
    string Image,
    string Category
);