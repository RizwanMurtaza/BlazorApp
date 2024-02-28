using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CleanArchitecture.Blazor.Domain.Common.Entities;

namespace CleanArchitecture.Blazor.Domain.Entities.Company;
public class Address : BaseAuditableEntity, IEquatable<Address>
{

    public int EntityId { get; set; }

    public AddressType AddressType { get; set; }
        public string Address1 { get; set; }
       
        public string Address2 { get; set; }
       
        public string Suite { get; set; }
       
        public string City { get; set; }
       
        public string State { get; set; }
       
        public string PostalCode { get; set; }
       
        public string Plus4 { get; set; }
       
        public string LastLine { get; set; }
       
        public string CountryCode { get; set; }
       
        public string Urbanization { get; set; }
       
        public bool Equals(Address other)
        {
            return
                Address1 == other.Address1 &&
                Address2 == other.Address2 &&
                Suite == other.Suite &&
                City == other.City &&
                State == other.State &&
                PostalCode == other.PostalCode &&
                Plus4 == other.Plus4 &&
                CountryCode == other.CountryCode &&
                Urbanization == other.Urbanization;
        }
    }
