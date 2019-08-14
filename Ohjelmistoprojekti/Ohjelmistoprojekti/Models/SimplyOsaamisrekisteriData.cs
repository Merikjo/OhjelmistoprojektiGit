using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace Ohjelmistoprojekti.Models
{
    public class SimplyOsaamisrekisteriData
    {
        public SimplyOsaamisrekisteriData()
        {
            this.Henkilot = new HashSet<Henkilot>();
            this.HenkilonOsaamiset = new HashSet<HenkilonOsaamiset>();
        }


        public string Etunimi { get; set; }
        public string Sukunimi { get; set; }
        public string TyoPuhelin { get; set; }
        public string TyoSahkoposti { get; set; }
        public string Organiaatio { get; set; }
        public int? Henkilonumero { get; set; }


        public string Kuvaus { get; set; }

        public int? HenkilonOsaamisID { get; set; }
        public int OsaamisaiheID { get; set; }
        public int? HenkiloID { get; set; }
        public int? Osaamistaso { get; set; }

        public int? Osaamisaiheet { get; set; }

        public int? ListOsaamiset { get; set; }

        //public virtual Henkilot Henkilot { get; set; }
        //public virtual Osaamisaiheet Osaamisaiheet { get; set; }


        public virtual ICollection<Henkilot> Henkilot { get; set; }
        public virtual ICollection<HenkilonOsaamiset> HenkilonOsaamiset { get; set; }

    }
}