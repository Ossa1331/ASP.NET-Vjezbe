using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;

namespace Vjezba.Model
{
    public class Osoba
    {
        public string Ime { get; set; }
        public string Prezime { get; set; }

        private string _oib;
        private string _jmbg;

        public DateTime DatumRodjenja
        {
            get;set;
        }
        public string OIB
        {
            get
            {
                return _oib;
            }
            set
            {
                if(value.Length==11 && long.TryParse(value, out long result))
                {
                    _oib = value;
                }
                else
                {
                    throw new InvalidOperationException("");
                }
            }
        }
        public string JMBG
        {
            get
            {
                return _jmbg;
            }
            set
            {
                if (value.Length == 13 && long.TryParse(value, out long result))
                {
                    _jmbg = value;

                    int dan = int.Parse(value.Substring(0, 2));
                    int mjesec = int.Parse(value.Substring(2, 2));
                    int godina= int.Parse(value.Substring(4,3));

                    DatumRodjenja=new DateTime(godina+1000,mjesec,dan);

                }
                else
                {
                    throw new InvalidOperationException("");
                }
            }
        }

        public Osoba()
        {

        }

    }
    public enum Zvanje
    {
        Asistent,
        Predavac,
        VisiPredavac,
        ProfVisokeSkole
    }

    public class Profesor : Osoba
    {
        public string Odjel { get; set; }

        public Zvanje Zvanje {  get; set; }

        public DateTime DatumIzbora {  get; set; }

        public List<Predmet> Predmeti=new List<Predmet>();

        public int KolikoDoReizbora()
        {
            int godineIzbora;

            if (Zvanje == Zvanje.Asistent)
            {
                godineIzbora = 4;
            }
            else
            {
                godineIzbora = 5;
            }

            DateTime sljedeciIzbor=DatumIzbora.AddYears(godineIzbora);
            TimeSpan vrijemeDoIzbora = sljedeciIzbor - DateTime.Now;

            int preostaleGodine = sljedeciIzbor.Year - DateTime.Now.Year;

            return preostaleGodine;
        }
    }

    public class Student : Osoba
    {
        private string _jmbag;
        public string JMBAG
        {
            get { 
            return _jmbag;
            }
            set
            {
                if (value.Length == 10 && long.TryParse(value, out long result))
                {
                    _jmbag = value;
                }
                else
                {
                    throw new InvalidOperationException("");
                }

            }
        }
        public decimal Prosjek { get; set; }
        public int BrPolozeno { get; set; }
        public int ECTS {  get; set; }
    }

    public class Predmet
    {
        public int Sifra { get; set; }

        public int ECTS { get; set; }

        public string Naziv {  get; set; }
    }

    public class Fakultet
    {
        public List<Osoba> listOsoba { get; set; }

        public Fakultet() {
            listOsoba = new List<Osoba>();
            Osoba osoba = new Osoba();
            osoba.Ime = "Djuro";
            osoba.Prezime = "Djuric";
            osoba.JMBG = "1111111111111";
            osoba.OIB = "11111111111";
            listOsoba.Add(osoba);
        }

        public int KolikoProfesora()
        {
            int brojProfesora = 0;
            foreach (var osoba in listOsoba)
            {
                if (osoba is Profesor)
                {
                    brojProfesora++;
                }

            }
            return brojProfesora;

        }
        public int KolikoStudenata()
        {
            int brojStudenata = 0;
            foreach (var Osoba in listOsoba)
            {
                if (Osoba is Student)
                {
                    brojStudenata++;
                }

            }
            return brojStudenata;
        }

        public Student DohvatiStudenta(string JMBAG)
        {
            foreach (var osoba in listOsoba)
            {
                if (osoba is Student)
                {
                    Student student = osoba as Student;
                    if (student.JMBAG == JMBAG)
                    {
                        return student;
                    }
                }
            }
            return null;
        }

        public IEnumerable<Profesor> DohvatiProfesore()
        {
            List<Profesor> profesori = new List<Profesor>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Profesor)
                {
                    profesori.Add((Profesor)osoba);
                }
            }
            profesori.Sort((p1, p2) => p1.DatumIzbora.CompareTo(p2.DatumIzbora));

