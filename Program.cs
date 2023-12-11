using System;

public enum Education
{
    Specialist,
    Bachelor,
    SecondEducation
}

public class Exam
{
    public string Subject { get; set; }
    public int Grade { get; set; }
    public DateTime ExamDate { get; set; }

    public Exam(string subject, int grade, DateTime examDate)
    {
        Subject = subject;
        Grade = grade;
        ExamDate = examDate;
    }

    public Exam()
    {
        Subject = "DefaultSubject";
        Grade = 0;
        ExamDate = DateTime.Now;
    }

    public override string ToString()
    {
        return $"{Subject ?? "Unknown Subject"} - Grade: {Grade}, Exam Date: {ExamDate.ToShortDateString()}";
    }
}

public class Person
{
    private string firstName;
    private string lastName;
    private DateTime birthDate;

    public Person(string firstName, string lastName, DateTime birthDate)
    {
        this.firstName = firstName;
        this.lastName = lastName;
        this.birthDate = birthDate;
    }

    public Person()
    {
        this.firstName = "Name";
        this.lastName = "Surname";
        this.birthDate = new DateTime(2000, 5, 20);
    }

    public string FirstName
    {
        get { return firstName; }
        set { firstName = value; }
    }

    public string LastName
    {
        get { return lastName; }
        set { lastName = value; }
    }

    public DateTime BirthDate
    {
        get { return birthDate; }
        set { birthDate = value; }
    }


    public int BirthYear
    {
        get { return birthDate.Year; }
        set { birthDate = new DateTime(value, birthDate.Month, birthDate.Day); }
    }

    public override string ToString()
    {
        return $"Name: {FirstName} {LastName}, Birthdate: {BirthDate.ToShortDateString()}";
    }
}

public class Student
{
    private Person person;
    private Education education;
    private int groupNumber;
    private Exam[] exams;

    public Student(Person person, Education education, int groupNumber)
    {
        this.person = person;
        this.education = education;
        this.groupNumber = groupNumber;
        this.exams = new Exam[0];
    }

    public Student() : this(new Person(), Education.Bachelor, 1)
    {
    }

    public Person Person
    {
        get { return person; }
        set { person = value; }
    }

    public Education Education
    {
        get { return education; }
        set { education = value; }
    }

    public int GroupNumber
    {
        get { return groupNumber; }
        set { groupNumber = value; }
    }

    public Exam[] Exams
    {
        get { return exams; }
        set { exams = value; }
    }

    public double AverageGrade
    {
        get
        {
            if (exams == null || exams.Length == 0)
                return 0;

            int totalGrade = 0;
            foreach (var exam in exams)
            {
                totalGrade += exam.Grade;
            }

            return (double)totalGrade / exams.Length;
        }
    }

    public bool this[Education edu]
    {
        get { return education == edu; }
    }

    public void AddExams(params Exam[] newExams)
    {
        Array.Resize(ref exams, exams.Length + newExams.Length);
        for (int i = 0; i < newExams.Length; i++)
        {
            exams[exams.Length - newExams.Length + i] = newExams[i];
        }
    }

    public override string ToString()
    {
        string result = $"Student: {person}, Education: {education}, Group: {groupNumber}\nExams:\n";
        if (exams != null)
        {
            foreach (var exam in exams)
            {
                result += $"{exam}\n";
            }
        }
        return result;
    }

    public virtual string ToShortString()
    {
        return $"Student: {person}, Education: {education}, Group: {groupNumber}, Average Grade: {AverageGrade:F2}";
    }
}

class Program
{
    static void Main()
    {
        Student student = new Student(new Person("Artem", "Kraievyi", new DateTime(1995, 8, 3)),
                                     Education.Bachelor, 1);

        Console.WriteLine("Initial Student Data:\n" + student.ToShortString());

        Console.WriteLine($"Is Bachelor: {student[Education.Bachelor]}");
        Console.WriteLine($"Is Specialist: {student[Education.Specialist]}");
        Console.WriteLine($"Is SecondEducation: {student[Education.SecondEducation]}");

        student.Person = new Person("Artem", "Kraievyi", new DateTime(1995, 8, 3));
        student.Education = Education.Specialist;
        student.GroupNumber = 2;

        Exam[] newExams = { new Exam("Python", 90, new DateTime(2023, 12, 5)),
                            new Exam("OOP", 85, new DateTime(2023, 12, 13)) };
        student.AddExams(newExams);

        Console.WriteLine("\nUpdated Student Data:\n" + student.ToString());

        Console.ReadLine();
    }
}
