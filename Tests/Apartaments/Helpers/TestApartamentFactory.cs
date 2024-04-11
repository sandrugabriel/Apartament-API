using ApartamentAPI.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Tests.Apartaments.Helpers
{
    public class TestApartamentFactory
    {

        public static Apartament CreateApartament(int id)
        {
            return new Apartament
            {
                Id = id,
                Room = id + 2,
                Address = "test" + id,
                Price = (id+5) * 10

            };
        }

        public static List<Apartament> CreateApartaments(int count) { 
        
            List<Apartament> list = new List<Apartament>();
            for(int i=1;i<count;i++)
            {
                list.Add(CreateApartament(i));
            }
            
            return list;
        }



    }
}
