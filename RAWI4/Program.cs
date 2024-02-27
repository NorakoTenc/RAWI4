using System;
using System.Reflection;
public class Person
{
    private string _firstName;
    public string LastName { get; set; }
    internal int Age { get; private set; }
    protected DateTime BirthDate { get; }
    public readonly string Gender; 

    public Person(string firstName, string lastName, int age, DateTime birthDate, string gender)
    {
        _firstName = firstName;
        LastName = lastName;
        Age = age;
        BirthDate = birthDate;
        Gender = gender;
    }
    public string GetFullName() => $"{_firstName} {LastName}";
    public void SetAge(int age)
    {
        if (age < 0)
        {
            throw new ArgumentOutOfRangeException(nameof(age), "Age cannot be negative.");
        }

        Age = age;
    }
    private void PrintBirthDate() => Console.WriteLine(BirthDate.ToString("dd.MM.yyyy"));
}

class Program
{
    static void Main(string[] args)
    {
        Person person = new Person("Solko", "Kolko", 25, new DateTime(1997, 12, 31), "M");
        Console.WriteLine("-------------------------Type  TypeInfo-------------------------");
        Type personType = person.GetType();
        TypeInfo personTypeInfo = personType.GetTypeInfo();
        Console.WriteLine(personType.IsClass);
        Console.WriteLine(personTypeInfo.Name);
        Console.WriteLine(personTypeInfo.FullName);
        Console.WriteLine("-------------------------MemberInfo-------------------------");
        MemberInfo[] members = personType.GetMembers();
        foreach (MemberInfo member in members)
        {
            Console.WriteLine($"{member.MemberType} - {member.Name}");
        }
        Console.WriteLine("-------------------------FieldInfo-------------------------");
        FieldInfo firstNameField = personType.GetField("_firstName", BindingFlags.NonPublic | BindingFlags.Instance);
        object firstNameValue = firstNameField.GetValue(person);
        Console.WriteLine($"Name: {firstNameValue}");
        firstNameField.SetValue(person, "Olgo");
        Console.WriteLine($"New name: {person.GetFullName()}");
        Console.WriteLine("-------------------------MethodInfo-------------------------");
        MethodInfo getFullNameMethod = personType.GetMethod("GetFullName");
        string fullName = (string)getFullNameMethod.Invoke(person, null);
        Console.WriteLine(fullName);
        Console.WriteLine("-------------------------Reflection-------------------------");
        MethodInfo printBirthDateMethod = personType.GetMethod("PrintBirthDate", BindingFlags.NonPublic | BindingFlags.Instance);
        printBirthDateMethod.Invoke(person, null);
    }
}
