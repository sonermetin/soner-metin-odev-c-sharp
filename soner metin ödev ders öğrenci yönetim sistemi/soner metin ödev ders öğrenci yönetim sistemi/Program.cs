using System;
using System.Collections.Generic;

public interface IPerson
{
    void Login();
    void ShowInfo();
}

public class Person : IPerson
{
    public string FirstName { get; set; }
    public string LastName { get; set; }

    public virtual void Login()
    {
        Console.WriteLine($"{FirstName} {LastName} giriş yaptı.");
    }

    public virtual void ShowInfo()
    {
        Console.WriteLine($"Ad: {FirstName}, Soyad: {LastName}");
    }
}

public class Ogrenci : Person
{
    public int StudentID { get; set; }
    public Bölüm StudentDepartment { get; set; }
    public List<Ders> EnrolledCourses { get; set; } = new List<Ders>();

    public override void ShowInfo()
    {
        base.ShowInfo();
        Console.WriteLine($"Öğrenci ID: {StudentID}");

        if (StudentDepartment != null)
        {
            Console.WriteLine($"Bölüm: {StudentDepartment.DepartmentName}");
        }
        else
        {
            Console.WriteLine("Bölüm bilgisi mevcut değil.");
        }

        Console.WriteLine("Kayıtlı Olduğu Dersler:");
        foreach (var course in EnrolledCourses)
        {
            Console.WriteLine($"- {course.CourseName} ({course.Credits} kredisi)");
        }
    }

    public void EnrollInCourse(Ders course)
    {
        EnrolledCourses.Add(course);
        Console.WriteLine($"{FirstName} {LastName}, {course.CourseName} dersine kaydoldu.");
    }
}

public class OgretimGorevlisi : Person
{
    public Bölüm Department { get; set; }
    public List<Ders> AvailableCourses { get; set; } = new List<Ders>();

    public override void ShowInfo()
    {
        base.ShowInfo();
        Console.WriteLine($"Bölüm: {Department.DepartmentName}");
    }

    public void CreateCourses()
    {
        AvailableCourses.Add(new Ders { CourseName = "C# Programlama", Credits = 4, Instructor = this });
        AvailableCourses.Add(new Ders { CourseName = "Veritabanı Yönetim Sistemleri", Credits = 3, Instructor = this });
        AvailableCourses.Add(new Ders { CourseName = "Algoritmalar ve Veri Yapıları", Credits = 4, Instructor = this });
        AvailableCourses.Add(new Ders { CourseName = "İngilizce Teknik Yazışma", Credits = 2, Instructor = this });
        AvailableCourses.Add(new Ders { CourseName = "Web Tabanlı Uygulama Geliştirme", Credits = 5, Instructor = this });
    }

    public void ShowAvailableCourses()
    {
        Console.WriteLine("Mevcut Dersler:");
        for (int i = 0; i < AvailableCourses.Count; i++)
        {
            var course = AvailableCourses[i];
            Console.WriteLine($"{i + 1}. {course.CourseName} ({course.Credits} kredisi)");
        }
    }
}

public class Ders
{
    public string CourseName { get; set; }
    public int Credits { get; set; }
    public OgretimGorevlisi Instructor { get; set; }
    public List<Ogrenci> EnrolledStudents { get; set; } = new List<Ogrenci>();

    public void AddStudent(Ogrenci student)
    {
        EnrolledStudents.Add(student);
        Console.WriteLine($"{student.FirstName} {student.LastName} derse kaydoldu.");
    }

    public void ShowCourseInfo()
    {
        Console.WriteLine($"Ders: {CourseName}, Krediler: {Credits}, Öğretim Görevlisi: {Instructor.FirstName} {Instructor.LastName}");
        Console.WriteLine("Kayıtlı Öğrenciler:");
        foreach (var student in EnrolledStudents)
        {
            Console.WriteLine($"- {student.FirstName} {student.LastName}");
        }
    }
}

public class Bölüm
{
    public string DepartmentName { get; set; }
    public List<Ders> OfferedCourses { get; set; } = new List<Ders>();