            return profesori;
        }

        public IEnumerable<Student> DohvatiStudente91()
        {
            List<Student> studenti = new List<Student>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Student)
                {
                    studenti.Add((Student)osoba);
                }
            }

            var studenti91 = studenti
                .Where(s => s.DatumRodjenja.Year > 1991);

            return studenti91;
        }

        public IEnumerable<Student> DohvatiStudente91NoLinq()
        {
            List<Student> studenti = new List<Student>();

            List<Student> studenti91 = new List<Student>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Student)
                {
                    studenti.Add(osoba as Student);
                }
            }

            foreach (var student in studenti)
            {
                if (student.DatumRodjenja.Year > 1991)
                {
                    studenti91.Add(student);
                }
            }

            return studenti91;
        }
        public IEnumerable<Student> StudentiNeTvzD() {
            List<Student> studenti = new List<Student>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Student)
                {
                    studenti.Add(osoba as Student);
                }
            }

            var studentiNeTvz = studenti
                .Where(s => s.JMBAG.Substring(0, 4) != "0246" && s.Prezime.Substring(0, 1) == "D");

            return studentiNeTvz;
        }
        public List<Student> DohvatiStudente91List()
        {
            List<Student> studenti = new List<Student>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Student)
                {
                    studenti.Add(osoba as Student);
                }
            }

            List<Student> studenti91 = studenti
                .Where(s => s.DatumRodjenja.Year > 1991)
                .ToList();

            return studenti91;
        }

        public Student NajboljiProsjek(int god)
        {
            List<Student> studenti = new List<Student>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Student)
                {
                    studenti.Add(osoba as Student);
                }
            }
            Student najboljiStudent = studenti
                .Where(s => s.DatumRodjenja.Year == god)
                .OrderByDescending(s => s.Prosjek)
                .FirstOrDefault();

            return najboljiStudent;
        }
        public List<Student> StudentiGodinaOrdered(int god)
        {
            List<Student> studenti = new List<Student>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Student)
                {
                    studenti.Add(osoba as Student);
                }
            }
            List<Student> poredaniStudenti = studenti
                .Where(s => s.DatumRodjenja.Year == god)
                .OrderByDescending(s => s.Prosjek)
                .ToList();

            return poredaniStudenti;
        }
        public List<Profesor> SviProfesori(bool asc)
        {
            List<Profesor> profesori = new List<Profesor>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Profesor)
                {
                    profesori.Add(osoba as Profesor);
                }
            }

            if (asc)
            {
                return profesori
                    .OrderBy(p => p.Prezime)
                    .ThenBy(p => p.Ime)
                    .ToList();
            }
            else
            {
                return profesori
                    .OrderByDescending (p => p.Prezime)
                    .ThenByDescending(p=>p.Ime)
                    .ToList();
            }
            
        }
        public int KolikoProfesoraUZvanju(Zvanje zvanje)
        {
            List<Profesor> profesori = new List<Profesor>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Profesor)
                {
                    profesori.Add(osoba as Profesor);
                }
            }

            int kolikoProfesora = profesori
                .Where(p=>p.Zvanje.Equals(zvanje))
                .Count();

            return kolikoProfesora;
        }

        public IEnumerable<Profesor> NeaktivniProfesori(int x)
        {
            List<Profesor> profesori = new List<Profesor>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Profesor)
                {
                    profesori.Add(osoba as Profesor);
                }
            }

            var neaktivniProfesori = profesori
                .Where(p => p.Zvanje.Equals(Zvanje.Predavac) || p.Zvanje.Equals(Zvanje.VisiPredavac))
                .Where(p => p.Predmeti.Count() < x);

            return neaktivniProfesori;
        }

        public IEnumerable<Profesor> AktivniAsistenti(int x, int minEcts)
        {
            List<Profesor> profesori = new List<Profesor>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Profesor)
                {
                    profesori.Add(osoba as Profesor);
                }
            }

            var aktivniAsistenti = profesori
                .Where(p => p.Zvanje.Equals(Zvanje.Asistent)&&
                    p.Predmeti.Count>x && 
                    p.Predmeti.Count(pr=>pr.ECTS>=minEcts)>0);

            return aktivniAsistenti;
        }

        public void IzmjeniProfesore(Action<Profesor> action)
        {
            List<Profesor> profesori = new List<Profesor>();

            foreach (var osoba in listOsoba)
            {
                if (osoba is Profesor)
                {
                    profesori.Add(osoba as Profesor);
                }
            }
            foreach (var profesor in profesori)
            {
                action(profesor);
            }
        }

        public void Tasks()
        {
            Task t1 = Task.Run(() =>
            {
                Console.WriteLine($"Sleeping started");
                Thread.Sleep(1000);
                Console.WriteLine($"Sleeping completed");
            });
            Console.WriteLine($"Waiting on task..");
            t1.Wait();

            Task t2 = Task.Run(() =>
            {
                Console.WriteLine($"Sleeping started");
                Thread.Sleep(1500);
                Console.WriteLine($"Sleeping completed");
            });
            Console.WriteLine($"Waiting on task..");
            t2.Wait();
        }
        static async Task SleepF1()
        {
            Console.WriteLine("Početak SleepF1");
            await SleepF2();
            await Task.Delay(1000);
            Console.WriteLine("Kraj SleepF1");
        }

        static async Task SleepF2()
        {
            Console.WriteLine("Početak SleepF2");
            await Task.Delay(1000); 
            Console.WriteLine("Kraj SleepF2");
        }

    }
    
}