    public void ShowDepartmentInfo()
    {
        Console.WriteLine($"Bölüm: {DepartmentName}");
        Console.WriteLine("Sunulan Dersler:");
        foreach (var course in OfferedCourses)
        {
            Console.WriteLine($"- {course.CourseName} ({course.Credits} kredisi)");
        }
    }
}

public class Program
{
    public static void Main()
    {
        Bölüm bilgisayarMuhendisligi = new Bölüm { DepartmentName = "Bilgisayar Mühendisliği" };
        Bölüm yazilimMuhendisligi = new Bölüm { DepartmentName = "Yazılım Mühendisliği" };

        OgretimGorevlisi ogretimGorevlisi = new OgretimGorevlisi();
        Console.Write("Öğretim görevlisinin adını girin: ");
        ogretimGorevlisi.FirstName = Console.ReadLine();

        Console.Write("Öğretim görevlisinin soyadını girin: ");
        ogretimGorevlisi.LastName = Console.ReadLine();

        Console.Write("Öğretim görevlisinin bölümünü girin (Bilgisayar Mühendisliği / Yazılım Mühendisliği): ");
        string departmentName = Console.ReadLine().ToLower();
        if (departmentName == "bilgisayar mühendisliği")
        {
            ogretimGorevlisi.Department = bilgisayarMuhendisligi;
        }
        else if (departmentName == "yazılım mühendisliği")
        {
            ogretimGorevlisi.Department = yazilimMuhendisligi;
        }
        else
        {
            Console.WriteLine("Geçersiz bölüm adı.");
            return;
        }

        ogretimGorevlisi.CreateCourses();

        List<Ogrenci> ogrenciler = new List<Ogrenci>();
        bool addMoreStudents = true;

        while (addMoreStudents)
        {
            Ogrenci ogrenci = new Ogrenci();

            Console.Write("Öğrencinin adını girin: ");
            ogrenci.FirstName = Console.ReadLine();

            Console.Write("Öğrencinin soyadını girin: ");
            ogrenci.LastName = Console.ReadLine();

            Console.Write("Öğrencinin ID'sini girin: ");
            ogrenci.StudentID = Convert.ToInt32(Console.ReadLine());

            Console.Write("Öğrencinin bölümünü seçin (Bilgisayar Mühendisliği / Yazılım Mühendisliği): ");
            string ogrenciBölüm = Console.ReadLine().ToLower();
            if (ogrenciBölüm == "bilgisayar mühendisliği")
            {
                ogrenci.StudentDepartment = bilgisayarMuhendisligi;
            }
            else if (ogrenciBölüm == "yazılım mühendisliği")
            {
                ogrenci.StudentDepartment = yazilimMuhendisligi;
            }
            else
            {
                Console.WriteLine("Geçersiz bölüm adı.");
                return;
            }

            ogrenciler.Add(ogrenci);

            bool addMoreCourses = true;
            while (addMoreCourses)
            {
                ogretimGorevlisi.ShowAvailableCourses();

                Console.Write("Kaydolmak istediğiniz dersin numarasını girin: ");
                int courseIndex = Convert.ToInt32(Console.ReadLine()) - 1;

                if (courseIndex >= 0 && courseIndex < ogretimGorevlisi.AvailableCourses.Count)
                {
                    Ders selectedCourse = ogretimGorevlisi.AvailableCourses[courseIndex];
                    ogrenci.EnrollInCourse(selectedCourse);
                    selectedCourse.AddStudent(ogrenci);
                }
                else
                {
                    Console.WriteLine("Geçersiz ders numarası.");
                }

                Console.Write("Başka bir derse kaydolmak istiyor musunuz? (Evet/Hayır): ");
                string response = Console.ReadLine();
                addMoreCourses = response.ToLower() == "evet";
            }

            ogrenci.ShowInfo();

            Console.Write("Başka bir öğrenci eklemek istiyor musunuz? (Evet/Hayır): ");
            string studentResponse = Console.ReadLine();
            addMoreStudents = studentResponse.ToLower() == "evet";
        }

        Console.WriteLine("\nTüm Öğrenciler ve Kayıtlı Oldukları Dersler:");
        foreach (var ogrenci in ogrenciler)
        {
            ogrenci.ShowInfo();
        }
    }
}
